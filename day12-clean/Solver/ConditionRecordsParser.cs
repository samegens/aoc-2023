
namespace AoC;

public static class ConditionRecordsParser
{
    public static IEnumerable<ConditionRecord> Parse(string[] lines)
    {
        return lines.Select(ParseLine);
    }

    public static ConditionRecord ParseLine(string line)
    {
        string[] parts = line.Split(' ');

        const int ConditionIndex = 0;
        const int GroupsIndex = 1;
        string conditions = parts[ConditionIndex];

        string groupsText = parts[GroupsIndex];
        IEnumerable<int> groups = groupsText
            .Split(',')
            .Select(int.Parse);

        return new(conditions, groups);
    }
}