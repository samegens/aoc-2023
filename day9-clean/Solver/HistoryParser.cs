namespace AoC;

public static class HistoryParser
{
    public static IEnumerable<History> Parse(string[] lines)
    {
        return lines.Select(ParseHistory);
    }

    private static History ParseHistory(string line)
    {
        IEnumerable<long> values = line
            .Split(' ')
            .Select(long.Parse);
        return new History(values);
    }
}