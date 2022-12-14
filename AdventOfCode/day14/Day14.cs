using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using AdventOfCode.utils;

namespace AdventOfCode.day14;

public static class Day14 {
    private static IEnumerable<List<(int, int)>> ParseInput(string inputFile) {
        foreach (var line in File.ReadLines(inputFile)) {
            yield return line.Split(" -> ")
                .Select(str => str.Split(","))
                .Select(spl => (int.Parse(spl[0]), int.Parse(spl[1])))
                .ToList();
        }
    }

    private static Dictionary<(int, int), bool> LoadGrid(string inputFile, out int maxY) {
        var grid = new Dictionary<(int, int), bool>();
        maxY = int.MinValue;
        foreach (var coordinateSet in ParseInput(inputFile)) {
            for (var ix = 0; ix < coordinateSet.Count - 1; ix++) {
                var (fromX, fromY) = coordinateSet[ix];
                var (toX, toY) = coordinateSet[ix + 1];
                var (diffX, diffY) = (toX - fromX, toY - fromY);
                if (diffX > 0) diffX = 1;
                if (diffX < 0) diffX = -1;
                if (diffY > 0) diffY = 1;
                if (diffY < 0) diffY = -1;

                grid[(fromX, fromY)] = true;
                while (fromX != toX || fromY != toY) {
                    if (fromY > maxY) {
                        maxY = fromY;
                    }

                    fromX += diffX;
                    fromY += diffY;
                    grid[(fromX, fromY)] = true;
                }
            }
        }

        return grid;
    }
    
    public static int SolveStar1(string inputFile = "day14/input.txt") {
        var grid = LoadGrid(inputFile, out var maxY);

        var fellIntoTheVoid = false;
        var sandSettled = 0;
        while (!fellIntoTheVoid) {
            var (sandX, sandY) = (500, 0);
            while (sandY < maxY) {
                // try below
                if (!grid.ContainsKey((sandX, sandY + 1))) {
                    sandY += 1;
                    continue;
                }

                if (!grid.ContainsKey((sandX - 1, sandY + 1))) {
                    sandX -= 1;
                    sandY += 1;
                    continue;
                }
                
                if (!grid.ContainsKey((sandX + 1, sandY + 1))) {
                    sandX += 1;
                    sandY += 1;
                    continue;
                }

                grid[(sandX, sandY)] = true;
                sandSettled += 1;
                break;
            }

            fellIntoTheVoid = sandY >= maxY;
        }
        
        return sandSettled;
    }
    
    public static int SolveStar2(string inputFile = "day14/input.txt") {
        var grid = LoadGrid(inputFile, out var maxY);
        var floorY = maxY + 2;
        
        var sandSettled = 0;
        while (!grid.ContainsKey((500, 0))) {
            var (sandX, sandY) = (500, 0);
            while (true) {
                // try below
                if (sandY < floorY - 1) {
                    if (!grid.ContainsKey((sandX, sandY + 1))) {
                        sandY += 1;
                        continue;
                    }

                    if (!grid.ContainsKey((sandX - 1, sandY + 1))) {
                        sandX -= 1;
                        sandY += 1;
                        continue;
                    }

                    if (!grid.ContainsKey((sandX + 1, sandY + 1))) {
                        sandX += 1;
                        sandY += 1;
                        continue;
                    }
                }

                grid[(sandX, sandY)] = true;
                sandSettled += 1;
                break;
            }
        }
        
        return sandSettled;
    }
}