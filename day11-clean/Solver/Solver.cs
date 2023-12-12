namespace AoC;

public class Solver
{
    public string[] Lines { get; }

    public Solver(string[] lines)
    {
        Lines = lines;
    }

    public long SolvePart1()
    {
        return Solve(factor: 2);
    }

    public long SolvePart2()
    {
        return Solve(factor: 1000000);
    }

    private long Solve(long factor)
    {
        Image image = ImageParser.Parse(Lines);
        image = image.Expanded(factor);
        return image.GetUniquePointPairs()
            .Select(t => t.Item1.GetManhattanDistanceTo(t.Item2))
            .Sum();
    }
}