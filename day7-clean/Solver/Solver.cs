namespace AoC;

public class Solver
{
    public static int SolvePart1(string[] lines)
    {
        return Solve(lines, new HandComparerPart1());
    }

    public static int SolvePart2(string[] lines)
    {
        return Solve(lines, new HandComparerPart2());
    }

    public static int Solve(string[] lines, IComparer<Hand> comparer)
    {
        IEnumerable<Hand> hands = new HandsParser().Parse(lines);
        IEnumerable<Hand> sortedHands = hands.OrderByDescending(h => h, comparer);
        int rank = 1;
        int total = 0;
        foreach (Hand hand in sortedHands)
        {
            total += rank * hand.Bid;
            rank++;
        }
        return total;
    }
}