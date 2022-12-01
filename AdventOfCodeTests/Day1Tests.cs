namespace AdventOfCodeTests;
using AdventOfCode.day1; 

public class Tests {
    [SetUp]
    public void Setup() { }

    [Test]
    public void ExampleInputTest() {
        var result = Day1.SolveStar1("day1/example.txt");
        Assert.That(result, Is.EqualTo(150));
    }
}
