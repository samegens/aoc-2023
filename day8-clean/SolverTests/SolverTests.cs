namespace SolverTests;

public class SolverTests
{
    private readonly Solver _sut = new();

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        string[] lines = @"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)".Split('\n');

        // Act
        int result = _sut.SolvePart1(lines);

        // Assert
        Assert.That(result, Is.EqualTo(6));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        string[] lines = @"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)".Split('\n');

        // Act
        long result = _sut.SolvePart2(lines);

        // Assert
        Assert.That(result, Is.EqualTo(6));
    }
}