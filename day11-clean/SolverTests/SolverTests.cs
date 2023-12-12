namespace SolverTests;

public class SolverTests
{
    private static readonly string[] Lines = @"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....".Split('\n');

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        Solver solver = new(Lines);

        // Act
        long result = solver.SolvePart1();

        // Assert
        Assert.That(result, Is.EqualTo(374));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        Solver solver = new(Lines);

        // Act
        long result = solver.SolvePart2();

        // Assert
        Assert.That(result, Is.EqualTo(82000210));
    }
}