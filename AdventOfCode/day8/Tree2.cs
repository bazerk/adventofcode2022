namespace AdventOfCode.day8; 

public class Tree2 {
    public int VisibleLeft { get; set; }
    public int VisibleRight { get; set; }
    public int VisibleTop { get; set; }
    public int VisibleBottom { get; set; }

    public int ViewScore => VisibleLeft * VisibleRight * VisibleTop * VisibleBottom;
    
    public int Height { get; }

    public Tree2(char ch) {
        Height = int.Parse(ch.ToString());
    }
    
    public override string ToString() {
        return ViewScore.ToString();
    }
}