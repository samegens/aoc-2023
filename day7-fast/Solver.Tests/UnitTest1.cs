namespace Solver.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Hand hand1 = new("KK677", 1);
        Hand hand2 = new("KTJJT", 2);

        int result = hand1.CompareTo(hand2);

        Assert.That(result, Is.EqualTo(1));
    }

    [TestCase("JJJTT", 7)]
    [TestCase("JJJJJ", 7)]
    [TestCase("AKQT9", 1)]
    public void Test2(string cards, int expectedStrength)
    {
        Hand2 hand = new(cards, 1);

        Assert.That(hand.Strength, Is.EqualTo(expectedStrength));
    }
}