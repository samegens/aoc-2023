class Schematic
{
    private readonly List<PartNr> _partNrs = new();
    private readonly int _height;
    private readonly int _width;
    private readonly string[] _lines;

    public Schematic(string[] lines)
    {
        _lines = lines;
        _height = lines.Length;
        _width = lines[0].Length;
        ParseLines(lines);
    }

    public char At(Point p)
    {
        return _lines[p.Y][p.X];
    }

    public bool IsSymbolAt(Point p)
    {
        char ch = At(p);
        return ch != '.' && !char.IsDigit(ch);
    }

    public bool IsValidPosition(Point p)
    {
        return p.Y >= 0 && p.Y < _height && p.X >= 0 && p.X < _width;
    }

    public IEnumerable<Point> GetPointsSurroundingPartNr(PartNr partNr)
    {
        for (int y = partNr.Position.Y - 1; y <= partNr.Position.Y + 1; y++)
        {
            for (int x = partNr.Position.X - 1; x <= partNr.Position.X + partNr.Length; x++)
            {
                Point p = new(x, y);
                if (IsValidPosition(p) && !partNr.ContainsPoint(p))
                {
                    yield return p;
                }
            }
        }
    }

    public IEnumerable<PartNr> GetPartNrsNextToSymbol()
    {
        foreach (PartNr partnr in _partNrs)
        {
            foreach (Point point in GetPointsSurroundingPartNr(partnr))
            {
                if (IsSymbolAt(point))
                {
                    yield return partnr;
                    break;
                }
            }
        }
    }

    public IEnumerable<Point> GetGearPositions()
    {
        for (int y = 0; y < _lines.Length; y++)
        {
            for (int x = 0; x < _lines[0].Length; x++)
            {
                if (_lines[y][x] == '*')
                {
                    yield return new Point(x, y);
                }
            }
        }
    }

    public IEnumerable<Gear> FindGears()
    {
        foreach (Point point in GetGearPositions())
        {
            List<PartNr> adjacentPartNrs = FindPartNrsAdjacentTo(point).ToList();
            if (adjacentPartNrs.Count == 2)
            {
                yield return new Gear(adjacentPartNrs);
            }
        }
    }

    private IEnumerable<PartNr> FindPartNrsAdjacentTo(Point point)
    {
        return _partNrs.Where(p => p.IsAdjacentTo(point));
    }

    private void ParseLines(string[] lines)
    {
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            _partNrs.AddRange(ParseLine(line, y));
        }
    }

    private static IEnumerable<PartNr> ParseLine(string line, int y)
    {
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
                yield return new PartNr(new Point(nrX, y), nr, nrLength);
                nr = 0;
                nrLength = 0;
                nrX = 0;
            }
        }

        // Don't forget the last number on the line!
        if (nr > 0)
        {
            yield return new PartNr(new Point(nrX, y), nr, nrLength);
        }
    }
}
