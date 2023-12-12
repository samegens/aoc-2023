namespace AoC;

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
        long total = 0;
        List<Point> ps = new();
        List<int> ex = new();
        List<int> ey = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            bool lineEmpty = true;
            for (int x = 0; x < lines[0].Length; x++)
            {
                if (lines[y][x] == '#')
                {
                    ps.Add(new Point(x, y));
                    lineEmpty = false;
                }
            }
            if (lineEmpty)
            {
                ey.Add(y);
            }
        }
        for (int x = 0; x < lines[0].Length; x++)
        {
            bool lineEmpty = true;
            for (int y = 0; y < lines[0].Length; y++)
            {
                if (lines[y][x] == '#')
                {
                    lineEmpty = false;
                }
            }
            if (lineEmpty)
            {
                ex.Add(x);
            }
        }
        for (int i = 0; i < ey.Count; i++)
        {
            int y = ey[i];
            foreach (var p in ps)
            {
                if (p.Y > y)
                {
                    p.Y++;
                }
            }
            for (int j = 0; j < ey.Count; j++)
            {
                if (ey[j] > y) ey[j]++;
            }
        }
        for (int i = 0; i < ex.Count; i++)
        {
            int x = ex[i];
            foreach (var p in ps)
            {
                if (p.X > x)
                {
                    p.X++;
                }
            }
            for (int j = 0; j < ex.Count; j++)
            {
                if (ex[j] > x) ex[j]++;
            }
        }
        for (int i = 0; i < ps.Count; i++)
        {
            for (int j = i + 1; j < ps.Count; j++)
            {
                var p1 = ps[i];
                var p2 = ps[j];
                total += Math.Abs(p2.X - p1.X) + Math.Abs(p2.Y - p1.Y);
            }
        }
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        long total = 0;
        List<Point> ps = new();
        List<long> ex = new();
        List<long> ey = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            bool lineEmpty = true;
            for (int x = 0; x < lines[0].Length; x++)
            {
                if (lines[y][x] == '#')
                {
                    ps.Add(new Point(x, y));
                    lineEmpty = false;
                }
            }
            if (lineEmpty)
            {
                ey.Add(y);
            }
        }
        for (int x = 0; x < lines[0].Length; x++)
        {
            bool lineEmpty = true;
            for (int y = 0; y < lines[0].Length; y++)
            {
                if (lines[y][x] == '#')
                {
                    lineEmpty = false;
                }
            }
            if (lineEmpty)
            {
                ex.Add(x);
            }
        }
        for (int i = 0; i < ey.Count; i++)
        {
            long y = ey[i];
            foreach (var p in ps)
            {
                if (p.Y > y)
                {
                    p.Y += 999999L;
                }
            }
            for (int j = 0; j < ey.Count; j++)
            {
                if (ey[j] > y) ey[j] += 999999L;
            }
        }
        for (int i = 0; i < ex.Count; i++)
        {
            long x = ex[i];
            foreach (var p in ps)
            {
                if (p.X > x)
                {
                    p.X += 999999L;
                }
            }
            for (int j = 0; j < ex.Count; j++)
            {
                if (ex[j] > x) ex[j] += 999999L;
            }
        }
        for (int i = 0; i < ps.Count; i++)
        {
            for (int j = i + 1; j < ps.Count; j++)
            {
                var p1 = ps[i];
                var p2 = ps[j];
                total += Math.Abs(p2.X - p1.X) + Math.Abs(p2.Y - p1.Y);
            }
        }
        Console.WriteLine($"Part 2: {total}");
    }
}
