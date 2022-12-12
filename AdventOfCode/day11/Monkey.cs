namespace AdventOfCode.day11; 

public class Monkey {
    public Monkey(Func<long, long> operation, Func<long, int> test, IEnumerable<long> startingItems) {
        _operation = operation;
        _test = test;
        foreach (var item in startingItems) {
            _items.Push(item);
        }
    }

    private readonly Stack<long> _items = new();
    private readonly Func<long, long> _operation;
    private readonly Func<long, int> _test;

    public long InspectionCount { get; private set; }

    public IEnumerable<(int, long)> InspectItems(long? worryFactor = null) {
        while (_items.Count > 0) {
            InspectionCount++;
            var item = _items.Pop();
            item = _operation(item);
            if (worryFactor.HasValue) {
                item %= worryFactor.Value;
            } else {
                item /= 3;
            }
            yield return (_test(item), item);
        }
    }

    public void AddItem(long item) {
        _items.Push(item);
    }
}