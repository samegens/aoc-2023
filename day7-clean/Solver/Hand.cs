namespace AoC;

public class Hand
{
    public class CardCount
    {
        public char Card { get; private set; }
        public int Count { get; private set; }

        public CardCount(char ch, int count)
        {
            Card = ch;
            Count = count;
        }
    }

    public enum HandType
    {
        FiveOfAKind,
        FourOfAKind,
        FullHouse,
        ThreeOfAKind,
        TwoPair,
        OnePair,
        HighCard
    }

    public Hand(string cards, int bid)
    {
        Cards = cards;
        Bid = bid;
    }

    public string Cards { get; }

    public int Bid { get; }

    /// <summary>
    /// Returns the character counts sorted by descending count, so character with the highest count
    /// is the first element in the list.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<CardCount> GetSortedCardCounts()
    {
        Dictionary<char, int> cardCounts = new();
        foreach (var ch in Cards)
        {
            if (!cardCounts.ContainsKey(ch))
            {
                cardCounts[ch] = 0;
            }
            cardCounts[ch]++;
        }

        return cardCounts
            .Select(kv => new CardCount(ch: kv.Key, count: kv.Value))
            .OrderByDescending(cc => cc.Count);
    }

    public override bool Equals(object? obj)
    {
        return obj is Hand other && Equals(other);
    }

    protected bool Equals(Hand other)
    {
        return Cards == other.Cards && Bid == other.Bid;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Cards, Bid);
    }
}
