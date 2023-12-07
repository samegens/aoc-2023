namespace AoC;

public class Hand : IComparable
{
    public List<char> Cards { get; private set; }
    public int Bid { get; private set; }

    public Hand(IEnumerable<char> cards, int bid)
    {
        Cards = new(cards);
        Bid = bid;
    }

    public int Strength
    {
        get
        {
            Dictionary<char, int> counts = new();
            foreach (var ch in Cards)
            {
                if (!counts.ContainsKey(ch))
                {
                    counts[ch] = 0;
                }
                counts[ch]++;
            }

            var countsByCount = counts.OrderByDescending(kv => kv.Value).ToList();
            if (countsByCount[0].Value == 5)
            {
                return 7;
            }
            if (countsByCount[0].Value == 4)
            {
                return 6;
            }
            if (countsByCount[0].Value == 3 && countsByCount[1].Value == 2)
            {
                return 5;
            }
            if (countsByCount[0].Value == 3 && countsByCount[1].Value == 1 && countsByCount[2].Value == 1)
            {
                return 4;
            }
            if (countsByCount[0].Value == 2 && countsByCount[1].Value == 2)
            {
                return 3;
            }
            if (countsByCount[0].Value == 2 && countsByCount[1].Value == 1 && countsByCount[2].Value == 1 && countsByCount[3].Value == 1)
            {
                return 2;
            }
            return 1;
        }
    }

    public int CompareTo(object? obj)
    {
        if (obj is not Hand || obj == null)
        {
            throw new NotImplementedException();
        }

        Hand other = (Hand)obj;
        if (other.Strength > Strength)
        {
            return -1;
        }
        if (other.Strength < Strength)
        {
            return 1;
        }

        for (int i = 0; i < 5; i++)
        {
            const string cardValues = "AKQJT98765432";
            int myValue = cardValues.IndexOf(Cards[i]);
            int otherValue = cardValues.IndexOf(other.Cards[i]);
            if (otherValue > myValue)
            {
                return 1;
            }
            if (otherValue < myValue)
            {
                return -1;
            }
        }
        return 0;
    }
}

public class Hand2 : IComparable
{
    public List<char> Cards { get; private set; }
    public int Bid { get; private set; }

    public Hand2(IEnumerable<char> cards, int bid)
    {
        Cards = new(cards);
        Bid = bid;
    }

    public int Strength
    {
        get
        {
            Dictionary<char, int> counts = new();
            foreach (var ch in Cards)
            {
                if (!counts.ContainsKey(ch))
                {
                    counts[ch] = 0;
                }
                counts[ch]++;
            }

            var countsByCount = counts.OrderByDescending(kv => kv.Value).ToList();
            int jIndex = -1;
            int firstNonJIndex = -1;
            for (int i = 0; i < countsByCount.Count; i++)
            {
                if (countsByCount[i].Key == 'J' && jIndex < 0)
                {
                    jIndex = i;
                }
                if (countsByCount[i].Key != 'J' && firstNonJIndex < 0)
                {
                    firstNonJIndex = i;
                }
            }
            if (jIndex >= 0 && firstNonJIndex >= 0)
            {
                countsByCount[firstNonJIndex] = new KeyValuePair<char, int>(
                    countsByCount[firstNonJIndex].Key,
                    countsByCount[firstNonJIndex].Value + countsByCount[jIndex].Value);
                countsByCount.RemoveAt(jIndex);
            }
            if (countsByCount[0].Value == 5)
            {
                return 7;
            }
            if (countsByCount[0].Value == 4)
            {
                return 6;
            }
            if (countsByCount[0].Value == 3 && countsByCount[1].Value == 2)
            {
                return 5;
            }
            if (countsByCount[0].Value == 3 && countsByCount[1].Value == 1 && countsByCount[2].Value == 1)
            {
                return 4;
            }
            if (countsByCount[0].Value == 2 && countsByCount[1].Value == 2)
            {
                return 3;
            }
            if (countsByCount[0].Value == 2 && countsByCount[1].Value == 1 && countsByCount[2].Value == 1 && countsByCount[3].Value == 1)
            {
                return 2;
            }
            return 1;
        }
    }

    public int CompareTo(object? obj)
    {
        if (obj is not Hand2 || obj == null)
        {
            throw new NotImplementedException();
        }

        Hand2 other = (Hand2)obj;
        if (other.Strength > Strength)
        {
            return -1;
        }
        if (other.Strength < Strength)
        {
            return 1;
        }

        for (int i = 0; i < 5; i++)
        {
            const string cardValues = "AKQT98765432J";
            int myValue = cardValues.IndexOf(Cards[i]);
            int otherValue = cardValues.IndexOf(other.Cards[i]);
            if (otherValue > myValue)
            {
                return 1;
            }
            if (otherValue < myValue)
            {
                return -1;
            }
        }
        return 0;
    }
}

public class Program
{
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");
        SolvePart1(lines);
        SolvePart2(lines);
    }

    private static void SolvePart1(string[] lines)
    {
        long total = 0;
        List<Hand> hands = lines.Select(l => new Hand(l.Split(' ')[0], int.Parse(l.Split(' ')[1]))).ToList();
        hands.Sort();
        for (int i = 0; i < hands.Count; i++)
        {
            Hand hand = hands[i];
            total += hand.Bid * (i + 1);
        }
        Console.WriteLine($"Part 1: {total}");
    }

    private static void SolvePart2(string[] lines)
    {
        long total = 0;
        List<Hand2> hands = lines.Select(l => new Hand2(l.Split(' ')[0], int.Parse(l.Split(' ')[1]))).ToList();
        hands.Sort();
        for (int i = 0; i < hands.Count; i++)
        {
            Hand2 hand = hands[i];
            total += hand.Bid * (i + 1);
        }
        Console.WriteLine($"Part 2: {total}");
    }
}
