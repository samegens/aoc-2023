
class PartNr
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Nr { get; set; }
    public int Length { get; set; }
}

internal class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    private static void SolvePart1(string[] lines)
    {
        int total = 0;
        List<PartNr> partnrs = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            partnrs.AddRange(ParseLine(line, y));
        }
        foreach (var partnr in partnrs)
        {
            bool isNextToSymbol = false;
            for (int y = partnr.Y - 1; y <= partnr.Y + 1; y++)
            {
                for (int x = partnr.X - 1; x <= partnr.X + partnr.Length; x++)
                {
                    if (y == partnr.Y && x >= partnr.X && x < partnr.X + partnr.Length)
                    {
                        continue;
                    }
                    if (y < lines.Length && y >= 0 && x < lines[0].Length && x >= 0)
                    {
                        char ch = lines[y][x];
                        if (ch != '.' && !char.IsDigit(ch))
                        {
                            isNextToSymbol = true;
                        }
                    }
                }
            }
            if (isNextToSymbol)
            {
                total += partnr.Nr;
            }
        }
        Console.WriteLine($"Part 1: {total}");
    }

    private static IEnumerable<PartNr> ParseLine(string line, int y)
    {
        List<PartNr> partnrs = new();
        int nr = 0;
        int nrLength = 0;
        int nrX = 0;
        for (int x = 0; x < line.Length; x++)
        {
            char ch = line[x];
            if (char.IsDigit(ch))
            {
                if (nr == 0)
                {
                    nrX = x;
                }
                nr = nr * 10 + ch - '0';
                nrLength++;
            }
            else if (nr > 0)
            {
                partnrs.Add(new PartNr() { X = nrX, Y = y, Nr = nr, Length = nrLength });
                nr = 0;
                nrLength = 0;
                nrX = 0;
            }
        }

        if (nr > 0)
        {
            partnrs.Add(new PartNr() { X = nrX, Y = y, Nr = nr, Length = nrLength });
        }

        return partnrs;
    }

    private static void SolvePart2(string[] lines)
    {
        int total = 0;
        List<PartNr> partnrs = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            partnrs.AddRange(ParseLine(line, y));
        }
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                if (lines[y][x] == '*')
                {
                    List<PartNr> adjacentPartNrs = new();
                    foreach (var p in partnrs)
                    {
                        if (x >= p.X - 1 && x <= p.X + p.Length && y >= p.Y - 1 && y <= p.Y + 1)
                        {
                            adjacentPartNrs.Add(p);
                        }
                    }
                    if (adjacentPartNrs.Count == 2)
                    {
                        total += adjacentPartNrs[0].Nr * adjacentPartNrs[1].Nr;
                    }
                }
            }
        }
        Console.WriteLine($"Part 2: {total}");
    }
}