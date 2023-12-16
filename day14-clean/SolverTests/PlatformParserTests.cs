namespace SolverTests;

public class PlatformParserTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        string[] lines = @"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....".Split('\n');

        // Act
        Platform result = PlatformParser.Parse(lines);

        // Assert
        Assert.That(result.Width, Is.EqualTo(10));
        Assert.That(result.Height, Is.EqualTo(10));
        Assert.That(result[4, 3], Is.EqualTo('O'));
        Assert.That(result[5, 2], Is.EqualTo('#'));
        Assert.That(result[1, 0], Is.EqualTo('.'));
    }
}