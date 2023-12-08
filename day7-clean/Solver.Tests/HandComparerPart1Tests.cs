namespace SolverTests;

public class HandComparerPart1Tests
{
    private readonly HandComparerPart1 _sut = new();

    [TestCase("33333", Hand.HandType.FiveOfAKind)]
    [TestCase("33334", Hand.HandType.FourOfAKind)]
    [TestCase("33322", Hand.HandType.FullHouse)]
    [TestCase("33321", Hand.HandType.ThreeOfAKind)]
    [TestCase("33221", Hand.HandType.TwoPair)]
    [TestCase("32T3K", Hand.HandType.OnePair)]
    [TestCase("54321", Hand.HandType.HighCard)]
    public void TestClassify(string cards, Hand.HandType expectedType)
    {
        // Arrange
        Hand hand = new(cards, 42);

        // Act
        Hand.HandType result = _sut.Classify(hand);

        // Assert
        Assert.That(result, Is.EqualTo(expectedType));
    }

    [TestCase("55555", "54321", -1)]
    [TestCase("54321", "55555", 1)]
    [TestCase("54321", "54321", 0)]
    [TestCase("T55J5", "QQQJA", 1)]
    [TestCase("KTJJT", "QQQJA", 1)]
    public void TestCompare(string hand1Cards, string hand2Cards, int expectedResult)
    {
        // Arrange
        Hand hand1 = new(hand1Cards, 42);
        Hand hand2 = new(hand2Cards, 42);

        // Act
        int result = _sut.Compare(hand1, hand2);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void TestOrderBy()
    {
        // Arrange
        Hand hand1 = new("55555", 42);
        Hand hand2 = new("54321", 42);
        List<Hand> list = new() { hand1, hand2 };

        // Act
        List<Hand> result = list.OrderBy(h => h, _sut).ToList();

        // Assert
        Assert.That(result[0].Cards, Is.EqualTo("55555"));
    }
}
