namespace AdventOfCodeTests; 
using AdventOfCode.day9;

public class Day9Tests {
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day9.SolveStar1("day9/example.txt");
        Assert.That(result, Is.EqualTo(13));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var result = Day9.SolveStar2("day9/example2.txt");
        Assert.That(result, Is.EqualTo(36));
    }
}