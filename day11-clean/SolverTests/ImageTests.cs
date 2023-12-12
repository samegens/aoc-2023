namespace SolverTests;

public class ImageTests
{
    private Image _sut;

    public ImageTests()
    {
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
        _sut = ImageParser.Parse(lines);
    }

    [Test]
    public void TestEmptyRows()
    {
        // Arrange

        // Act
        List<long> result = _sut.EmptyRows;

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public void TestEmptyColumns()
    {
        // Arrange

        // Act
        List<long> result = _sut.EmptyColumns;

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
    }

    [Test]
    public void TestExpanded()
    {
        // Arrange

        // Act
        Image result = _sut.Expanded(2);

        // Assert
        Assert.That(result.Points.Count, Is.EqualTo(9));
        Assert.That(result.Points, Does.Contain(new PointL(0, 2)));
        Assert.That(result.Points, Does.Contain(new PointL(12, 7)));
    }
}