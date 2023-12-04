class Program
{
    private static void Main(string[] args)
    {
        IEnumerable<Card> cards = File.ReadAllLines("input.txt").Select(Card.Parse);
        SolvePart1(cards);
        SolvePart2(cards);
    }

    private static void SolvePart1(IEnumerable<Card> cards)
    {
        int total = cards.Sum(c => c.Points);
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(IEnumerable<Card> cards)
    {
        Dictionary<int, int> cardCounts = GetCardCounts(cards);

        IncreaseCardCountsByWins(cardCounts, cards);

        int total = cardCounts.Sum(kv => kv.Value);
        Console.WriteLine($"Part 1: {total}");
    }

    private static void IncreaseCardCountsByWins(Dictionary<int, int> cardCounts, IEnumerable<Card> cards)
    {
        foreach (Card card in cards)
        {
            for (int i = 1; i <= card.NrMatches; i++)
            {
                int cardIdOfCopy = card.Id + i;
                cardCounts[cardIdOfCopy] += cardCounts[card.Id];
            }
        }
    }

    private static Dictionary<int, int> GetCardCounts(IEnumerable<Card> cards)
    {
        Dictionary<int, int> cardCount = new();
        foreach (Card card in cards)
        {
            // We start with one copy of each card.
            cardCount[card.Id] = 1;
        }

        return cardCount;
    }
}