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
        long total = Solve(lines, new AlmanacParserPart1());
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        long total = Solve(lines, new AlmanacParserPart2());
        Console.WriteLine($"Part 2: {total}");
    }

    private static long Solve(string[] lines, BaseAlmanacParser parser)
    {
        Almanac almanac = parser.Parse(lines);
        IEnumerable<Range> seedRanges = almanac.ApplySeedMapsToSeedRanges();
        return seedRanges
            .Select(r => r.Start)
            .Min();
    }
}
