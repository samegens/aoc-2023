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
        int sx = -1;
        int sy = -1;
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            int pos = line.IndexOf('S');
            if (pos >= 0)
            {
                sx = pos;
                sy = y;
                break;
            }
        }

        Dictionary<Point, int> dists = new();
        VisitPart1(new Point(sx, sy), new Point(sx, sy), 0, dists, lines);
        int total = dists.Values.Max();
        Console.WriteLine($"Part 1: {total}");
    }

    private static void VisitPart1(Point start, Point current, int current_dist, Dictionary<Point, int> dists, string[] lines)
    {
        if (current.Equals(start) && dists.Any())
        {
            return;
        }

        if (!dists.ContainsKey(current) || current_dist < dists[current])
        {
            dists[current] = current_dist;
        }

        if (current_dist > dists[current])
        {
            return;
        }

        char current_ch = lines[current.Y][current.X];
        char ch;

        if (current.Y - 1 >= 0 && "S|LJ".Contains(current_ch))
        {
            ch = lines[current.Y - 1][current.X];
            if (ch == '|' || ch == 'F' || ch == '7')
            {
                VisitPart1(start, current.Move(0, -1), current_dist + 1, dists, lines);
            }
        }
        if (current.X < lines[0].Length - 1 && "S-FL".Contains(current_ch))
        {
            ch = lines[current.Y][current.X + 1];
            if (ch == '-' || ch == 'J' || ch == '7')
            {
                VisitPart1(start, current.Move(1, 0), current_dist + 1, dists, lines);
            }
        }
        if (current.Y < lines.Length - 1 && "S|F7".Contains(current_ch))
        {
            ch = lines[current.Y + 1][current.X];
            if (ch == '|' || ch == 'L' || ch == 'J')
            {
                VisitPart1(start, current.Move(0, 1), current_dist + 1, dists, lines);
            }
        }
        if (current.X > 0 && "S-7J".Contains(current_ch))
        {
            ch = lines[current.Y][current.X - 1];
            if (ch == '-' || ch == 'F' || ch == 'L')
            {
                VisitPart1(start, current.Move(-1, 0), current_dist + 1, dists, lines);
            }
        }
    }

    private static void SolvePart2(string[] lines)
    {
        int sx = -1;
        int sy = -1;
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            int pos = line.IndexOf('S');
            if (pos >= 0)
            {
                sx = pos;
                sy = y;
                break;
            }
        }

        List<Point> visitedPoints = new();
        VisitPart2(new Point(sx, sy), new Point(sx, sy), 0, lines, visitedPoints);
        visitedPoints.Add(new Point(sx, sy));
        int total = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                Point pos = new(x, y);
                if (IsInside(pos, visitedPoints, lines))
                {
                    total++;
                }
            }
        }
        Console.WriteLine($"Part 2: {total}");
    }

    private static bool IsInside(Point pos, List<Point> visitedPoints, string[] lines)
    {
        Box bb = new(new Point(0, 0), new Point(lines[0].Length - 1, lines.Length - 1));
        if (visitedPoints.Contains(pos))
        {
            return false;
        }

        if (GetNrCrosses(pos, visitedPoints, lines) % 2 == 1)
        {
            return true;
        }

        return false;
    }

    private static int GetNrCrosses(Point pos, List<Point> visitedPoints, string[] lines)
    {
        int nrCrosses = 0;

        pos = pos.Move(1, 0);
        while (pos.X >= 0 && pos.X < lines[0].Length && pos.Y >= 0 && pos.Y < lines[0].Length)
        {
            int index = visitedPoints.IndexOf(pos);
            if (index >= 0)
            {
                int i = (index - 1 + visitedPoints.Count) % visitedPoints.Count;
                while (visitedPoints[i].Y == pos.Y)
                {
                    i--;
                    if (i < 0)
                    {
                        i = visitedPoints.Count - 1;
                    }
                }
                int y1 = visitedPoints[i].Y;
                int x1 = visitedPoints[i].X;

                i = (index + 1) % visitedPoints.Count;
                while (visitedPoints[i].Y == pos.Y)
                {
                    i++;
                    if (i == visitedPoints.Count)
                    {
                        i = 0;
                    }
                }
                int y2 = visitedPoints[i].Y;
                int x2 = visitedPoints[i].X;

                if (y1 != y2)
                {
                    nrCrosses++;
                    pos = new Point(Math.Max(x1, x2), pos.Y);
                }
            }
            pos = pos.Move(1, 0);
        }

        return nrCrosses;
    }

    private static void VisitPart2(Point start, Point current, int current_dist, string[] lines, List<Point> visitedPoints)
    {
        if (current.Equals(start) && visitedPoints.Any())
        {
            return;
        }

        if (visitedPoints.Contains(current))
        {
            return;
        }

        visitedPoints.Add(current);

        char current_ch = lines[current.Y][current.X];
        char ch;

        if (current.Y - 1 >= 0 && "S|LJ".Contains(current_ch))
        {
            ch = lines[current.Y - 1][current.X];
            if (ch == '|' || ch == 'F' || ch == '7')
            {
                VisitPart2(start, current.Move(0, -1), current_dist + 1, lines, visitedPoints);
            }
        }
        if (current.X < lines[0].Length - 1 && "S-FL".Contains(current_ch))
        {
            ch = lines[current.Y][current.X + 1];
            if (ch == '-' || ch == 'J' || ch == '7')
            {
                VisitPart2(start, current.Move(1, 0), current_dist + 1, lines, visitedPoints);
            }
        }
        if (current.Y < lines.Length - 1 && "S|F7".Contains(current_ch))
        {
            ch = lines[current.Y + 1][current.X];
            if (ch == '|' || ch == 'L' || ch == 'J')
            {
                VisitPart2(start, current.Move(0, 1), current_dist + 1, lines, visitedPoints);
            }
        }
        if (current.X > 0 && "S-7J".Contains(current_ch))
        {
            ch = lines[current.Y][current.X - 1];
            if (ch == '-' || ch == 'F' || ch == 'L')
            {
                VisitPart2(start, current.Move(-1, 0), current_dist + 1, lines, visitedPoints);
            }
        }
    }
}
