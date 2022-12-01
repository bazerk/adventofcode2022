namespace AdventOfCodeTests;
using AdventOfCode.day1; 

public class Tests {
    [SetUp]
    public void Setup() { }

    [Test]
    public void Star1ExampleInputTest() {
        var result = Day1.SolveStar1("day1/example.txt");
        Assert.That(result, Is.EqualTo(24000));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var result = Day1.SolveStar2("day1/example.txt");
        Assert.That(result, Is.EqualTo(45000));
    }
}
