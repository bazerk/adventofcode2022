using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using AdventOfCode.utils;

namespace AdventOfCode.day13;

public static class Day13 {

    private static IEnumerable<(Packet, Packet)> LoadPackets(string inputFile) {
        var packets = new Stack<Packet>();

        (Packet lhs, Packet rhs) ReturnPair() {
            var rhs = packets.Pop();
            var lhs = packets.Pop();
            return (lhs, rhs);
        }

        foreach (var line in File.ReadLines(inputFile)) {
            if (string.IsNullOrEmpty(line)) {
                yield return ReturnPair();
            }
            packets.Push(ParsePacket(line));
        }
        yield return ReturnPair();
    }
    
    private static IEnumerable<Packet> LoadPackets2(string inputFile) {
        foreach (var line in File.ReadLines(inputFile)) {
            if (string.IsNullOrWhiteSpace(line)) continue;
            yield return ParsePacket(line);
        }
    }

    private static Packet ParsePacket(string line, bool divider = false) {
        var packet = new Packet(divider);
        var ixLine = 0;
        var lists = new Stack<ArrayList?>();
        while (ixLine < line.Length) {
            var test = line[ixLine].ToString();
            ixLine++;

            lists.TryPeek(out var currentList);

            if (test == "[") {
                var newList = new ArrayList();
                if (currentList == null) {
                    packet.Contents = newList;
                }

                currentList?.Add(newList);
                lists.Push(newList);
                continue;
            }

            if (test == "]") {
                lists.Pop();
                continue;
            }

            if (test == ",") {
                continue;
            }

            while (char.IsDigit(line[ixLine])) {
                test += line[ixLine];
                ixLine++;
            }

            if (currentList == null) {
                throw new Exception();
            }

            currentList.Add(int.Parse(test));
        }

        return packet;
    }

    private static int ComparePackets(ArrayList lhs, ArrayList rhs) {
        for (var ix = 0; ix < int.Max(lhs.Count, rhs.Count); ix++) {
            if (ix >= lhs.Count) {
                return -1;
            }
            
            if (ix >= rhs.Count) {
                return 1;
            }
            
            var lhsItem = lhs[ix];
            var rhsItem = rhs[ix];

            if (lhsItem is int x && rhsItem is int y) {
                if (x > y) {
                    return 1;
                }

                if (x < y) {
                    return -1;
                }
                continue;
            }

            if (lhsItem is int) {
                lhsItem = new ArrayList {lhsItem};
            }

            if (rhsItem is int) {
                rhsItem = new ArrayList {rhsItem};
            }

            var result = ComparePackets((ArrayList)lhsItem, (ArrayList)rhsItem);
            if (result == 0) {
                continue;
            }

            return result;
        }

        return 0;
    }
    
    public static int SolveStar1(string inputFile = "day13/input.txt") {
        var sum = 0;
        var ix = 1;
        foreach (var (lhs, rhs) in LoadPackets(inputFile)) {
            if (ComparePackets(lhs.Contents, rhs.Contents) <= 0) {
                sum += ix;
            }
            ix++;
        }
        return sum;
    }
    
    private class PacketCompare : IComparer<Packet> {
        public int Compare(Packet x, Packet y) => ComparePackets(x.Contents, y.Contents);
    }

    public static int SolveStar2(string inputFile = "day13/input.txt") {
        var packets = LoadPackets2(inputFile).ToList();
        packets.Add(ParsePacket("[[2]]", true));
        packets.Add(ParsePacket("[[6]]", true));
        packets.Sort(new PacketCompare());

        var indexes = new List<int>();
        for (var ix = 0; ix < packets.Count; ix++) {
            if (packets[ix].Divider) {
                indexes.Add(ix+1);
            }
        }
        
        return indexes[0] * indexes[1];
    }
}