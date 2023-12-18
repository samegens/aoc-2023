namespace AoC;

class DigInstruction
{
    public char Dir;
    public int NrTiles;

    public DigInstruction(char dir, int nrTiles)
    {
        Dir = dir;
        NrTiles = nrTiles;
    }

    public void Print()
    {
        Console.WriteLine($"{Dir} {NrTiles}");
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
        List<DigInstruction> instructions = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            instructions.Add(new DigInstruction(line.Split(' ')[0][0], int.Parse(line.Split(' ')[1])));
        }
        bool[,] tiles = new bool[1000, 1000];
        Point p = new(500, 500);
        tiles[p.X, p.Y] = true;
        int minX = 500, minY = 500, maxX = 500, maxY = 500;
        int nrDug = 1;
        foreach (var instruction in instructions)
        {
            Point delta = instruction.Dir switch
            {
                'U' => new Point(0, -1),
                'R' => new Point(1, 0),
                'D' => new Point(0, 1),
                'L' => new Point(-1, 0),
                _ => throw new NotImplementedException()
            };
            for (int i = 0; i < instruction.NrTiles; i++)
            {
                p = p.Move(delta);
                if (!tiles[p.X, p.Y]) nrDug++;
                tiles[p.X, p.Y] = true;
                minX = Math.Min(minX, p.X);
                minY = Math.Min(minY, p.Y);
                maxX = Math.Max(maxX, p.X);
                maxY = Math.Max(maxY, p.Y);
            }
        }

        p = new Point(minX + (maxX - minX) / 2, minY + (maxY - minY) / 2);
        nrDug += FloodFill(tiles, p);

        Console.WriteLine($"Part 1: {nrDug}");
    }

    private static int FloodFill(bool[,] tiles, Point p)
    {
        if (tiles[p.X, p.Y]) return 0;

        tiles[p.X, p.Y] = true;
        int nrDug = 1;

        nrDug += FloodFill(tiles, p.Move(0, -1));
        nrDug += FloodFill(tiles, p.Move(1, 0));
        nrDug += FloodFill(tiles, p.Move(0, 1));
        nrDug += FloodFill(tiles, p.Move(-1, 0));

        return nrDug;
    }

    private static void SolvePart2(string[] lines)
    {
        List<DigInstruction> instructions = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            string hex = line.Split('#')[1][..6];
            char dir = hex.Last();
            dir = dir switch
            {
                '0' => 'R',
                '1' => 'D',
                '2' => 'L',
                '3' => 'U',
                _ => throw new NotImplementedException()
            };
            hex = hex[..5];
            instructions.Add(new DigInstruction(dir, Convert.ToInt32(hex, 16)));
        }

        List<Point> poly = new();
        Point p = new(0, 0);
        poly.Add(p);
        long nrDug = 0;
        foreach (var inst in instructions)
        {
            Point delta = inst.Dir switch
            {
                'U' => new Point(0, -inst.NrTiles),
                'R' => new Point(inst.NrTiles, 0),
                'D' => new Point(0, inst.NrTiles),
                'L' => new Point(-inst.NrTiles, 0),
                _ => throw new NotImplementedException()
            };

            p = p.Move(delta);
            poly.Add(p);
            nrDug += inst.NrTiles;
        }

        long total = 0;

        // https://www.wikiwand.com/en/Shoelace_formula
        for (int i = 0; i < poly.Count - 1; i++)
        {
            var p1 = poly[i];
            var p2 = poly[i + 1];
            total += (long)p1.X * (long)p2.Y - (long)p1.Y * (long)p2.X;
        }

        total /= 2;

        // I found nrDug / 2 + 1 only by experimenting, can't properly explain why it works.
        Console.WriteLine($"Part 2: {total + nrDug / 2 + 1}");
    }
}
