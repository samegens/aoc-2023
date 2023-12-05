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
        LineParserPart1 parser = new();
        int total = lines
            .Select(parser.Parse)
            .Sum();
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        LineParserPart2 parser = new();
        int total = lines
            .Select(parser.Parse)
            .Sum();
        Console.WriteLine($"Part 2: {total}");
    }
}
