namespace AdventOfCode.day8; 

public static class Day8 {
    
    public static int SolveStar1(string inputFile = "day8/input.txt") {
        var grid = new Grid<Tree>(File.ReadAllLines(inputFile), c => new Tree(c));

        // Walk from the left
        for (var y = 0; y < grid.Height; y++) {
            var maxHeight = int.MaxValue;
            for (var x = 0; x < grid.Width; x++) {
                var tree = grid.Items[x, y];
                if (x == 0) {
                    tree.VisibleLeft = true;
                    maxHeight = tree.Height;
                    continue;
                }

                if (tree.Height > maxHeight) {
                    tree.VisibleLeft = true;
                    maxHeight = tree.Height;
                }
            }
        }
        
        // Walk from the right
        for (var y = 0; y < grid.Height; y++) {
            var maxHeight = int.MaxValue;
            for (var x = grid.Width - 1; x >= 0; x--) {
                var tree = grid.Items[x, y];
                if (x == grid.Width - 1) {
                    tree.VisibleRight = true;
                    maxHeight = tree.Height;
                    continue;
                }

                if (tree.Height > maxHeight) {
                    tree.VisibleRight = true;
                    maxHeight = tree.Height;
                }
            }
        }
        
        // Walk from the top
        for (var x = 0; x < grid.Width; x++) {
            var maxHeight = int.MaxValue;
            for (var y = 0; y < grid.Height; y++) {
                var tree = grid.Items[x, y];
                if (y == 0) {
                    tree.VisibleTop = true;
                    maxHeight = tree.Height;
                    continue;
                }

                if (tree.Height > maxHeight) {
                    tree.VisibleTop = true;
                    maxHeight = tree.Height;
                }
            }
        }
        
        // Walk from the bottom
        for (var x = 0; x < grid.Width; x++) {
            var maxHeight = int.MaxValue;
            for (var y = grid.Height - 1; y >= 0; y--) {
                var tree = grid.Items[x, y];
                if (y == grid.Height - 1) {
                    tree.VisibleBottom = true;
                    maxHeight = tree.Height;
                    continue;
                }

                if (tree.Height > maxHeight) {
                    tree.VisibleBottom = true;
                    maxHeight = tree.Height;
                }
            }
        }


        foreach (var (x, y, tree) in grid.Enumerate()) {
            if (x == 0) {
                tree.VisibleLeft = true;
            }

            if (x == grid.Width - 1) {
                tree.VisibleRight = true;
            }

            if (y == 0) {
                tree.VisibleTop = true;
            }

            if (y == grid.Height - 1) {
                tree.VisibleBottom = true;
            }

            var walkLeft = x - 1;
            while (walkLeft >= 0) {
                var test = grid.Items[walkLeft, y];
                if (test.Height < tree.Height && tree.VisibleLeft) {
                    
                }
                walkLeft--;
            }
        }

        return grid.EnumerateItems().Count(x => x.Visible);
    }

    public static int SolveStar2(string inputFile = "day8/input.txt") {
        var grid = new Grid<Tree2>(File.ReadAllLines(inputFile), c => new Tree2(c));

        foreach (var (x, y, tree) in grid.Enumerate()) {
            var walkLeft = x - 1;
            while (walkLeft >= 0) {
                tree.VisibleLeft++;
                var test = grid.Items[walkLeft, y];
                if (test.Height >= tree.Height) {
                    break;
                }
                walkLeft--;
            }

            var walkRight = x + 1;
            while (walkRight < grid.Width) {
                tree.VisibleRight++;
                var test = grid.Items[walkRight, y];
                if (test.Height >= tree.Height) {
                    break;
                }
                walkRight++;
            }

            var walkUp = y - 1;
            while (walkUp >= 0) {
                tree.VisibleTop++;
                var test = grid.Items[x, walkUp];
                if (test.Height >= tree.Height) {
                    break;
                }
                walkUp--;
            }

            var walkDown = y + 1;
            while (walkDown < grid.Height) {
                tree.VisibleBottom++;
                var test = grid.Items[x, walkDown];
                if (test.Height >= tree.Height) {
                    break;
                }
                walkDown++;
            }
        }

        return grid.EnumerateItems().Select(t => t.ViewScore).Max();
    }
}