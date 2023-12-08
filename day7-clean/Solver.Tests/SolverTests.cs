namespace SolverTests;

public class SolverTests
{
    static readonly string[] _lines = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483".Split('\n');

    [Test]
    public void TestSolvePart1()
    {
        // Arrange

        // Act
        int result = Solver.SolvePart1(_lines);

        // Assert
        Assert.That(result, Is.EqualTo(6440));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange

        // Act
        int result = Solver.SolvePart2(_lines);

        // Assert
        Assert.That(result, Is.EqualTo(5905));
    }
}