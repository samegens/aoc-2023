using AoC;

namespace Solver.Tests;

public class RaceTests
{
    [TestCase(7, 9, 4)]
    [TestCase(15, 40, 8)]
    public void TestGetNrWaysToBeatRecord(long time, long distance, int expectedNrWays)
    {
        // Arrange
        Race race = new(time, distance);

        // Act
        int result = race.GetNrWaysToBeatRecord();

        // Assert
        Assert.That(result, Is.EqualTo(expectedNrWays));
    }
}