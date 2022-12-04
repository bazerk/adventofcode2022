namespace AdventOfCode.day2; 

public static class Day2 {

    public enum Move {
        Rock = 0,
        Paper = 1,
        Scissors = 2
    }

    private static Dictionary<string, Move> _moveMap = new Dictionary<string, Move>(){
        {"A", Move.Rock},
        {"B", Move.Paper},
        {"C", Move.Scissors},
        {"X", Move.Rock},
        {"Y", Move.Paper},
        {"Z", Move.Scissors},
    };
    
    private static Dictionary<Move, int> _score = new Dictionary<Move, int>() {
        {Move.Rock, 1},
        {Move.Paper, 2},
        {Move.Scissors, 3},
    };

    private static int ScoreMoves(List<(Move, Move)> moves) {
        var score = 0;
        foreach (var move in moves) {
            var (elf, me) = move;
            var moveScore = _score[me];
            if (elf == me) {
                moveScore += 3;
            } else if (elf == Move.Paper && me == Move.Scissors || 
                       elf == Move.Rock && me == Move.Paper || 
                       elf == Move.Scissors && me == Move.Rock) {
                moveScore += 6;
            }

            score += moveScore;
        }
        return score;
    }
    
    private static List<(Move, Move)> ParseInput(string inputFile) {
        var moves = new List<(Move, Move)>();
        
        foreach (var line in File.ReadLines(inputFile)) {
            var split = line.Split();
            moves.Add((_moveMap[split[0]], _moveMap[split[1]]));
        }

        return moves;
    }
    
    private static List<(Move, Move)> ParseInput2(string inputFile) {
        var moves = new List<(Move, Move)>();
        
        
        foreach (var line in File.ReadLines(inputFile)) {
            var split = line.Split();
            var elf = _moveMap[split[0]];
            var outcome = split[1];
            var me = elf;;

            if (outcome == "X") {
                me = (Move)(((int)elf + 2) % 3);
            } else if (outcome == "Z") {
                me = (Move)(((int)elf + 1) % 3);
            }
            
            moves.Add((elf, me));
        }

        return moves;
    }
    
    public static int SolveStar1(string inputFile = "day2/input.txt") {
        var moves = ParseInput(inputFile);
        return ScoreMoves(moves);
    }

    public static int SolveStar2(string inputFile = "day2/input.txt") {
        var moves = ParseInput2(inputFile);
        return ScoreMoves(moves);
    }
}