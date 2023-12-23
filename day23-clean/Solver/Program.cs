
namespace AoC;

public class Program
{
    private static Dictionary<Direction, char> DirectionToAllowedTileMap = new()
    {
        [Direction.Up] = '^',
        [Direction.Right] = '>',
        [Direction.Down] = 'v',
        [Direction.Left] = '<'
    };

    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    private static void SolvePart1(string[] lines)
    {
        Board board = new(lines);

        int longestPath = int.MinValue;
        HashSet<Point> visitedPoints = new();
        Point start = new(1, 0);
        Point end = new(board.Width - 2, board.Height - 1);
        DetermineLongestPath(board, start, end, 0, visitedPoints, ref longestPath);

        Console.WriteLine($"Part 1: {longestPath}");
    }

    private static void DetermineLongestPath(Board board, Point p, Point end, int nrSteps, HashSet<Point> visitedPoints, ref int longestPath)
    {
        if (p.Equals(end))
        {
            if (nrSteps > longestPath)
            {
                longestPath = nrSteps;
            }
            return;
        }

        visitedPoints.Add(p);
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            var newP = p.Move(DirectionHelpers.Movements[dir]);
            if (!visitedPoints.Contains(newP) &&
                board.Contains(newP) &&
                board[newP] != '#' &&
                TileMatchesDirection(board[newP], dir))
            {
                DetermineLongestPath(board, newP, end, nrSteps + 1, visitedPoints, ref longestPath);
            }
        }
        visitedPoints.Remove(p);
    }

    private static bool TileMatchesDirection(char tile, Direction dir)
    {
        return tile == '.' || DirectionToAllowedTileMap[dir] == tile;
    }

    private static void SolvePart2(string[] lines)
    {
    }
}
