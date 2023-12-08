namespace SolverTests;

public class HandTests
{
    [Test]
    public void TestEquals()
    {
        // Arrange
        Hand hand1 = new("ABCDE", 42);
        Hand hand2 = new("ABCDE", 42);
        Hand hand3 = new("ABCDE", 43);

        // Act & Assert
        Assert.That(hand1.Equals(hand1), Is.True);
        Assert.That(hand1.Equals(hand2), Is.True);
        Assert.That(hand2.Equals(hand1), Is.True);
        Assert.That(hand1.Equals(hand3), Is.False);
    }
}
