namespace AoC;

class Brick
{
    public string Name;
    public Point3d P1;
    public Point3d P2;
    public List<Brick> Supporting = new();
    public List<Brick> SupportedBy = new();

    public Brick(string name, Point3d p1, Point3d p2)
    {
        Name = name;
        P1 = p1;
        P2 = p2;
    }

    public void Print()
    {
        Console.WriteLine($"{Name} {P1.X},{P1.Y},{P1.Z}-{P2.X},{P2.Y},{P2.Z}");
    }

    public int GetNrBlockThatWouldFallIfDisintegrated()
    {
        HashSet<Brick> visited = new();
        Queue<Brick> todo = new();
        todo.Enqueue(this);
        while (todo.Any())
        {
            Brick b = todo.Dequeue();
            if (!visited.Contains(b))
            {
                visited.Add(b);
                foreach (var supportedBrick in b.Supporting)
                {
                    bool willFall = true;
                    foreach (var supportingBrick in supportedBrick.SupportedBy)
                    {
                        if (!visited.Contains(supportingBrick))
                            willFall = false;
                    }
                    if (willFall)
                        todo.Enqueue(supportedBrick);
                }
            }
        }
        return visited.Count - 1;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Brick other = (Brick)obj;

        return other.P1.Equals(P1) && other.P2.Equals(P2);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(P1, P2);
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
        int total = 0;
        List<Brick> bricks = new();
        Brick plane = new("-", new Point3d(int.MinValue, int.MinValue, 0), new Point3d(int.MaxValue, int.MaxValue, 0));
        bricks.Add(plane);
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            Brick brick = ParseBrick(line, ((char)('A' + y)).ToString());
            if (brick.P2.Z < brick.P1.Z || brick.P2.Y < brick.P1.Y || brick.P2.X < brick.P1.X) throw new NotImplementedException();
            bricks.Add(brick);
        }

        bricks = bricks.OrderBy(b => b.P1.Z).ToList();
        for (int i = 1; i < bricks.Count; i++)
        {
            MoveDown(bricks, i);
        }

        ComputeSupports(bricks);

        for (int i = 1; i < bricks.Count; i++)
        {
            Brick brick = bricks[i];
            if (brick.Supporting.Count == 0)
            {
                total++;
            }
            else
            {
                bool canBeDisintegrated = true;
                foreach (var supportedBrick in brick.Supporting)
                {
                    if (supportedBrick.SupportedBy.Count < 2)
                        canBeDisintegrated = false;
                }
                if (canBeDisintegrated)
                    total++;
            }
        }
        Console.WriteLine($"Part 1: {total}");
    }

    private static void ComputeSupports(List<Brick> bricks)
    {
        for (int i = 1; i < bricks.Count; i++)
        {
            Brick thisBrick = bricks[i];
            for (int j = i + 1; j < bricks.Count; j++)
            {
                Brick other = bricks[j];
                if (other.P1.Z == thisBrick.P2.Z + 1)
                {
                    if (other.P1.X > thisBrick.P2.X ||
                        other.P1.Y > thisBrick.P2.Y ||
                        other.P2.X < thisBrick.P1.X ||
                        other.P2.Y < thisBrick.P1.Y)
                        continue;
                    thisBrick.Supporting.Add(other);
                    other.SupportedBy.Add(thisBrick);
                }
            }
        }
    }

    private static void MoveDown(List<Brick> bricks, int i)
    {
        Brick brick = bricks[i];
        while (!IsResting(bricks, i))
        {
            brick.P1.Z--;
            brick.P2.Z--;
        }
    }

    private static bool IsResting(List<Brick> bricks, int brickIndex)
    {
        Brick thisBrick = bricks[brickIndex];
        for (int i = 0; i < bricks.Count; i++)
        {
            if (i != brickIndex)
            {
                Brick other = bricks[i];
                if (other.P2.Z + 1 == thisBrick.P1.Z)
                {
                    if (other.P1.X > thisBrick.P2.X ||
                        other.P1.Y > thisBrick.P2.Y ||
                        other.P2.X < thisBrick.P1.X ||
                        other.P2.Y < thisBrick.P1.Y)
                        continue;
                    return true;
                }
            }
        }
        return false;
    }

    private static Brick ParseBrick(string line, string name)
    {
        var parts = line.Split('~');
        Point3d p1 = ParsePoint(parts[0]);
        Point3d p2 = ParsePoint(parts[1]);
        return new Brick(name, p1, p2);
    }

    private static Point3d ParsePoint(string v)
    {
        var parts = v.Split(',');
        return new Point3d(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
    }

    private static void SolvePart2(string[] lines)
    {
        int total = 0;
        List<Brick> bricks = new();
        Brick plane = new("-", new Point3d(int.MinValue, int.MinValue, 0), new Point3d(int.MaxValue, int.MaxValue, 0));
        bricks.Add(plane);
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            Brick brick = ParseBrick(line, ((char)('A' + y)).ToString());
            if (brick.P2.Z < brick.P1.Z || brick.P2.Y < brick.P1.Y || brick.P2.X < brick.P1.X) throw new NotImplementedException();
            bricks.Add(brick);
        }

        bricks = bricks.OrderBy(b => b.P1.Z).ToList();
        for (int i = 1; i < bricks.Count; i++)
        {
            MoveDown(bricks, i);
        }

        ComputeSupports(bricks);

        List<Brick> bricksToDisintegrate = new();
        for (int i = 1; i < bricks.Count; i++)
        {
            Brick brick = bricks[i];
            if (brick.Supporting.Count == 0)
            {
                // skip
            }
            else
            {
                bool canBeDisintegrated = true;
                foreach (var supportedBrick in brick.Supporting)
                {
                    if (supportedBrick.SupportedBy.Count < 2)
                        canBeDisintegrated = false;
                }
                if (!canBeDisintegrated)
                    bricksToDisintegrate.Add(brick);
            }
        }

        total = bricksToDisintegrate.Select(b => b.GetNrBlockThatWouldFallIfDisintegrated()).Sum();
        Console.WriteLine($"Part 2: {total}");
    }
}
