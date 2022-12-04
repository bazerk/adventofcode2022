using System.Runtime.Intrinsics;

namespace AdventOfCode.day3; 

public static class Day3 {

    private static Dictionary<char, int> GetPriorities() {
        var priorities = new Dictionary<char, int>();
        for (var ch = 'a'; ch <= 'z'; ch++) {
            priorities[ch] = ch - 'a' + 1;
        }
        for (var ch = 'A'; ch <= 'Z'; ch++) {
            priorities[ch] = ch - 'A' + 27;
        }

        return priorities;
    }

    public static int SolveStar1(string inputFile = "day3/input.txt") {
        var priorities = GetPriorities();
        var total = 0;
        foreach (var rucksack in File.ReadLines(inputFile)) {
            var halfway = rucksack.Length / 2;
            var c1 = new HashSet<char>(rucksack.Substring(0, halfway).Select(x => x));
            var c2 = new HashSet<char>(rucksack.Substring(halfway, halfway).Select(x => x));
            c1.IntersectWith(c2);
            total += priorities[c1.First()];
        }
        
        return total;
    }

    public static int SolveStar2(string inputFile = "day3/input.txt") {
        var priorities = GetPriorities();
        var rucksacks = File.ReadLines(inputFile).ToArray();
        var total = 0;
        for (var ix = 0; ix < rucksacks.Length; ix += 3) {
            var sack1 = new HashSet<char>(rucksacks[ix].Select(ch => ch));
            var sack2 = new HashSet<char>(rucksacks[ix + 1].Select(ch => ch));
            var sack3 = new HashSet<char>(rucksacks[ix + 2].Select(ch => ch));
            sack1.IntersectWith(sack2);
            sack1.IntersectWith(sack3);
            if (sack1.Count != 1) {
                throw new Exception("wrong");
            }
            total += priorities[sack1.First()];
        }
        return total;
    }
}