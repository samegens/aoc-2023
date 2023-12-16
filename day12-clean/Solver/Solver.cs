
namespace AoC;

public class Solver
{
    private readonly string[] _lines;

    public Solver(string[] lines)
    {
        _lines = lines;
    }

    public long SolvePart1()
    {
        return _lines
            .Select(ConditionRecordsParser.ParseLine)
            .Select(r => r.GetNrArrangements())
            .Sum();
    }

    public long SolvePart2()
    {
        return _lines
            .Select(ConditionRecordsParser.ParseLine)
            .Select(Expand)
            .Select(r => r.GetNrArrangements())
            .Sum();
    }

    private ConditionRecord Expand(ConditionRecord conditionRecord)
    {
        const int NrCopies = 5;
        string newSpringConditions = string.Join(ConditionRecord.UnknownSpring, Enumerable.Repeat(conditionRecord.SpringConditions, NrCopies));
        IEnumerable<int> newGroupSizes = Enumerable.Repeat(conditionRecord.GroupSizes, NrCopies)
            .SelectMany(x => x);
        return new ConditionRecord(newSpringConditions, newGroupSizes);
    }
}