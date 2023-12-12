namespace SolverTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test2()
    {
        ConditionRecord record = new("???.###", new List<int> { 1, 1, 3 });
        Assert.That(Program.GetNrArrangements(record), Is.EqualTo(1));
    }

    [Test]
    public void Test3()
    {
        ConditionRecord record = new("?###????????", new List<int> { 3, 2, 1 });
        Assert.That(Program.GetNrArrangements(record), Is.EqualTo(10));
    }

    [Test]
    public void Test4()
    {
        ConditionRecord record = Program.Parse(".# 1");
        Assert.That(record.StringConditions, Is.EqualTo(".#?.#?.#?.#?.#"));
        Assert.That(record.GroupSizes.Count, Is.EqualTo(5));
        Assert.That(record.GroupSizes[3], Is.EqualTo(1));
    }

    [Test]
    public void Test5()
    {
        ConditionRecord record = Program.Parse(".?????.?#?????? 2,2,2,1,1");
        Assert.That(Program.GetNrArrangements(record), Is.EqualTo(8102922));
    }
}