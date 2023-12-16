namespace SolverTests;

public class ConditionRecordsParserTests
{
    [Test]
    public void TestParse()
    {
        // Arrange
        string[] lines = @"???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1".Split('\n');
        ConditionRecord expectedRecord = new("?#?#?#?#?#?#?#?", new List<int> { 1, 3, 1, 6 });

        // Act
        List<ConditionRecord> result = ConditionRecordsParser.Parse(lines).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(6));
        Assert.That(result, Does.Contain(expectedRecord));
    }
}