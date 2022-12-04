namespace AdventOfCodeTests; 
using AdventOfCode.day3; 

public class Day3Tests {
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day3.SolveStar1("day3/example.txt");
        Assert.That(result, Is.EqualTo(157));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var result = Day3.SolveStar2("day3/example.txt");
        Assert.That(result, Is.EqualTo(70));
    }
}