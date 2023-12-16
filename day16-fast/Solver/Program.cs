namespace AoC;

enum Direction
{
    Up,
    Right,
    Down,
    Left
}

class Cave
{
    public int Width;
    public int Height;
    public char[,] Tiles;

    public Cave(string[] lines)
    {
        Width = lines[0].Length;
        Height = lines.Length;
        Tiles = new char[Width, Height];
        for (int y = 0; y < Height; y++)
        {
            string line = lines[y];
            for (int x = 0; x < Width; x++)
                Tiles[x, y] = line[x];
        }
    }

    public char At(Point p) => Tiles[p.X, p.Y];

    public void Print()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Console.Write(Tiles[x, y]);
            }
            Console.WriteLine();
        }
    }
}

class Beam
{
    public Point Position;
    public Direction Direction;

    public Beam(Point position, Direction direction)
    {
        Position = position;
        Direction = direction;
    }

    public bool IsEnded(Cave cave)
    {
        return Position.X < 0 || Position.X >= cave.Width || Position.Y < 0 || Position.Y >= cave.Height;
    }

    public IEnumerable<Beam> Travel(Cave cave)
    {
        switch (Direction)
        {
            case Direction.Left:
                Position = Position.Move(-1, 0);
                break;
            case Direction.Right:
                Position = Position.Move(1, 0);
                break;
            case Direction.Up:
                Position = Position.Move(0, -1);
                break;
            case Direction.Down:
                Position = Position.Move(0, 1);
                break;
        }

        if (IsEnded(cave))
            yield break;

        switch (Direction)
        {
            case Direction.Left:
                switch (cave.At(Position))
                {
                    case '|':
                        Direction = Direction.Up;
                        yield return new Beam(Position, Direction.Down);
                        break;
                    case '/':
                        Direction = Direction.Down;
                        break;
                    case '\\':
                        Direction = Direction.Up;
                        break;
                }
                break;
            case Direction.Right:
                switch (cave.At(Position))
                {
                    case '|':
                        Direction = Direction.Up;
                        yield return new Beam(Position, Direction.Down);
                        break;
                    case '/':
                        Direction = Direction.Up;
                        break;
                    case '\\':
                        Direction = Direction.Down;
                        break;
                }
                break;
            case Direction.Up:
                switch (cave.At(Position))
                {
                    case '-':
                        Direction = Direction.Left;
                        yield return new Beam(Position, Direction.Right);
                        break;
                    case '/':
                        Direction = Direction.Right;
                        break;
                    case '\\':
                        Direction = Direction.Left;
                        break;
                }
                break;
            case Direction.Down:
                switch (cave.At(Position))
                {
                    case '-':
                        Direction = Direction.Left;
                        yield return new Beam(Position, Direction.Right);
                        break;
                    case '/':
                        Direction = Direction.Left;
                        break;
                    case '\\':
                        Direction = Direction.Right;
                        break;
                }
                break;
        }
    }
}

public class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    private static void SolvePart1(string[] lines)
    {
        Cave cave = new(lines);
        Beam beam = new(new Point(-1, 0), Direction.Right);
        int total = GetNrLitTiles(cave, beam);
        Console.WriteLine($"Part 1: {total}");
    }

    private static int GetNrLitTiles(Cave cave, Beam beam)
    {
        bool[,] visited = new bool[cave.Width, cave.Height];
        HashSet<Tuple<Point, Direction>> history = new();
        List<Beam> beams = new() { beam };
        while (true)
        {
            List<Beam> newBeams = new();
            foreach (var b in beams)
            {
                newBeams.AddRange(b.Travel(cave));
            }

            foreach (var b in newBeams)
            {
                if (!history.Contains(new Tuple<Point, Direction>(b.Position, b.Direction)))
                    beams.Add(b);
            }

            foreach (var b in beams)
            {
                if (history.Contains(new Tuple<Point, Direction>(b.Position, b.Direction)))
                {
                    b.Position = new Point(-1, -1);
                }
                if (!b.IsEnded(cave))
                {
                    visited[b.Position.X, b.Position.Y] = true;
                    history.Add(new Tuple<Point, Direction>(b.Position, b.Direction));
                }
            }

            if (beams.All(b => b.IsEnded(cave)))
                break;

            if (beams.Count > 1000)
                throw new Exception("cycle detected");
        }

        int total = 0;
        for (int y = 0; y < cave.Height; y++)
        {
            for (int x = 0; x < cave.Width; x++)
            {
                total += visited[x, y] ? 1 : 0;
            }
        }

        return total;
    }

    private static void SolvePart2(string[] lines)
    {
        Cave cave = new(lines);
        int total = 0;
        for (int y = 0; y < cave.Height; y++)
        {
            Beam beam = new(new Point(-1, y), Direction.Right);
            total = Math.Max(total, GetNrLitTiles(cave, beam));
        }
        for (int y = 0; y < cave.Height; y++)
        {
            Beam beam = new(new Point(cave.Width, y), Direction.Left);
            total = Math.Max(total, GetNrLitTiles(cave, beam));
        }
        for (int x = 0; x < cave.Width; x++)
        {
            Beam beam = new(new Point(x, -1), Direction.Down);
            total = Math.Max(total, GetNrLitTiles(cave, beam));
        }
        for (int x = 0; x < cave.Width; x++)
        {
            Beam beam = new(new Point(x, cave.Height), Direction.Up);
            total = Math.Max(total, GetNrLitTiles(cave, beam));
        }
        Console.WriteLine($"Part 2: {total}");
    }
}
