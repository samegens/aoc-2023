namespace AoC;

public abstract class BaseHandComparer : IComparer<Hand>
{
    public int Compare(Hand? lhs, Hand? rhs)
    {
        if (lhs == null && rhs == null)
        {
            return 0;
        }
        if (lhs == null)
        {
            return -1;
        }
        if (rhs == null)
        {
            return 1;
        }

        Hand.HandType typeLhs = Classify(lhs);
        Hand.HandType typeRhs = Classify(rhs);

        if (typeLhs < typeRhs)
        {
            return -1;
        }
        if (typeLhs > typeRhs)
        {
            return 1;
        }

        return CompareCardValues(lhs, rhs);
    }

    public virtual Hand.HandType Classify(Hand hand)
    {
        return Classify(hand.GetSortedCardCounts());
    }

    protected static Hand.HandType Classify(IEnumerable<Hand.CardCount> sortedCardCounts)
    {
        var enumerator = sortedCardCounts.GetEnumerator();
        enumerator.MoveNext();
        int count = enumerator.Current.Count;
        switch (count)
        {
            case 5:
                return Hand.HandType.FiveOfAKind;
            case 4:
                return Hand.HandType.FourOfAKind;
            case 3:
                enumerator.MoveNext();
                count = enumerator.Current.Count;
                return count switch
                {
                    2 => Hand.HandType.FullHouse,
                    1 => Hand.HandType.ThreeOfAKind,
                    _ => throw new Exception("WTF?"),
                };
            case 2:
                enumerator.MoveNext();
                count = enumerator.Current.Count;
                return count switch
                {
                    2 => Hand.HandType.TwoPair,
                    1 => Hand.HandType.OnePair,
                    _ => throw new Exception("WTF?"),
                };
            case 1:
                return Hand.HandType.HighCard;
            default:
                throw new Exception("WTF?");
        }
    }

    public int CompareCardValues(Hand lhs, Hand rhs)
    {
        for (int i = 0; i < 5; i++)
        {
            string cardValues = GetCardValues();
            int lhsValue = cardValues.IndexOf(lhs.Cards[i]);
            int rhsValue = cardValues.IndexOf(rhs.Cards[i]);
            if (lhsValue < rhsValue)
            {
                return -1;
            }
            if (lhsValue > rhsValue)
            {
                return 1;
            }
        }
        return 0;
    }

    public abstract string GetCardValues();
}

public class HandComparerPart1 : BaseHandComparer
{
    public override string GetCardValues()
    {
        return "AKQJT98765432";
    }
}

public class HandComparerPart2 : BaseHandComparer
{
    public override Hand.HandType Classify(Hand hand)
    {
        List<Hand.CardCount> cardCounts = hand.GetSortedCardCounts().ToList();
        ApplyJokers(cardCounts);
        return Classify(cardCounts);
    }

    private static void ApplyJokers(List<Hand.CardCount> countsByCount)
    {
        int jokerIndex = -1;
        int firstNonJokerIndex = -1;
        for (int i = 0; i < countsByCount.Count; i++)
        {
            if (countsByCount[i].Card == 'J' && jokerIndex < 0)
            {
                jokerIndex = i;
            }
            if (countsByCount[i].Card != 'J' && firstNonJokerIndex < 0)
            {
                firstNonJokerIndex = i;
            }
        }
        if (jokerIndex >= 0 && firstNonJokerIndex >= 0)
        {
            countsByCount[firstNonJokerIndex] = new Hand.CardCount(
                countsByCount[firstNonJokerIndex].Card,
                countsByCount[firstNonJokerIndex].Count + countsByCount[jokerIndex].Count);
            countsByCount.RemoveAt(jokerIndex);
        }
    }

    public override string GetCardValues()
    {
        return "AKQT98765432J";
    }
}
