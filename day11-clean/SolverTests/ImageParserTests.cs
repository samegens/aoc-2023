namespace SolverTests;

public class ImageParserTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        string[] lines = @"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....".Split('\n');

        // Act
        Image result = ImageParser.Parse(lines);

        // Assert
        Assert.That(result.Points.Count, Is.EqualTo(9));
        Assert.That(result.Points, Does.Contain(new PointL(0, 2)));
    }
}