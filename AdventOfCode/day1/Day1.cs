namespace AdventOfCode.day1;

public static class Day1 {


    private static List<List<int>> ParseInput(string inputFile) {
        var elves = new List<List<int>>();
        List<int>? currentElf = null;
        foreach (var line in File.ReadLines(inputFile)) {
            if (currentElf == null || String.IsNullOrEmpty(line)) {
                currentElf = new List<int>();
                elves.Add(currentElf);
            }

            if (String.IsNullOrEmpty(line)) continue;
            
            currentElf.Add(Int32.Parse(line));
        }

        return elves;
    }

    public static int SolveStar1(string inputFile = "day1/input.txt") {
        var elves = ParseInput(inputFile);
        var maxCal = int.MinValue;
        foreach (var elf in elves) {
            var totalCal = elf.Sum();
            if (totalCal > maxCal) {
                maxCal = totalCal;
            }
        }

        return maxCal;
    }

    public static int SolveStar2(string inputFile = "day1/input.txt") {
        var elves = ParseInput(inputFile);
        var calorieList = elves.Select(elf => elf.Sum()).OrderDescending().ToList();
        return calorieList[0] + calorieList[1] + calorieList[2];
    }
}