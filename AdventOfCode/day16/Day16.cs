using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfCode.day16;

public static class Day16 {

    public class Valve {
        public string Name { get; set; }
        public int FlowRate { get; set; }
        public List<string> Neighbours { get; set; }

        public override string ToString() {
            return $"{Name} = {FlowRate}";
        }
    }
    
    private static Dictionary<string, int> GetPathCosts(Dictionary<string, Valve> valves, string initialNode) {
        var dist = new Dictionary<string, int> {
            [initialNode] = 0
        };

        var queue = new Dictionary<string, int>();

        foreach (var nodeName in valves.Keys) {
            if (nodeName != initialNode) {
                dist[nodeName] = Int32.MaxValue;
            }

            queue[nodeName] = dist[nodeName];
        }

        while (queue.Count > 0) {
            var nodeName = queue.MinBy(kv => kv.Value).Key;
            queue.Remove(nodeName);
            foreach (var neighbour in valves[nodeName].Neighbours) {
                var distance = dist[nodeName] + 1;
                if (distance < dist[neighbour]) {
                    dist[neighbour] = distance;
                    queue[neighbour] = distance;
                }
            }
        }

        return dist;
    }


    private static IEnumerable<Valve> GetValves(string inputFile) {
        var regex = new Regex(@"Valve (?<valveName>[A-Z]*) has flow rate=(?<flowRate>[0-9]*); tunnel(s)? lead(s)? to valve(s)? (?<neighbours>.*)$");
        foreach (var line in File.ReadLines(inputFile)) {
            var match = regex.Match(line);
            var name = match.Groups["valveName"].Value;
            var flowRate = int.Parse(match.Groups["flowRate"].Value);
            var neighbours = match.Groups["neighbours"].Value.Split(", ");
            yield return new Valve {
                Name=name,
                FlowRate=flowRate,
                Neighbours=neighbours.ToList()
            };
        }
    }

    private static int SearchSpace(
        int timeLeft,
        HashSet<string> closedNodes,
        string currentValve,
        Dictionary<string, Valve> valves,
        Dictionary<string, Dictionary<string, int>> distances,
        int currentPressure
    ) {
        if (timeLeft <= 0) return currentPressure;

        currentPressure += valves.Where(v => !closedNodes.Contains(v.Key)).Sum(v => v.Value.FlowRate);

        var newClosedNodes = new HashSet<string>(closedNodes);
        // We open the current value and then move to a neighbour
        if (closedNodes.Contains(currentValve)) {
            timeLeft -= 1;
            newClosedNodes.Remove(currentValve);

            if (newClosedNodes.Count == 0) {
                var finalFlowPerMinute = valves.Sum(v => v.Value.FlowRate);
                return currentPressure + finalFlowPerMinute * timeLeft;
            }
        }

        if (!distances.ContainsKey(currentValve)) {
            distances[currentValve] = GetPathCosts(valves, currentValve);
        }

        var distancesFromHere = distances[currentValve];
        
        var bestPressure = int.MinValue;
        var flowPerMinute = valves.Where(v => !newClosedNodes.Contains(v.Key)).Sum(v => v.Value.FlowRate);

        foreach (var valve in newClosedNodes) {
            var distance = distancesFromHere[valve];
            var result = currentPressure + timeLeft * flowPerMinute;
            if (distance < timeLeft) {
                result = SearchSpace(timeLeft - distance, newClosedNodes, valve, valves, distances,
                    currentPressure + distance * flowPerMinute);
            }

            if (result > bestPressure) {
                bestPressure = result;
            }
        }

        return bestPressure;
    }
    
    public static int SolveStar1(string inputFile = "day16/input.txt") {
        var valves = GetValves(inputFile).ToDictionary(v => v.Name, v => v);
        var distances = new Dictionary<string, Dictionary<string, int>>();
        var closedNodes = new HashSet<string>(valves.Values.Where(v => v.FlowRate > 0).Select(v => v.Name));
        var best = SearchSpace(30, closedNodes, "AA", valves, distances, 0);

        return best;
    }
    
    public static long SolveStar2(string inputFile = "day16/input.txt") {
        return -1;
    }
}