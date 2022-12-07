using AdventOfCode.day7;

public class Dir {
    public Dir(string name, Dir? parent) {
        Name = name;
        Parent = parent;
    }

    public long CachedSize { get; private set; } = 0L;
    public Dir? Parent { get; private set; }
    public string Name { get; private set; }
    public List<Dir> Children { get; } = new();
    public List<FileItem> Items { get; } = new();

    public void AddFileItem(FileItem file) {
        Items.Add(file);
        BubbleSizeUp(file);
    }

    private void BubbleSizeUp(FileItem file) {
        CachedSize += file.Size;
        if (Parent != null) {
            Parent.BubbleSizeUp(file);
        }
    }

    public IEnumerable<Dir> WalkDirs() {
        foreach (var child in Children) {
            yield return child;
            foreach (var gc in child.WalkDirs()) {
                yield return gc;
            }
        }
    }
}
