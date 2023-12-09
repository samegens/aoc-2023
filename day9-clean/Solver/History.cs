

namespace AoC;

public class History
{
    public History(IEnumerable<long> values)
    {
        Values = new List<long>(values);
    }

    public List<long> Values { get; private set; }

    public History Diffs => new(DiffValues);

    public IEnumerable<long> DiffValues
    {
        get
        {
            for (int i = 0; i < Values.Count - 1; i++)
            {
                yield return Values[i + 1] - Values[i];
            }
        }
    }

    public long First => Values.First();

    public long Last => Values.Last();

    public bool IsZeroDiff => Values.All(v => v == 0);

    public long ExtrapolateForward()
    {
        List<History> diffHistories = GenerateDiffHistoriesUntilZeroDiffs();
        return GetForwardExtrapolatedValueFromDiffHistories(diffHistories);
    }

    public long ExtrapolateBackward()
    {
        List<History> diffHistories = GenerateDiffHistoriesUntilZeroDiffs();
        return GetBackwardExtrapolatedValueFromDiffHistories(diffHistories);
    }

    private List<History> GenerateDiffHistoriesUntilZeroDiffs()
    {
        List<History> diffs = new()
        {
            Diffs
        };
        while (!diffs.Last().IsZeroDiff)
        {
            diffs.Add(diffs.Last().Diffs);
        }

        return diffs;
    }

    private long GetForwardExtrapolatedValueFromDiffHistories(List<History> diffHistories)
    {
        /*
        This is how this works:
        1   3   6  10  15  21  [28]
          2   3   4   5   6   [7]
            1   1   1   1   [1]
              0   0   0   0

        We're trying to find the [28] in this example. We get that number by summing
        all last numbers of the diff histories.
        */
        return diffHistories.Select(h => h.Last).Sum() + Last;
    }

    private long GetBackwardExtrapolatedValueFromDiffHistories(List<History> diffHistories)
    {
        /*
        This is how this works:
        [5]  10  13  16  21  30  45     -> original
          [5]   3   3   5   9  15       -> first diff
           [-2]   0   2   4   6         -> second diff
              [2]   2   2   2
                [0]   0   0
        Each backward extrapolated value [x] is computed by subtracting the extrapolated value
        of the next diff from the first value of the current diff.
        The final value is the first value of the history minus the extrapolated value of the
        first diff ('result').
        */
        long result = 0;
        for (int i = diffHistories.Count - 2; i >= 0; i--)
        {
            result = diffHistories[i].First - result;
        }
        return First - result;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        History other = (History)obj;

        return Values.SequenceEqual(other.Values);
    }

    public override int GetHashCode()
    {
        return Values.GetHashCode();
    }
}