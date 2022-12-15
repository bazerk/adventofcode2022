using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.utils;

namespace AdventOfCode.day15;

public static class Day15 {
    
    private static IEnumerable<((int, int), (int, int))> GetBeaconPairs(string inputFile) {
        var regex = new Regex(@"Sensor at x=(?<sensorX>[\-0-9]*), y=(?<sensorY>[\-0-9]*): closest beacon is at x=(?<beaconX>[\-0-9]*), y=(?<beaconY>[\-0-9]*)");
        foreach (var line in File.ReadLines(inputFile)) {
            var match = regex.Match(line);
            var sensor = (int.Parse(match.Groups["sensorX"].Value), int.Parse(match.Groups["sensorY"].Value));
            var beacon = (int.Parse(match.Groups["beaconX"].Value), int.Parse(match.Groups["beaconY"].Value));
            yield return (sensor, beacon);
        }
    }

    private static void PrintSpace(Dictionary<(int, int), char> space) {
        var minX = int.MaxValue;
        var minY = int.MaxValue;
        var maxX = int.MinValue;
        var maxY = int.MinValue;

        foreach (var (keyX, keyY) in space.Keys) {
            if (keyX < minX) minX = keyX;
            if (keyX > maxX) maxX = keyX;
            if (keyY < minY) minY = keyY;
            if (keyY > maxY) maxY = keyY;
        }

        var sb = new StringBuilder();
        sb.AppendLine($"from x={minX} to x={maxX}; y={minY} to y={maxY}");
        for (var y = minY; y <= maxY; y++) {
            for (var x = minX; x <= maxX; x++) {
                if (space.TryGetValue((x, y), out char output)) {
                    sb.Append(output);
                } else {
                    sb.Append('.');
                }
            }
            sb.Append(Environment.NewLine);
        }
        sb.Append(Environment.NewLine);
        Console.Write(sb.ToString());
    }
    
    public static int SolveStar1Naive(string inputFile = "day15/input.txt", int testY = 2000000) {
        var space = new Dictionary<(int, int), char>();
        foreach (var (sensor, beacon) in GetBeaconPairs(inputFile)) {
            space[sensor] = 'S';
            space[beacon] = 'B';
            var (sensorX, sensorY) = sensor;
            var (beaconX, beaconY) = beacon;
            var diff = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);
            for (var x = -diff; x <= diff; x++) {
                var yDiff = diff - Math.Abs(x);
                for (var y = -yDiff; y <= yDiff; y++) {
                    var coord = (sensorX + x, sensorY + y);
                    space.TryAdd(coord, '#');
                }
            }
        }

        var count = 0;
        foreach (var ((keyX, keyY), value) in space) {
            if (keyY == testY && value == '#') {
                count++;
            }
        }
        return count;
    }
    
    public static int SolveStar1(string inputFile = "day15/input.txt", int testY = 2000000) {
        var runs = new List<(int, int)>();
        foreach (var (sensor, beacon) in GetBeaconPairs(inputFile)) {
            var (sensorX, sensorY) = sensor;
            var (beaconX, beaconY) = beacon;
            var maxDiff = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);

            var diffY = Math.Abs(sensorY - testY);
            if (diffY > maxDiff) continue;
            
            var xRange = maxDiff - diffY;
            runs.Add((sensorX - xRange, sensorX + xRange));
        }

        var maxX = int.MinValue;
        var count = 0;
        foreach (var (runX1, runX2) in runs.OrderBy(x => x.Item1)) {
            var startX = Math.Max(runX1, maxX);
            if (runX2 > maxX) maxX = runX2;
            var diff = runX2 - startX;
            if (diff > 0) count += diff;
        }

        return count;
    }
    
    public static long SolveStar2(string inputFile = "day15/input.txt", int maxValue = 4000000) {
        var pairs = GetBeaconPairs(inputFile).ToList();
        for (var y = 0; y <= maxValue; y++) {
            var runs = new List<(int, int)>();
            foreach (var (sensor, beacon) in pairs) {
                var (sensorX, sensorY) = sensor;
                var (beaconX, beaconY) = beacon;
                var maxDiff = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);

                var diffY = Math.Abs(sensorY - y);
                if (diffY > maxDiff) continue;
            
                var xRange = maxDiff - diffY;
                var x1 = sensorX - xRange;
                var x2 = sensorX + xRange;
                if (x1 < 0) x1 = 0;
                if (x2 > maxValue) x2 = maxValue;
                if (x1 > x2) continue;

                runs.Add((x1, x2));
            }

            var maxX = -1;
            foreach (var (runX1, runX2) in runs.OrderBy(x => x.Item1)) {
                var startX = runX1;
                if (startX <= maxX) startX = maxX + 1;

                if (startX > (maxX + 1)) {
                    return (startX - 1L) * 4000000L + y;
                }
                
                if (runX2 > maxX) maxX = runX2;
            }
        }
        
        return -1;
    }
}