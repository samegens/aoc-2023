namespace SolverTests;

public class HistoryParserTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        string[] lines = @"0 3 6
1 3 6".Split('\n');
        List<History> expectedResult = new()
        {
            new History(new long[] { 0, 3, 6 }),
            new History(new long[] { 1, 3, 6 }),
        };

        // Act
        List<History> result = HistoryParser.Parse(lines).ToList();

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}