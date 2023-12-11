namespace AoC;

/// <summary>
/// Field uses a coordinate system where (0,0) is at the top left,
/// with positive x goes right, and positive y goes down.
/// </summary>
public class Field
{
    private readonly Tile[,] _tiles;
    private readonly int _width;
    private readonly int _height;
    private readonly Point _animalStartPosition;
    private List<Point>? _animalPositions;

    // This map is used to replace the slow AnimalPositions.IndexOf(Point):
    private readonly Dictionary<Point, int> _pointToIndexMap = new();

    public Field(string[] lines)
    {
        _width = lines[0].Length;
        _height = lines.Length;
        _tiles = new Tile[_width, _height];

        _animalStartPosition = FillTiles(lines);
    }

    public Point AnimalStartPosition => _animalStartPosition;

    public Tile At(Point pos) => _tiles[pos.X, pos.Y];

    public List<Point> AnimalPositions
    {
        get
        {
            _animalPositions ??= GetAnimalPositions();
            return _animalPositions;
        }
    }

    private Point FillTiles(string[] lines)
    {
        Point animalStartPosition = new(-1, -1);
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Tile tile = CharToTile(lines[y][x]);
                _tiles[x, y] = tile;
                if (tile == Tile.Start)
                {
                    animalStartPosition = new Point(x, y);
                }
            }
        }
        return animalStartPosition;
    }

    private static Tile CharToTile(char ch)
    {
        return ch switch
        {
            'S' => Tile.Start,
            '|' => Tile.Vertical,
            '-' => Tile.Horizontal,
            'L' => Tile.NorthEast,
            'J' => Tile.NorthWest,
            'F' => Tile.SouthEast,
            '7' => Tile.SouthWest,
            _ => Tile.None,
        };
    }

    private List<Point> GetAnimalPositions()
    {
        Point currentPosition = AnimalStartPosition;
        List<Point> positions = new() { currentPosition };

        Point? nextPosition = GetNextAnimalPosition(currentPosition, positions);
        while (nextPosition != null)
        {
            positions.Add(nextPosition);
            nextPosition = GetNextAnimalPosition(nextPosition, positions);
        }

        for (int i = 0; i < positions.Count; i++)
        {
            _pointToIndexMap[positions[i]] = i;
        }

        return positions;
    }

    private Point? GetNextAnimalPosition(Point currentPosition, List<Point> positions)
    {
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            (bool canMove, Point? nextPosition) = CanMove(direction, currentPosition, positions);
            if (canMove)
            {
                return nextPosition;
            }
        }

        return null;
    }

    private (bool canMove, Point? nextPosition) CanMove(Direction direction, Point currentPosition, List<Point> positions)
    {
        Dictionary<Direction, Point> directionDeltas = new()
        {
            [Direction.Up] = new Point(0, -1),
            [Direction.Right] = new Point(1, 0),
            [Direction.Down] = new Point(0, 1),
            [Direction.Left] = new Point(-1, 0)
        };

        Point delta = directionDeltas[direction];
        Point nextPosition = currentPosition.Move(delta.X, delta.Y);
        if (nextPosition.X < 0 || nextPosition.X >= _width || nextPosition.Y < 0 || nextPosition.Y >= _height)
        {
            return (false, null);
        }

        Tile currentTile = At(currentPosition);
        Tile nextTile = At(nextPosition);

        bool canMove = direction switch
        {
            Direction.Up => currentTile.CanMoveUpTo(nextTile),
            Direction.Right => currentTile.CanMoveRightTo(nextTile),
            Direction.Down => currentTile.CanMoveDownTo(nextTile),
            Direction.Left => currentTile.CanMoveLeftTo(nextTile),
            _ => throw new Exception($"Unknown direction {direction}")
        };
        if (canMove && !positions.Contains(nextPosition))
        {
            return (true, nextPosition);
        }

        return (false, null);
    }

    public int GetNrEnclosedTiles()
    {
        // Create HashSet so the check if a position is on the loop is way faster.
        HashSet<Point> positions = new(GetAnimalPositions());

        // We're using raycasting to determine how many loop boundaries are crossed
        // from a certain point. An even number means the point lies outside the loop,
        // odd means inside the loop.
        // Because we need to determine of every point if it's inside or outside the
        // loop, we're using _reverse_ raycasting: we're casting to the left, but
        // we're walking from left to right. We're keeping count of the number of
        // bounderies we're crossing. When we're on a non-loop tile, the evenness
        // of the number of crossings will tell if that tile is inside or outside the
        // loop.
        int nrEnclosedTiles = 0;
        for (int y = 0; y < _height; y++)
        {
            nrEnclosedTiles += GetNrEnclosedTilesForRow(y, positions);
        }

        return nrEnclosedTiles;
    }

    private int GetNrEnclosedTilesForRow(int y, HashSet<Point> positions)
    {
        int nrEnclosedTiles = 0;
        int nrBoundaryCrossings = 0;
        for (int x = 0; x < _width; x++)
        {
            Point pos = new(x, y);
            if (positions.Contains(pos))
            {
                // The tile is part of the loop, this means it's outside the loop.
                // However, this means we're possibly about to cross a boundary.
                // We're only actually crossing a boundary when we follow the loop
                // both sides until the Y values are different from the current Y.
                // When both these Y values are the same, we don't actually cross
                // a boundary.
                // |     => a boundary crossing
                // F---7 => not a boundary crossing
                // F---J => a boundary crossing
                (bool isBoundaryCrossing, int maxX) = IsBoundaryCrossing(pos);
                if (isBoundaryCrossing)
                {
                    nrBoundaryCrossings++;
                }
                // To prevent duplicate boundary crossings we have to skip the segment
                // of the loop that's on the same line.
                x = maxX;
            }
            else
            {
                if (nrBoundaryCrossings % 2 == 1)
                {
                    nrEnclosedTiles++;
                }
            }
        }

        return nrEnclosedTiles;
    }

    private (bool isBoundaryCrossing, int maxX) IsBoundaryCrossing(Point pos)
    {
        Point p1 = GetPreviousPositionOnDifferentLine(pos);
        Point p2 = GetNextPositionOnDifferentLine(pos);

        return (p2.Y != p1.Y, Math.Max(p1.X, p2.X));
    }

    private Point GetPreviousPositionOnDifferentLine(Point pos)
    {
        int startY = pos.Y;
        int i = _pointToIndexMap[pos];
        while (pos.Y == startY)
        {
            i = (i - 1 + AnimalPositions.Count) % AnimalPositions.Count;
            pos = AnimalPositions[i];
        }
        return pos;
    }

    private Point GetNextPositionOnDifferentLine(Point pos)
    {
        int startY = pos.Y;
        int i = _pointToIndexMap[pos];
        while (pos.Y == startY)
        {
            i = (i + 1) % AnimalPositions.Count;
            pos = AnimalPositions[i];
        }
        return pos;
    }
}