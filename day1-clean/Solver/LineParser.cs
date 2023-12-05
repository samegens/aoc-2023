namespace AoC;

public abstract class BaseLineParser
{
    public int Parse(string line)
    {
        Dictionary<string, int> numberMap = GetNumberMap();
        int firstValue = FindFirstNumber(line, numberMap);
        int lastValue = FindLastNumber(line, numberMap);
        return firstValue * 10 + lastValue;
    }

    private static int FindFirstNumber(string line, Dictionary<string, int> numberMap)
    {
        int firstNumber = int.MinValue;
        int firstNumberPos = int.MaxValue;
        foreach (KeyValuePair<string, int> kv in numberMap)
        {
            int pos = line.IndexOf(kv.Key);
            if (pos >= 0 && pos < firstNumberPos)
            {
                firstNumber = kv.Value;
                firstNumberPos = pos;
            }
        }

        if (firstNumber == int.MinValue)
        {
            throw new Exception();
        }

        return firstNumber;
    }

    private static int FindLastNumber(string line, Dictionary<string, int> numberMap)
    {
        int firstNumber = int.MinValue;
        int firstNumberPos = int.MinValue;
        foreach (KeyValuePair<string, int> kv in numberMap)
        {
            int pos = line.LastIndexOf(kv.Key);
            if (pos >= 0 && pos > firstNumberPos)
            {
                firstNumber = kv.Value;
                firstNumberPos = pos;
            }
        }

        if (firstNumber == int.MinValue)
        {
            throw new Exception();
        }

        return firstNumber;
    }

    abstract protected Dictionary<string, int> GetNumberMap();
}

public class LineParserPart1 : BaseLineParser
{
    protected override Dictionary<string, int> GetNumberMap()
    {
        return new Dictionary<string, int>()
        {
            ["1"] = 1,
            ["2"] = 2,
            ["3"] = 3,
            ["4"] = 4,
            ["5"] = 5,
            ["6"] = 6,
            ["7"] = 7,
            ["8"] = 8,
            ["9"] = 9
        };
    }
}

public class LineParserPart2 : BaseLineParser
{
    protected override Dictionary<string, int> GetNumberMap()
    {
        return new Dictionary<string, int>()
        {
            ["1"] = 1,
            ["2"] = 2,
            ["3"] = 3,
            ["4"] = 4,
            ["5"] = 5,
            ["6"] = 6,
            ["7"] = 7,
            ["8"] = 8,
            ["9"] = 9,
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9,
        };
    }
}
