namespace AdventOfCodeTests; 
using AdventOfCode.day11;

public class Day11Tests {

    private List<Monkey> GetTestMonkeys() {
        return new List<Monkey> {
            new(x => x * 19, x => x % 23 == 0 ? 2 : 3, new[] {62L, 92, 50, 63, 62, 93, 73, 50}),
            new(x => x + 6, x => x % 19 == 0 ? 2 : 0, new[] {54L, 65, 75, 74}),
            new(x => x * x, x => x % 13 == 0 ? 1 : 3, new[] {79L, 60, 97}),
            new(x => x + 3, x => x % 17 == 0 ? 0 : 1, new[] {74L}),
        };
    }
    
    [Test]
    public void Star1ExampleInputTest() {
        var result = Day11.SolveStar1(GetTestMonkeys());
        Assert.That(result, Is.EqualTo(10605));
    }
    
    [Test]
    public void Star2ExampleInputTest() {
        var worryFactor = 23 * 19 * 13 * 17;
        var result = Day11.SolveStar2(GetTestMonkeys(), worryFactor: worryFactor);
        Assert.That(result, Is.EqualTo(2713310158L));
    }
}