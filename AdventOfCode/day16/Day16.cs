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
        var distances = GetAllDistances(valves);
        var closedNodes = new HashSet<string>(valves.Values.Where(v => v.FlowRate > 0).Select(v => v.Name));
        var best = SearchSpace(30, closedNodes, "AA", valves, distances, 0);

        return best;
    }
    
    static IEnumerable<IEnumerable<T>> GetKCombs<T>(IEnumerable<T> list, int length) where T : IComparable
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetKCombs(list, length - 1)
            .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0), 
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    private static Dictionary<string, Dictionary<string, int>> GetAllDistances(Dictionary<string, Valve> valves) {
        var distances = new Dictionary<string, Dictionary<string, int>>();
        foreach (var kv in valves) {
            distances[kv.Key] = GetPathCosts(valves, kv.Key);
        }
        return distances;
    }
    
    public static long SolveStar2(string inputFile = "day16/input.txt") {
        var valves = GetValves(inputFile).ToDictionary(v => v.Name, v => v);
        var distances = GetAllDistances(valves);
        var valvesThatMatter = valves.Values.Where(v => v.FlowRate > 0).Select(v => v.Name).ToList();

        var best = -1;
        for (var split = 1; split <= valvesThatMatter.Count / 2; split++) {
            foreach (var set1Data in GetKCombs(valvesThatMatter, split)) {
                var set1 = new HashSet<string>(set1Data);
                var valves1 = valves.Where(v => set1.Contains(v.Key)).ToDictionary(kv => kv.Key, kv => kv.Value);
                var set2 = new HashSet<string>(valvesThatMatter);
                set2.ExceptWith(set1);
                var valves2 = valves.Where(v => set2.Contains(v.Key)).ToDictionary(kv => kv.Key, kv => kv.Value);
                var scoreSet1 = SearchSpace(26, new HashSet<string>(set1), "AA", valves1, distances, 0);
                var scoreSet2 = SearchSpace(26, set2, "AA", valves2, distances, 0);
                var totalScore = scoreSet1 + scoreSet2;
                if (totalScore > best) {
                    best = totalScore;
                }   
            }
        }
        
        return best;
    }
}