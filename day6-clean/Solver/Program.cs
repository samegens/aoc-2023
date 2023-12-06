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
        IEnumerable<Race> races = RacesParserPart1.Parse(lines);
        long total = races.Aggregate(1, (acc, race) => acc * race.GetNrWaysToBeatRecord());
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        Race race = RacesParserPart2.Parse(lines);
        long total = race.GetNrWaysToBeatRecord();
        Console.WriteLine($"Part 2: {total}");
    }
}
