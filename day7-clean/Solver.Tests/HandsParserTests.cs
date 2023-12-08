namespace SolverTests;

public class HandsParserTests
{
    private readonly HandsParser _sut;

    public HandsParserTests()
    {
        _sut = new HandsParser();
    }

    [Test]
    public void TestParse()
    {
        // Arrange
        string[] lines = @"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483".Split("\r\n");

        // Act
        List<Hand> result = _sut.Parse(lines).ToList();

        // Assert
        Assert.That(result.Count, Is.EqualTo(5));
        Assert.That(result[0], Is.EqualTo(new Hand("32T3K", 765)));
    }
}