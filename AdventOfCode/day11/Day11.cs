using System.Numerics;
using System.Text;

namespace AdventOfCode.day11; 

public static class Day11 {

    public static T GreatestCommonDivisor<T>(T a, T b) where T : INumber<T>
    {
        while (b != T.Zero)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static T LeastCommonMultiple<T>(T a, T b) where T : INumber<T>
        => a / GreatestCommonDivisor(a, b) * b;
    
    public static T LeastCommonMultiple<T>(this IEnumerable<T> values) where T : INumber<T>
        => values.Aggregate(LeastCommonMultiple);
    
    private static List<Monkey> GetMonkeys() {
        return new List<Monkey> {
            new(x => x * 7, x => x % 2 == 0 ? 7 : 1, new[] {62L, 92, 50, 63, 62, 93, 73, 50}),
            new(x => x + 3, x => x % 7 == 0 ? 2 : 4, new[] {51L, 97, 74, 84, 99}),
            new(x => x + 4, x => x % 13 == 0 ? 5 : 4, new[] {98L, 86, 62, 76, 51, 81, 95}),
            new(x => x + 5, x => x % 19 == 0 ? 6 : 0, new[] {53L, 95, 50, 85, 83, 72}),
            new(x => x * 5, x => x % 11 == 0 ? 5 : 3, new[] {59L, 60, 63, 71}),
            new(x => x * x, x => x % 5 == 0 ? 6 : 3, new[] {92L, 65}),
            new(x => x + 8, x => x % 3 == 0 ? 0 : 7, new[] {78L}),
            new(x => x + 1, x => x % 17 == 0 ? 2 : 1, new[] {84L, 93, 54}),
        };
    }
    
    public static long SolveStar1(List<Monkey>? monkeys = null, int rounds = 20) {
        monkeys ??= GetMonkeys();
        
        for (int round = 0; round < rounds; round++) {
            foreach (var monkey in monkeys) {
                foreach (var (receiver, item) in monkey.InspectItems()) {
                    monkeys[receiver].AddItem(item);
                }
            }
        }

        var ordered = monkeys.OrderByDescending(m => m.InspectionCount).ToList();
        return ordered[0].InspectionCount * ordered[1].InspectionCount;
    }

    public static long SolveStar2(List<Monkey>? monkeys = null, int rounds = 10000, long? worryFactor = null) {
        monkeys ??= GetMonkeys();
        worryFactor ??= LeastCommonMultiple(new [] {2L, 7, 13, 19, 11, 5, 3, 17});
        
        for (int round = 0; round < rounds; round++) {
            foreach (var monkey in monkeys) {
                foreach (var (receiver, item) in monkey.InspectItems(worryFactor: worryFactor)) {
                    monkeys[receiver].AddItem(item);
                }
            }
        }

        var ordered = monkeys.OrderByDescending(m => m.InspectionCount).ToList();
        return ordered[0].InspectionCount * ordered[1].InspectionCount;
    }
}