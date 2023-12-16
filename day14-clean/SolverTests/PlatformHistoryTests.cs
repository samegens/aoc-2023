namespace SolverTests;

public class PlatformHistoryTests
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

    [Test]
    public void TestFindLoopWithoutLoop()
    {
        // Arrange
        PlatformHistory platformHistory = new();
        platformHistory.Add(PlatformParser.Parse(Lines));

        // Act
        (int actualLoopStartIndex, int actualLoopEndIndex) = platformHistory.FindLoop();

        // Assert
        Assert.That(actualLoopStartIndex, Is.EqualTo(-1));
        Assert.That(actualLoopEndIndex, Is.EqualTo(-1));
    }

    [Test]
    public void TestFindLoopWithSmallLoop()
    {
        // Arrange
        PlatformHistory platformHistory = new();
        Platform platform = PlatformParser.Parse(Lines);
        platformHistory.Add(platform);
        platformHistory.Add(platform);

        // Act
        (int actualLoopStartIndex, int actualLoopEndIndex) = platformHistory.FindLoop();

        // Assert
        Assert.That(actualLoopStartIndex, Is.EqualTo(0));
        Assert.That(actualLoopEndIndex, Is.EqualTo(1));
    }
}