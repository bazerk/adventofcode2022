namespace AdventOfCode.day10;

public class Instruction {
    public int CycleCount { get; set; }
    public int Effect { get; set; }
}

public class CPU {

    private int _cycle = 1;
    public int _registerValue = 1;
    private readonly Queue<Instruction> _instructions; 

    public CPU(IEnumerable<Instruction> instructions) {
        _instructions = new Queue<Instruction>(instructions);
    }

    public (int, int, bool) Process() {
        var (cycleToReturn, regToReturn)  = (_cycle, _registerValue);
        _cycle++;

        if (_instructions.Count == 0) {
            return (_cycle, _registerValue, false);
        }

        var currentInstruction = _instructions.Peek();
        currentInstruction.CycleCount--;
        
        if (currentInstruction.CycleCount == 0) {
            _instructions.Dequeue();
            _registerValue += currentInstruction.Effect;
        }

        return (cycleToReturn, regToReturn, _instructions.Count > 0);
    }
}
