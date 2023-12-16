namespace SolverTests;

public class PlatformTests
{
    private static readonly string[] BaseLines = @"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....".Split('\n');

    [Test]
    public void TestTiltedNorth()
    {
        // Arrange
        string[] lines = @"OOOO.#.O..
OO..#....#
OO..O##..O
O..#.OO...
........#.
..#....#.#
..O..#.O.O
..O.......
#....###..
#....#....".Split('\n');
        Platform sut = PlatformParser.Parse(BaseLines);
        Platform expected = PlatformParser.Parse(lines);

        // Act
        Platform actual = sut.TiltedNorth();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestTiltedEast()
    {
        // Arrange
        string[] lines = @"....O#....
.OOO#....#
.....##...
.OO#....OO
......OO#.
.O#...O#.#
....O#..OO
.........O
#....###..
#..OO#....".Split('\n');
        Platform sut = PlatformParser.Parse(BaseLines);
        Platform expected = PlatformParser.Parse(lines);

        // Act
        Platform actual = sut.TiltedEast();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestTiltedSouth()
    {
        // Arrange
        string[] lines = @".....#....
....#....#
...O.##...
...#......
O.O....O#O
O.#..O.#.#
O....#....
OO....OO..
#OO..###..
#OO.O#...O".Split('\n');
        Platform sut = PlatformParser.Parse(BaseLines);
        Platform expected = PlatformParser.Parse(lines);

        // Act
        Platform actual = sut.TiltedSouth();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestTiltedWest()
    {
        // Arrange
        string[] lines = @"O....#....
OOO.#....#
.....##...
OO.#OO....
OO......#.
O.#O...#.#
O....#OO..
O.........
#....###..
#OO..#....".Split('\n');
        Platform sut = PlatformParser.Parse(BaseLines);
        Platform expected = PlatformParser.Parse(lines);

        // Act
        Platform actual = sut.TiltedWest();

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestGetLoad()
    {
        // Arrange
        Platform sut = PlatformParser.Parse(BaseLines);

        // Act
        int actual = sut.GetLoad();
    }
}