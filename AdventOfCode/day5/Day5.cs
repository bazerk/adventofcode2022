using System.Text.RegularExpressions;

namespace AdventOfCode.day5; 

public static class Day5 {

    private static (List<LinkedList<char>>, List<string>) GetInitialStateAndMoves(string inputFile) {
        var stacks = new List<LinkedList<char>>();
        var moves = new List<string>();
        var finishedStacks = false;
        
        foreach (var line in File.ReadLines(inputFile)) {
            if (string.IsNullOrWhiteSpace(line)) continue;
            
            if (finishedStacks) {
                moves.Add(line);
                continue;
            }

            for (var ix = 0; ix <= line.Length / 4; ix++) {
                var check = line[1 + ix * 4];
                if (char.IsWhiteSpace(check)) {
                    continue;
                }

                while (ix >= stacks.Count) {
                    stacks.Add(new LinkedList<char>());
                }
                if (char.IsDigit(check)) {
                    finishedStacks = true;
                    continue;
                }

                stacks[ix].AddFirst(check);
            }
        }

        return (stacks, moves);
    }
    
    public static string SolveStar1(string inputFile = "day5/input.txt") {
        var (stacks, moves) = GetInitialStateAndMoves(inputFile);
        foreach (var move in moves) {
            var match = Regex.Match(move, @"move (?<count>[0-9]*) from (?<source>[0-9]*) to (?<dest>[0-9]*)");
            var count = int.Parse(match.Groups["count"].Value);
            var source = int.Parse(match.Groups["source"].Value) - 1;
            var dest = int.Parse(match.Groups["dest"].Value) - 1;
            while (count > 0) {
                var item = stacks[source].Last.Value;
                stacks[source].RemoveLast();
                stacks[dest].AddLast(item);
                count--;
            }
        }

        string output = "";
        foreach (var stack in stacks) {
            if (stack.Count > 0) {
                output += stack.Last.Value;
            }
            else {
                output += " ";
            }
        }
        
        return output;
    }

    public static string SolveStar2(string inputFile = "day5/input.txt") {
        var (stacks, moves) = GetInitialStateAndMoves(inputFile);
        foreach (var move in moves) {
            var match = Regex.Match(move, @"move (?<count>[0-9]*) from (?<source>[0-9]*) to (?<dest>[0-9]*)");
            var count = int.Parse(match.Groups["count"].Value);
            var source = int.Parse(match.Groups["source"].Value) - 1;
            var dest = int.Parse(match.Groups["dest"].Value) - 1;
            var temp = new Stack<char>();
            while (count > 0) {
                var item = stacks[source].Last.Value;
                stacks[source].RemoveLast();
                temp.Push(item);
                count--;
            }

            while (temp.Count > 0) {
                stacks[dest].AddLast(temp.Pop());
            }
        }

        string output = "";
        foreach (var stack in stacks) {
            if (stack.Count > 0) {
                output += stack.Last.Value;
            }
            else {
                output += " ";
            }
        }
        
        return output;
    }
}