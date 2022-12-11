namespace AdventOfCodeTests; 
using AdventOfCode.day10;

public class Day10Tests {
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day10.SolveStar1("day10/example.txt");
        Assert.That(result, Is.EqualTo(13140));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var expected = """
##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....

""";
        var result = Day10.SolveStar2("day10/example.txt");
        Assert.That(result, Is.EquivalentTo(expected));
    }
}