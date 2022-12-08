namespace AdventOfCode.day8; 

public class Tree {
    public bool VisibleLeft { get; set; }
    public bool VisibleRight { get; set; }
    public bool VisibleTop { get; set; }
    public bool VisibleBottom { get; set; }

    public bool Visible => (VisibleBottom || VisibleLeft || VisibleRight || VisibleTop);

    public int Height { get; }

    public Tree(char ch) {
        Height = int.Parse(ch.ToString());
    }
}
