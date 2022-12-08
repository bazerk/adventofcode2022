using System.Text;

namespace AdventOfCode.day8; 



public class Grid<T> {
    public int Width => Items.GetLength(0);
    public int Height => Items.GetLength(1);
    public T[,] Items { get; }

    public Grid(string[] lines, Func<char, T> generator) {
        Items = new T[lines[0].Length, lines.Length];
        var y = 0;
        foreach (var line in lines) {
            var x = 0;
            foreach (var val in line) {
                Items[x, y] = generator(val);
                x++;
            }
            y++;
        }
    }
    
    public IEnumerable<(int, int, T)> Enumerate() {
        for (var y = 0; y < Height; y++) {
            for (var x = 0; x < Width; x++) {
                yield return (x, y, Items[x, y]);
            }
        }
    }
    
    public IEnumerable<T> EnumerateItems() {
        for (var y = 0; y < Height; y++) {
            for (var x = 0; x < Width; x++) {
                yield return Items[x, y];
            }
        }
    }

    public override string ToString() {
        var sb = new StringBuilder();
        for (var y = 0; y < Height; y++) {
            for (var x = 0; x < Width; x++) {
                sb.Append(Items[x, y]);
            }
            sb.Append(Environment.NewLine);
        }

        return sb.ToString();
    }
}
