namespace AdventOfCodeTests; 
using AdventOfCode.day5;

public class Day5Tests {
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day5.SolveStar1("day5/example.txt");
        Assert.That(result, Is.EqualTo("CMZ"));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var result = Day5.SolveStar2("day5/example.txt");
        Assert.That(result, Is.EqualTo("MCD"));
    }
}