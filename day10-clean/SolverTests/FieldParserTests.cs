namespace SolverTests;

public class FieldParserTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        string[] lines = @"7-F7-
.FJ|7
SJLL7
|F--J
LJ.LJ".Split('\n');

        // Act
        Field result = FieldParser.Parse(lines);

        // Assert
        Assert.That(result.At(new Point(0, 0)), Is.EqualTo(Tile.SouthWest));
        Assert.That(result.At(new Point(0, 2)), Is.EqualTo(Tile.Start));
    }
}