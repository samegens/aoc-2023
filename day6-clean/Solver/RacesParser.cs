
namespace AoC;

public static class RacesParserPart1
{
    public static IEnumerable<Race> Parse(string[] lines)
    {
        List<long> times = ParseNumbers(lines[0]);
        List<long> distances = ParseNumbers(lines[1]);

        for (int i = 0; i < times.Count; i++)
        {
            yield return new Race(time: times[i], distance: distances[i]);
        }
    }

    private static List<long> ParseNumbers(string line)
    {
        return line
            .Split(':')[1]
            .Trim()
            .Split(' ')
            .Where(s => !string.IsNullOrEmpty(s))
            .Select(long.Parse)
            .ToList();
    }
}

public static class RacesParserPart2
{
    public static Race Parse(string[] lines)
    {
        long time = ParseNumber(lines[0]);
        long distance = ParseNumber(lines[1]);

        return new Race(time: time, distance: distance);
    }

    private static long ParseNumber(string line)
    {
        string numberString = line
            .Split(':')[1]
            .Trim()
            .Replace(" ", "");
        return long.Parse(numberString);
    }
}
