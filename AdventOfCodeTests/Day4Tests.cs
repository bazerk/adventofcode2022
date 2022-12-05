namespace AdventOfCodeTests; 
using AdventOfCode.day4; 

public class Day4Tests {
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day4.SolveStar1("day4/example.txt");
        Assert.That(result, Is.EqualTo(2));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var result = Day4.SolveStar2("day4/example.txt");
        Assert.That(result, Is.EqualTo(4));
    }
}