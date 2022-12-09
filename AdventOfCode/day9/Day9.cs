namespace AdventOfCode.day9; 

public static class Day9 {

    private static IEnumerable<(string, int)> GetMoves(string inputFile) {
        foreach (var line in File.ReadLines(inputFile)) {
            var split = line.Split();
            yield return (split[0], int.Parse(split[1]));
        }
    }

    private static Dictionary<string, (int, int)> MoveMap = new Dictionary<string, (int, int)>() {
        {"U", (0, -1)},
        {"R", (1, 0)},
        {"D", (0, 1)},
        {"L", (-1, 0)},
    };
    
    public static int SolveStar1(string inputFile = "day9/input.txt") {
        var head = (0, 0);
        var tail = (0, 0);
        var trail = new HashSet<(int, int)>();
        trail.Add(tail);

        foreach (var (direction, count) in GetMoves(inputFile)) {
            var move = MoveMap[direction];
            var ixMove = count;
            while (ixMove > 0) {
                ixMove--;
                head = (head.Item1 + move.Item1, head.Item2 + move.Item2);

                var xDiff = head.Item1 - tail.Item1;
                var yDiff = head.Item2 - tail.Item2;

                if (Math.Abs(xDiff) <= 1 && Math.Abs(yDiff) <= 1) {
                    continue;
                }

                if (xDiff == 2) xDiff = 1;
                if (xDiff == -2) xDiff = -1;
                if (yDiff == 2) yDiff = 1;
                if (yDiff == -2) yDiff = -1;

                tail = (tail.Item1 + xDiff, tail.Item2 + yDiff);
                trail.Add(tail);
            }
        }

        return trail.Count;
    }

    public static int SolveStar2(string inputFile = "day9/input.txt") {
        var head = (0, 0);
        var knots = new List<(int, int)>();
        for (var ix = 0; ix < 9; ix++) {
            knots.Add((0, 0));
        }
        
        var trail = new HashSet<(int, int)>();
        trail.Add((0, 0));

        foreach (var (direction, count) in GetMoves(inputFile)) {
            var move = MoveMap[direction];
            var ixMove = count;
            while (ixMove > 0) {
                ixMove--;
                head = (head.Item1 + move.Item1, head.Item2 + move.Item2);

                for (var ix = 0; ix < 9; ix++) {
                    var previous = (ix == 0) ? head : knots[ix - 1];
                    var current = knots[ix];

                    var xDiff = previous.Item1 - current.Item1;
                    var yDiff = previous.Item2 - current.Item2;

                    if (Math.Abs(xDiff) <= 1 && Math.Abs(yDiff) <= 1) {
                        continue;
                    }

                    if (xDiff == 2) xDiff = 1;
                    if (xDiff == -2) xDiff = -1;
                    if (yDiff == 2) yDiff = 1;
                    if (yDiff == -2) yDiff = -1;

                    current = (current.Item1 + xDiff, current.Item2 + yDiff);
                    knots[ix] = current;

                    if (ix == 8) {
                        trail.Add(current);
                    }
                }
            }
        }

        return trail.Count;
    }
}