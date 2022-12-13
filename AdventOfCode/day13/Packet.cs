using System.Collections;

namespace AdventOfCode.day13; 

public class Packet {
    public Packet(bool divider) {
        Divider = divider;
    }
    public ArrayList Contents { get; set; }
    public bool Divider { get; private set; }
}