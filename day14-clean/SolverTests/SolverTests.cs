namespace SolverTests;

public class SolverTests
{
    private static readonly string[] Lines = @"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....".Split('\n');
    private Solver _sut = new(Lines);

    [Test]
    public void TestSolvePart1()
    {
        // Arrange

        // Act
        int actual = _sut.SolvePart1();

        // Assert
        Assert.That(actual, Is.EqualTo(136));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange

        // Act
        int actual = _sut.SolvePart2();

        // Assert
        Assert.That(actual, Is.EqualTo(64));
    }
}