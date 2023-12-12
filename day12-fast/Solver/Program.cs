namespace AoC;

public class ConditionRecord
{
    public string StringConditions { get; private set; }
    public List<int> GroupSizes { get; private set; }

    public ConditionRecord(string stringConditions, List<int> groupSizes)
    {
        StringConditions = stringConditions;
        GroupSizes = groupSizes;
    }
}

public class Program
{
    private static long[,] _cachedCounts = new long[0, 0];

    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    private static void SolvePart1(string[] lines)
    {
        long total = 0;
        List<ConditionRecord> records = new();
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            string cond = line.Split(' ')[0];
            var groups = line.Split(' ')[1].Split(',').Select(int.Parse).ToList();
            ConditionRecord record = new(cond, groups);
            total += GetNrArrangements(record);
        }
        Console.WriteLine($"Part 1: {total}");
    }

    public static long GetNrArrangements(ConditionRecord record)
    {
        _cachedCounts = new long[record.StringConditions.Length, record.GroupSizes.Count];
        for (int i = 0; i < record.StringConditions.Length; i++)
        {
            for (int j = 0; j < record.GroupSizes.Count; j++)
            {
                _cachedCounts[i, j] = -1;
            }
        }

        long total = Visit(record.StringConditions, 0, record, 0);
        return total;
    }

    private static long Visit(string line, int stringIndex, ConditionRecord record, int groupIndex)
    {
        if (stringIndex >= line.Length) return groupIndex == record.GroupSizes.Count ? 1 : 0;
        if (groupIndex == record.GroupSizes.Count) return line.Substring(stringIndex).Contains('#') ? 0 : 1;
        if (_cachedCounts[stringIndex, groupIndex] != -1)
        {
            return _cachedCounts[stringIndex, groupIndex];
        }

        switch (line[stringIndex])
        {
            case '.': return Visit(line, stringIndex + 1, record, groupIndex);
            case '#':
                if (!CanFitGroup(line, stringIndex, record.GroupSizes[groupIndex]))
                {
                    _cachedCounts[stringIndex, groupIndex] = 0;
                    return 0;
                }
                return Visit(line, stringIndex + record.GroupSizes[groupIndex] + 1, record, groupIndex + 1);
            case '?':
                long nrArrangements = 0;
                // First try to substitute '#'.
                if (groupIndex < record.GroupSizes.Count && CanFitGroup(line, stringIndex, record.GroupSizes[groupIndex]))
                {
                    nrArrangements += Visit(line, stringIndex + record.GroupSizes[groupIndex] + 1, record, groupIndex + 1);
                }
                // And then '.'.
                nrArrangements += Visit(line, stringIndex + 1, record, groupIndex);
                _cachedCounts[stringIndex, groupIndex] = nrArrangements;
                return nrArrangements;
            default:
                throw new Exception($"Unknown char {line[stringIndex]}");
        }
    }

    private static bool CanFitGroup(string line, int stringIndex, int groupSize)
    {
        if (stringIndex + groupSize > line.Length) return false;

        for (int i = 0; i <= groupSize; i++)
        {
            if (i == groupSize)
            {
                return stringIndex + i == line.Length || "?.".Contains(line[stringIndex + i]);
            }

            if (!"?#".Contains(line[stringIndex + i]))
            {
                return false;
            }
        }

        throw new Exception("WTF?");
    }

    private static void SolvePart2(string[] lines)
    {
        long total = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];

            ConditionRecord record = Parse(line);
            total += GetNrArrangements(record);
        }
        Console.WriteLine($"Part 2: {total}");
    }

    public static ConditionRecord Parse(string line)
    {
        string cond = line.Split(' ')[0];
        cond = string.Join('?', Enumerable.Repeat(cond, 5));
        var groups = line.Split(' ')[1].Split(',').Select(int.Parse).ToList();
        groups = Enumerable.Repeat(groups, 5).SelectMany(x => x).ToList();

        return new(cond, groups);
    }
}
