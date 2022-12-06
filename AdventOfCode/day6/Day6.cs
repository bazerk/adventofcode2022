namespace AdventOfCode.day6; 

public static class Day6 {

    private static int GetStartOfPacketMarket(string stream, int checkLength = 4) {
        var buffer = new Queue<char>();
        for (var ix = 0; ix < stream.Length; ix++) {
            if (buffer.Count < checkLength) {
                buffer.Enqueue(stream[ix]);
                continue;
            }

            buffer.Dequeue();
            buffer.Enqueue(stream[ix]);

            var check = new HashSet<char>(buffer);
            if (check.Count == checkLength) {
                return ix + 1;
            }
        }

        return -1;
    }
    
    public static int SolveStar1(string inputFile = "day6/input.txt") {
        return GetStartOfPacketMarket(File.ReadAllText(inputFile));
    }

    public static int SolveStar2(string inputFile = "day6/input.txt") {
        return GetStartOfPacketMarket(File.ReadAllText(inputFile), checkLength: 14);
    }
}