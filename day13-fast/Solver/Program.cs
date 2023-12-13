namespace AoC;

public class Pattern
{
    public List<Point> RockPositions { get; set; } = new();

    public int Width { get; set; }
    public int Height { get; set; }

    public Pattern(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int GetVerticalMirrorPosition(int skipColumn = -1)
    {
        for (int mirrorColumn = 1; mirrorColumn < Width; mirrorColumn++)
        {
            if (mirrorColumn == skipColumn) continue;

            // column = 1 means mirror is located between column 0 and column 1.
            int nrColumnsLeftOfMirror = mirrorColumn;
            int nrColumnsRightOfMirror = Width - nrColumnsLeftOfMirror;
            int nrColumnsToInspect = Math.Min(nrColumnsLeftOfMirror, nrColumnsRightOfMirror);

            bool allColumnsMatch = true;
            for (int i = 0; i < nrColumnsToInspect; i++)
            {
                int leftColumn = mirrorColumn - i - 1;
                int rightColumn = mirrorColumn + i;

                IEnumerable<Point> pointsInLeftColumn = RockPositions.Where(p => p.X == leftColumn);
                IEnumerable<Point> pointsInRightColumn = RockPositions.Where(p => p.X == rightColumn);

                if (pointsInLeftColumn.Count() != pointsInRightColumn.Count())
                {
                    allColumnsMatch = false;
                    break;
                }

                int deltaX = i * 2 + 1;
                foreach (Point pL in pointsInLeftColumn)
                {
                    Point mirroredPoint = pL.Move(deltaX, 0);
                    if (!pointsInRightColumn.Contains(mirroredPoint))
                    {
                        allColumnsMatch = false;
                        break;
                    }
                }
            }

            if (allColumnsMatch) return mirrorColumn;
        }

        return -1;
    }

    public int GetHorizontalMirrorPosition(int skipRow = -1)
    {
        for (int mirrorRow = 1; mirrorRow < Height; mirrorRow++)
        {
            if (mirrorRow == skipRow) continue;
            // column = 1 means mirror is located between column 0 and column 1.
            int nrRowsAboveMirror = mirrorRow;
            int nrRowsBelowMirror = Height - nrRowsAboveMirror;
            int nrRowsToInspect = Math.Min(nrRowsAboveMirror, nrRowsBelowMirror);

            bool allRowsMatch = true;
            for (int i = 0; i < nrRowsToInspect; i++)
            {
                int topRow = mirrorRow - i - 1;
                int bottomRow = mirrorRow + i;

                IEnumerable<Point> pointsInTopRow = RockPositions.Where(p => p.Y == topRow);
                IEnumerable<Point> pointsInBottomRow = RockPositions.Where(p => p.Y == bottomRow);

                if (pointsInTopRow.Count() != pointsInBottomRow.Count())
                {
                    allRowsMatch = false;
                    break;
                }

                int deltaY = i * 2 + 1;
                foreach (Point pL in pointsInTopRow)
                {
                    Point mirroredPoint = pL.Move(0, deltaY);
                    if (!pointsInBottomRow.Contains(mirroredPoint))
                    {
                        allRowsMatch = false;
                        break;
                    }
                }
            }

            if (allRowsMatch) return mirrorRow;
        }

        return -1;
    }

    public void TogglePosition(Point pos)
    {
        if (RockPositions.Contains(pos))
        {
            RockPositions.Remove(pos);
        }
        else
        {
            RockPositions.Add(pos);
        }
    }

    public void Print()
    {
        Console.WriteLine($"width = {Width}, height = {Height}");
        RockPositions.ForEach(Console.WriteLine);
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
        int width = lines[0].Length;
        List<Pattern> patterns = new();
        Pattern pattern = new(width, 0);
        int total = 0;
        int height = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            if (string.IsNullOrEmpty(line))
            {
                pattern.Height = height;
                patterns.Add(pattern);
                pattern = new(width, 0);
                height = 0;
            }
            else
            {
                pattern.Width = line.Length;
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                        pattern.RockPositions.Add(new Point(x, height));
                }
                height++;
            }
        }
        if (pattern.RockPositions.Any())
        {
            pattern.Height = height;
            patterns.Add(pattern);
        }

        foreach (var p in patterns)
        {
            int vpos = p.GetVerticalMirrorPosition();
            if (vpos >= 0)
                total += vpos;

            int hpos = p.GetHorizontalMirrorPosition();
            if (hpos >= 0)
                total += 100 * hpos;

            if (hpos == -1 && vpos == -1)
                throw new Exception("WTF?");
            if (hpos != -1 && vpos != -1)
                throw new Exception("WTF?");
        }

        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        int width = lines[0].Length;
        List<Pattern> patterns = new();
        Pattern pattern = new(width, 0);
        int total = 0;
        int height = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            if (string.IsNullOrEmpty(line))
            {
                pattern.Height = height;
                patterns.Add(pattern);
                pattern = new(width, 0);
                height = 0;
            }
            else
            {
                pattern.Width = line.Length;
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '#')
                        pattern.RockPositions.Add(new Point(x, height));
                }
                height++;
            }
        }
        if (pattern.RockPositions.Any())
        {
            pattern.Height = height;
            patterns.Add(pattern);
        }

        foreach (var p in patterns)
        {
            int hpos = -1;
            int vpos = -1;
            int oldVpos = p.GetVerticalMirrorPosition();
            int oldHpos = p.GetHorizontalMirrorPosition();
            bool foundSmudge = false;
            for (int y = 0; y < p.Height; y++)
            {
                for (int x = 0; x < p.Width; x++)
                {
                    p.TogglePosition(new Point(x, y));
                    vpos = p.GetVerticalMirrorPosition(oldVpos);
                    hpos = p.GetHorizontalMirrorPosition(oldHpos);
                    p.TogglePosition(new Point(x, y));
                    if ((hpos != -1 && hpos != oldHpos) || (vpos != -1 && vpos != oldVpos))
                    {
                        foundSmudge = true;
                        break;
                    }
                }
                if (foundSmudge) break;
            }

            if (!foundSmudge)
            {
                throw new Exception("WTF?");
            }
            if (vpos >= 0 && vpos != oldVpos)
                total += vpos;

            if (hpos >= 0 && hpos != oldHpos)
                total += 100 * hpos;


            if (hpos == -1 && vpos == -1)
                throw new Exception("WTF?");
        }

        Console.WriteLine($"Part 2: {total}");
    }
}
