namespace AdventOfCode.day4; 

public static class Day4 {

    private static List<((int, int), (int, int))> GetAssignmentPairs(string inputFile) {
        var pairs = new List<((int, int), (int, int))>();
        var parseAssignment = (string[] str) => (int.Parse(str[0]), int.Parse(str[1])); 
        foreach (var line in File.ReadLines(inputFile)) {
            var split = line.Split(",");
            var assignment1 = split[0].Split("-");
            var assignment2 = split[1].Split("-");
            pairs.Add((parseAssignment(assignment1), parseAssignment(assignment2)));
        }
        
        return pairs;
    }

    private static bool CheckContains((int, int) pair1, (int, int) pair2) {
        var (x1, x2) = pair1;
        var (y1, y2) = pair2;

        if (x1 >= y1 && x2 <= y2) return true;
        if (y1 >= x1 && y2 <= x2) return true;
        return false;
    }
   
    public static int SolveStar1(string inputFile = "day4/input.txt") {
        var matchingCount = 0;
        foreach (var (p1, p2) in GetAssignmentPairs(inputFile)) {
            if (CheckContains(p1, p2)) {
                matchingCount++;
            }
        }

        return matchingCount;
    }
    
    private static bool CheckOverlaps((int, int) pair1, (int, int) pair2) {
        var (x1, x2) = pair1;
        var (y1, y2) = pair2;

        if (x1 >= y1 && x1 <= y2) return true;
        if (x2 >= y1 && x2 <= y2) return true;
        if (y1 >= x1 && y1 <= x2) return true;
        if (y2 >= x1 && y2 <= x2) return true;
        
        return false;
    }

    public static int SolveStar2(string inputFile = "day4/input.txt") {
        var matchingCount = 0;
        foreach (var (p1, p2) in GetAssignmentPairs(inputFile)) {
            if (CheckOverlaps(p1, p2)) {
                matchingCount++;
            }
        }

        return matchingCount;
    }
}