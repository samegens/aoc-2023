using AoC;

namespace Solver.Tests;

public class RacesParserTests
{
    [Test]
    public void TestRacesParserPart1()
    {
        // Arrange
        string text = @"Time:        44     82     69     81
Distance:   202   1076   1138   1458";

        // Act
        List<Race> result = RacesParserPart1.Parse(text.Split("\r\n")).ToList();

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result[0].Time, Is.EqualTo(44));
        Assert.That(result[3].Time, Is.EqualTo(81));
        Assert.That(result[0].Distance, Is.EqualTo(202));
        Assert.That(result[3].Distance, Is.EqualTo(1458));
    }

    [Test]
    public void TestRacesParserPart2()
    {
        // Arrange
        string text = @"Time:        44     82     69     81
Distance:   202   1076   1138   1458";

        // Act
        Race result = RacesParserPart2.Parse(text.Split("\r\n"));

        // Assert
        Assert.That(result.Time, Is.EqualTo(44826981L));
        Assert.That(result.Distance, Is.EqualTo(202107611381458L));
    }
}
