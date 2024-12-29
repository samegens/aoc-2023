public class Range
{
    public long Start { get; private set; }
    public long End { get; private set; }

    public Range(long start, long end)
    {
        Start = start;
        End = end;
    }

    public bool Contains(long value)
    {
        return value >= Start && value <= End;
    }

    public bool Contains(Range other)
    {
        return Start <= other.Start && End >= other.End;
    }

    public bool Overlaps(Range other)
    {
        return Start <= other.End && End >= other.Start;
    }

    public void Translate(long delta)
    {
        Start += delta;
        End += delta;
    }

    public IEnumerable<Range> SplitAt(long value)
    {
        if (value < Start || value > End)
        {
            throw new ArgumentException(nameof(value) + " is outside range");
        }
        if (End == Start)
        {
            throw new NotImplementedException();
        }
        if (value == Start)
        {
            throw new NotImplementedException();
        }
        yield return new Range(Start, value - 1);
        yield return new Range(value, End);
    }

    public IEnumerable<Range> SplitAt(long value1, long value2)
    {
        if (value1 < Start || value1 > End)
        {
            throw new ArgumentException(nameof(value1) + " is outside range");
        }
        if (value2 < Start || value2 > End)
        {
            throw new ArgumentException(nameof(value2) + " is outside range");
        }
        if (value1 == value2)
        {
            throw new ArgumentException(nameof(value2) + " is equal to " + nameof(value1));
        }
        if (End == Start)
        {
            throw new NotImplementedException();
        }
        if (value1 == Start || value2 == Start)
        {
            throw new NotImplementedException();
        }

        yield return new Range(Start, value1 - 1);
        yield return new Range(value1, value2 - 1);
        yield return new Range(value2, End - 1);
    }

    public void Invalidate()
    {
        End = long.MinValue;
    }

    public bool IsValid
    {
        get
        {
            return End >= Start;
        }
    }

    public void Print()
    {
        Console.WriteLine($"{Start}-{End}");
    }
}
