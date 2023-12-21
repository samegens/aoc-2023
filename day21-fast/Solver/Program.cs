
namespace AoC;

public class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    static int width = 0;
    static int height = 0;

    static readonly Dictionary<Tuple<Point, int>, HashSet<Point>> _cachedCounts = new();

    private static void SolvePart1(string[] lines)
    {
        width = lines[0].Length;
        height = lines.Length;
        int total = 0;
        Point start = new(0, 0);
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            int i = line.IndexOf('S');
            if (i >= 0)
            {
                start = new(i, y);
            }
        }

        total = Visit(lines, start, 64).Count;
        Console.WriteLine($"Part 1: {total}");
    }

    private static HashSet<Point> Visit(string[] lines, Point p, int stepsLeft)
    {
        if (p.X < 0 || p.X >= width || p.Y < 0 || p.Y >= height) return new HashSet<Point>();
        if (lines[p.Y][p.X] == '#') return new HashSet<Point>();
        var t = new Tuple<Point, int>(p, stepsLeft);
        if (_cachedCounts.ContainsKey(t)) return _cachedCounts[t];
        if (stepsLeft == 0)
        {
            var h = new HashSet<Point>
            {
                p
            };
            return h;
        }

        var all = Visit(lines, p.Move(0, -1), stepsLeft - 1);
        all.UnionWith(Visit(lines, p.Move(1, 0), stepsLeft - 1));
        all.UnionWith(Visit(lines, p.Move(0, 1), stepsLeft - 1));
        all.UnionWith(Visit(lines, p.Move(-1, 0), stepsLeft - 1));
        _cachedCounts[t] = all;
        return all;
    }

    private static void SolvePart2(string[] lines)
    {
        width = lines[0].Length;
        height = lines.Length;
        Point start = new(0, 0);
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            int i = line.IndexOf('S');
            if (i >= 0)
            {
                start = new(i, y);
            }
        }

        int[,] shortestPath = new int[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                shortestPath[x, y] = int.MaxValue;
            }
        }

        Visit3(lines, shortestPath, start, 0);

        // I couldn't figure out this myself, so I used
        // https://github.com/villuna/aoc23/wiki/A-Geometric-solution-to-advent-of-code-2023,-day-21
        long nrEvenTilesInFullSquare = 0;
        long nrOddTilesInFullSquare = 0;
        long nrEvenTilesInCorner = 0;
        long nrOddTilesInCorner = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (shortestPath[x, y] != int.MaxValue)
                {
                    if (shortestPath[x, y] % 2 == 0)
                    {
                        nrEvenTilesInFullSquare++;
                        if (shortestPath[x, y] > 65)
                            nrEvenTilesInCorner++;
                    }
                    else
                    {
                        nrOddTilesInFullSquare++;
                        if (shortestPath[x, y] > 65)
                            nrOddTilesInCorner++;
                    }
                }
            }
        }

        long n = 202300;
        long total = (n + 1) * (n + 1) * nrOddTilesInFullSquare
            + n * n * nrEvenTilesInFullSquare
            - (n + 1) * nrOddTilesInCorner
            + n * nrEvenTilesInCorner;
        Console.WriteLine($"Part 2: {total}");
    }

    private static void Visit3(string[] lines, int[,] shortestPath, Point p, int steps)
    {
        if (p.X < 0 || p.X >= width || p.Y < 0 || p.Y >= height) return;
        if (lines[p.Y][p.X] == '#') return;

        if (steps < shortestPath[p.X, p.Y])
            shortestPath[p.X, p.Y] = steps;
        else
            return;

        Visit3(lines, shortestPath, p.Move(0, -1), steps + 1);
        Visit3(lines, shortestPath, p.Move(1, 0), steps + 1);
        Visit3(lines, shortestPath, p.Move(0, 1), steps + 1);
        Visit3(lines, shortestPath, p.Move(-1, 0), steps + 1);
    }
}
