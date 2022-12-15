namespace AdventOfCodeTests; 
using AdventOfCode.day15;

public class Day15Tests {
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day15.SolveStar1("day15/example.txt", testY: 10);
        Assert.That(result, Is.EqualTo(26));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var result = Day15.SolveStar2("day15/example.txt", maxValue: 20);
        Assert.That(result, Is.EqualTo(56000011));
    }
}