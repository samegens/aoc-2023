namespace AoC;

public class Solver
{
    public long SolvePart1(string[] lines)
    {
        IEnumerable<History> histories = HistoryParser.Parse(lines);
        return histories.Aggregate(0L, (acc, history) => acc += history.ExtrapolateForward());
    }

    public long SolvePart2(string[] lines)
    {
        IEnumerable<History> histories = HistoryParser.Parse(lines);
        return histories.Aggregate(0L, (acc, history) => acc += history.ExtrapolateBackward());
    }
}