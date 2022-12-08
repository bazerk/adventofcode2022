namespace AdventOfCodeTests; 
using AdventOfCode.day8;

public class Day8Tests {
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day8.SolveStar1("day8/example.txt");
        Assert.That(result, Is.EqualTo(21));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var result = Day8.SolveStar2("day8/example.txt");
        Assert.That(result, Is.EqualTo(8));
    }
}