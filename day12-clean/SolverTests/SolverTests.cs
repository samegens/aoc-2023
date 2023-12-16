namespace SolverTests;

public class SolverTests
{
    private static readonly string[] Lines = @"???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1".Split('\n');

    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        Solver solver = new(Lines);

        // Act
        long actual = solver.SolvePart1();

        // Assert
        Assert.That(actual, Is.EqualTo(21));
    }

    [Test]
    public void TestSolvePart2()
    {
        // Arrange
        Solver solver = new(Lines);

        // Act
        long actual = solver.SolvePart2();

        // Assert
        Assert.That(actual, Is.EqualTo(525152));
    }
}