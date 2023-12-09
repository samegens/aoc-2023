namespace SolverTests;

public class SolverTests
{
    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        string[] lines = @"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45".Split('\n');
        Solver solver = new();

        // Act
        long result = solver.SolvePart1(lines);

        // Assert
        Assert.That(result, Is.EqualTo(114));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        string[] lines = @"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45".Split('\n');
        Solver solver = new();

        // Act
        long result = solver.SolvePart2(lines);

        // Assert
        Assert.That(result, Is.EqualTo(2));
    }
}