namespace SolverTests;

public class MapParserTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        string[] lines = @"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)".Split('\n');

        // Act
        Map result = MapParser.Parse(lines);

        // Assert
        Assert.That(result.Instructions, Is.EqualTo(new List<Instruction>() { Instruction.Left, Instruction.Left, Instruction.Right }));
        Assert.That(result.Network.Count, Is.EqualTo(3));
        Assert.That(result.Network["BBB"], Is.EqualTo(new Node("BBB", "AAA", "ZZZ")));
    }
}