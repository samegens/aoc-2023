namespace AoC;

public class ConditionRecord
{
    public const char DamagedSpring = '#';
    public const char OperationalSpring = '.';
    public const char UnknownSpring = '?';

    public string SpringConditions { get; private set; }

    // Size of each group of broken springs.
    public List<int> GroupSizes { get; private set; }

    // To greatly speed up traversing the search space, we take advantage of the fact
    // that when analysing char i when j groups have been matched, the number of
    // arrangements for the rest of the conditions string will be the same, no
    // matter where in the search space this combination occurs.
    private long[,] _cachedCounts = new long[0, 0];

    public ConditionRecord(string stringConditions, IEnumerable<int> groupSizes)
    {
        SpringConditions = stringConditions;
        GroupSizes = new List<int>(groupSizes);

        _cachedCounts = new long[SpringConditions.Length, GroupSizes.Count];
        for (int i = 0; i < SpringConditions.Length; i++)
        {
            for (int j = 0; j < GroupSizes.Count; j++)
            {
                _cachedCounts[i, j] = -1;
            }
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        ConditionRecord other = (ConditionRecord)obj;
        return other.SpringConditions == SpringConditions &&
               other.GroupSizes.SequenceEqual(GroupSizes);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(SpringConditions);

        foreach (int size in GroupSizes)
        {
            hashCode.Add(size);
        }

        return hashCode.ToHashCode();
    }

    public long GetNrArrangements()
    {
        long total = Visit(0, 0);
        return total;
    }

    private long Visit(int stringIndex, int groupIndex)
    {
        if (stringIndex >= SpringConditions.Length)
        {
            // We've reached the end of the line.
            // If we mapped every group, we found a valid arrangement.
            return groupIndex == GroupSizes.Count ? 1 : 0;
        }

        if (groupIndex == GroupSizes.Count)
        {
            // We've mapped all groups.
            // If there are any broken springs left, we know it's not a valid arrangement.
            return SpringConditions[stringIndex..].Contains(DamagedSpring) ? 0 : 1;
        }

        // If we've already computed the number of arrangements of the combination
        // of the current string index and group index, we can use that one
        // because the result will be exactly the same.
        if (_cachedCounts[stringIndex, groupIndex] != -1)
        {
            return _cachedCounts[stringIndex, groupIndex];
        }

        switch (SpringConditions[stringIndex])
        {
            case OperationalSpring:
                return Visit(stringIndex + 1, groupIndex);
            case DamagedSpring:
                if (!CanFitGroup(stringIndex, groupIndex))
                {
                    _cachedCounts[stringIndex, groupIndex] = 0;
                    return 0;
                }
                return Visit(stringIndex + GroupSizes[groupIndex] + 1, groupIndex + 1);
            case UnknownSpring:
                long nrArrangements = 0;
                // First try to substitute '#'.
                if (groupIndex < GroupSizes.Count && CanFitGroup(stringIndex, groupIndex))
                {
                    nrArrangements += Visit(stringIndex + GroupSizes[groupIndex] + 1, groupIndex + 1);
                }
                // And then '.'.
                nrArrangements += Visit(stringIndex + 1, groupIndex);
                _cachedCounts[stringIndex, groupIndex] = nrArrangements;
                return nrArrangements;
            default:
                throw new Exception($"Unknown char {SpringConditions[stringIndex]}");
        }
    }

    private bool CanFitGroup(int stringIndex, int groupIndex)
    {
        int groupSize = GroupSizes[groupIndex];
        if (stringIndex + groupSize > SpringConditions.Length)
        {
            return false;
        }

        for (int i = 0; i <= groupSize; i++)
        {
            if (i == groupSize)
            {
                return stringIndex + i == SpringConditions.Length ||
                       SpringConditions[stringIndex + i] != DamagedSpring;
            }

            if (SpringConditions[stringIndex + i] != DamagedSpring &&
                SpringConditions[stringIndex + i] != UnknownSpring)
            {
                return false;
            }
        }

        throw new Exception("coding error");
    }
}