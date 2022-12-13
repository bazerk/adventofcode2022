namespace AdventOfCodeTests; 
using AdventOfCode.day13;

public class Day13Tests {
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day13.SolveStar1("day13/example.txt");
        Assert.That(result, Is.EqualTo(13));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var result = Day13.SolveStar2("day13/example.txt");
        Assert.That(result, Is.EqualTo(140));
    }
}