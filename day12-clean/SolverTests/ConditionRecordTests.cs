namespace SolverTests;

public class ConditionRecordTests
{
    [TestCase("???.### 1,1,3", 1)]
    [TestCase("?###???????? 3,2,1", 10)]
    public void TestGetNrArrangements(string line, long expectedResult)
    {
        // Arrange
        ConditionRecord conditionRecord = ConditionRecordsParser.ParseLine(line);

        // Act
        long result = conditionRecord.GetNrArrangements();

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}