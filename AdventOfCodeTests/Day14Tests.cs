namespace AdventOfCodeTests; 
using AdventOfCode.day14;

public class Day14Tests {
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day14.SolveStar1("day14/example.txt");
        Assert.That(result, Is.EqualTo(24));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var result = Day14.SolveStar2("day14/example.txt");
        Assert.That(result, Is.EqualTo(93));
    }
}