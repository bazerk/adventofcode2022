namespace AdventOfCode.day7; 

public class FileItem {
    public FileItem(long size, string name) {
        Size = size;
        Name = name;
    }
    public string Name { get; }
    public long Size { get; }
}