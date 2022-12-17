namespace AdventOfCodeTests; 
using AdventOfCode.day16;

public class Day16Tests {
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day16.SolveStar1("day16/example.txt");
        Assert.That(result, Is.EqualTo(1651));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var result = Day16.SolveStar2("day16/example.txt");
        Assert.That(result, Is.EqualTo(1707));
    }
}