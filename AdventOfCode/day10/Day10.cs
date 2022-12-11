using System.Text;

namespace AdventOfCode.day10; 

public static class Day10 {

    private static IEnumerable<Instruction> LoadInstructions(string inputFile) {
        foreach (var line in File.ReadLines(inputFile)) {
            if (line.StartsWith("noop")) {
                yield return new Instruction {
                    CycleCount = 1
                };
                continue;
            }

            var split = line.Split();
            yield return new Instruction {
                CycleCount = 2,
                Effect = int.Parse(split[1])
            };
        }
    }

    public static int SolveStar1(string inputFile = "day10/input.txt") {
        var cpu = new CPU(LoadInstructions(inputFile));
        var hasMoreInstructions = true;
        var sum = 0;
        while (hasMoreInstructions) {
            var result = cpu.Process();
            hasMoreInstructions = result.Item3;
            var (cycle, reg) = (result.Item1, result.Item2);

            var check = (cycle - 20) % 40;
            if (check == 0) {
                sum += cycle * reg;
            }
        }

        return sum;
    }

    public static string SolveStar2(string inputFile = "day10/input.txt") {
        var cpu = new CPU(LoadInstructions(inputFile));
        var output = new StringBuilder();
        var (cycle, reg, _) = cpu.Process();
        while (cycle <= 240) {
            var pixel = cycle % 40;
            if (pixel == 0) pixel = 40;
            pixel--;

            if (reg >= pixel - 1 && reg <= pixel + 1) {
                output.Append('#');
            }
            else {
                output.Append('.');
            }
            
            if (cycle % 40 == 0) {
                output.AppendLine();
            }
            
            (cycle, reg, _) = cpu.Process();
        }

        return output.ToString();
    }
}