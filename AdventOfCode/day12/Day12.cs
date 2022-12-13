using AdventOfCode.utils;

namespace AdventOfCode.day12; 

public static class Day12 {

    private class Route {
        public (int, int, char) Position { get; private set; }
        public HashSet<(int, int, char)> Traversed { get; private set; }

        public Route() : this(new HashSet<(int, int, char)>()) {
            
        }

        public Route(HashSet<(int, int, char)> traversed) {
            Traversed = new HashSet<(int, int, char)>(traversed);
        }
        
        public void TravelTo((int, int, char) position) {
            Position = position;
            Traversed.Add(position);
        }
    }
    
    private class LengthCompare : IComparer<int> {
        public int Compare(int x, int y) => x.CompareTo(y);
    }

    private static int GetHeight(char ch) {
        if (ch == 'S') ch = 'a';
        if (ch == 'E') ch = 'z';
        return ch - 'a';
    }

    private static int SolveFromStart(Grid<char> grid, (int, int, char) start) {
        var seen = new HashSet<(int, int, char)>();
        var routes = new PriorityQueue<Route, int>(new LengthCompare());
        var startRoute = new Route();
        startRoute.TravelTo(start);
        routes.Enqueue(startRoute, startRoute.Traversed.Count);
        seen.Add(start);

        Route? shortestRoute = null;
        while (shortestRoute == null || shortestRoute.Position.Item3 != 'E') {
            shortestRoute = routes.Dequeue();
            foreach (var neighbour in grid.IterateNeighbours((shortestRoute.Position.Item1, shortestRoute.Position.Item2))) {
                if (seen.Contains(neighbour)) continue;
                
                if (shortestRoute.Traversed.Contains(neighbour)) continue;
                var (x, y, test) = neighbour;
                var currentHeight = GetHeight(shortestRoute.Position.Item3);
                var neighbourHeight = GetHeight(test);
                if (neighbourHeight - currentHeight > 1) {
                    continue;
                }

                var newRoute = new Route(shortestRoute.Traversed);
                newRoute.TravelTo(neighbour);
                routes.Enqueue(newRoute, newRoute.Traversed.Count);
                seen.Add(neighbour);

                if (test == 'E') break;
            }
            
        }

        return shortestRoute?.Traversed.Count - 1 ?? int.MaxValue ;
    }
    
    public static int SolveStar1(string inputFile = "day12/input.txt") {
        var grid = new Grid<char>(File.ReadAllLines(inputFile), x => x);
        var start = grid.Enumerate().First((x) => x.Item3 == 'S');
        return SolveFromStart(grid, start);
    }

    public static int SolveStar2(string inputFile = "day12/input.txt") {
        var grid = new Grid<char>(File.ReadAllLines(inputFile), x => x);
        var shortestFound = int.MaxValue;
        foreach (var position in grid.Enumerate().Where(x => GetHeight(x.Item3) == 0)) {
            try {
                var steps = SolveFromStart(grid, position);
                if (steps < shortestFound) {
                    shortestFound = steps;
                }   
            } catch {}
        }

        return shortestFound;
    }
}