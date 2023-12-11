using AoC;

namespace SolverTests;

public class SolverTests
{
    [Test]
    public void TestSolvePart1()
    {
        // Arrange
        string[] lines = @"7-F7-
.FJ|7
SJLL7
|F--J
LJ.LJ".Split('\n');
        Solver solver = new();

        // Act
        int result = solver.SolvePart1(lines);

        // Assert
        Assert.That(result, Is.EqualTo(8));
    }

    [Test]
    public void TestSolvePart2Simple()
    {
        // Arrange
        string[] lines = @"..........
.S------7.
.|F----7|.
.||OOOO||.
.||OOOO||.
.|L-7F-J|.
.|II||II|.
.L--JL--J.
..........".Split('\n');
        Solver solver = new();

        // Act
        int result = solver.SolvePart2(lines);

        // Assert
        Assert.That(result, Is.EqualTo(4));
    }

    [Test]
    public void TestSolvePart2Complex()
    {
        // Arrange
        string[] lines = @"FF7FSF7F7F7F7F7F---7
L|LJ||||||||||||F--J
FL-7LJLJ||||||LJL-77
F--JF--7||LJLJ7F7FJ-
L---JF-JLJ.||-FJLJJ7
|F|F-JF---7F7-L7L|7|
|FFJF7L7F-JF7|JL---7
7-L-JL7||F7|L7F-7F7|
L.L7LFJ|||||FJL7||LJ
L7JLJL-JLJLJL--JLJ.L".Split('\n');
        Solver solver = new();

        // Act
        int result = solver.SolvePart2(lines);

        // Assert
        Assert.That(result, Is.EqualTo(10));
    }
}