namespace AdventOfCodeTests; 
using AdventOfCode.day12;

public class Day12Tests {
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day12.SolveStar1("day12/example.txt");
        Assert.That(result, Is.EqualTo(31));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var result = Day12.SolveStar2("day12/example.txt");
        Assert.That(result, Is.EqualTo(29));
    }
}