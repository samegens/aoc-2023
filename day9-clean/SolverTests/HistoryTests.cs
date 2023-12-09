namespace SolverTests;

public class HistoryTests
{
    [TestCase(new long[] { 1L, 2L, 3L }, new long[] { 1L, 2L, 3L }, true)]
    [TestCase(new long[] { 1L, 2L, 3L }, new long[] { 1L, 2L, 2L }, false)]
    [TestCase(new long[] { 1L, 2L, 3L }, new long[] { 3L, 2L, 1L }, false)]
    [TestCase(new long[] { 1L, 2L, 3L }, new long[] { 1L, 2L, 3L, 3L }, false)]
    public void TestEquals(long[] h1Values, long[] h2Values, bool expectedResult)
    {
        // Arrange
        History history1 = new(h1Values);
        History history2 = new(h2Values);

        // Act
        bool result = history1.Equals(history2);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void TestDiffs()
    {
        // Arrange
        History history = new(new List<long>() { 1, 2, 4, 8 });
        History expectedResult = new(new List<long>() { 1, 2, 4 });

        // Act
        History result = history.Diffs;

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase(1, 2, 3, false)]
    [TestCase(1, 1, 1, false)]
    [TestCase(0, 0, 0, true)]
    public void TestIsZeroDiff(long v1, long v2, long v3, bool expectedResult)
    {
        // Arrange
        History history = new(new List<long>() { v1, v2, v3 });

        // Act
        bool result = history.IsZeroDiff;

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase(new long[] { 0, 3, 6, 9, 12, 15 }, 18)]
    [TestCase(new long[] { 1, 3, 6, 10, 15, 21 }, 28)]
    public void TestExtrapolateForward(long[] values, long expectedResult)
    {
        // Arrange
        History history = new(values);

        // Act
        long result = history.ExtrapolateForward();

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase(new long[] { 0, 3, 6, 9, 12, 15 }, -3)]
    [TestCase(new long[] { 1, 3, 6, 10, 15, 21 }, 0)]
    [TestCase(new long[] { 10, 13, 16, 21, 30, 45 }, 5)]
    public void TestExtrapolateBackward(long[] values, long expectedResult)
    {
        // Arrange
        History history = new(values);

        // Act
        long result = history.ExtrapolateBackward();

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}