namespace Solver.Tests;

public class Tests
{
    [TestCase("1abc2", 12)]
    [TestCase("dssmtmrkonedbbhdhjbf9hq", 99)]
    public void TestParsePart1(string line, int expectedValue)
    {
        // Arrange
        LineParserPart1 parser = new();

        // Act
        int result = parser.Parse(line);

        // Assert
        Assert.That(result, Is.EqualTo(expectedValue));
    }

    [TestCase("1abc2", 12)]
    [TestCase("dssmtmrkonedbbhdhjbf9hq", 19)]
    public void TestParsePart2(string line, int expectedValue)
    {
        // Arrange
        LineParserPart2 parser = new();

        // Act
        int result = parser.Parse(line);

        // Assert
        Assert.That(result, Is.EqualTo(expectedValue));
    }
}