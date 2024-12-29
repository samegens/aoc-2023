namespace AoC;

public static class Algorithms
{
    // Adopted from https://stackoverflow.com/a/29717490
    public static long LCM(IEnumerable<long> numbers)
    {
        return numbers.Aggregate(LCM);
    }

    public static long LCM(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }

    public static long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }

}