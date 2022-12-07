namespace AdventOfCode.day7; 

public static class Day7 {
    
    private static Dir ParseDirInfo(string inputFile) {
        var rootDir = new Dir("/", null);
        var curDir = rootDir;
        foreach (var line in File.ReadLines(inputFile)) {
            if (line.StartsWith("$ cd ")) {
                var dirName = line.Substring("$ cd ".Length);
                if (dirName == "/") {
                    curDir = rootDir;
                } else if (dirName == "..") {
                    curDir = curDir.Parent;
                }
                else {
                    curDir = curDir.Children.Find(d => d.Name == dirName);
                }
                continue;
            }
            
            if (line == "$ ls") continue;

            var split = line.Split();
            if (split[0] == "dir") {
                var child = new Dir(split[1], curDir);
                curDir.Children.Add(child);
                continue;
            }

            var file = new FileItem(long.Parse(split[0]), split[1]);
            curDir.AddFileItem(file);
        }
        return rootDir;
    }
    
    public static long SolveStar1(string inputFile = "day7/input.txt") {
        var root = ParseDirInfo(inputFile);
        var dirs = new List<Dir>();
        foreach (var dir in root.WalkDirs()) {
            if (dir.CachedSize <= 100000) {
                dirs.Add(dir);
            }
        }

        return dirs.Sum(d => d.CachedSize);
    }

    public static long SolveStar2(string inputFile = "day7/input.txt") {
        var totalSpace = 70000000L;
        var updateSpace = 30000000L;
        var root = ParseDirInfo(inputFile);
        var freeSpace = totalSpace - root.CachedSize;
        var neededSpace = updateSpace - freeSpace;

        Dir? candidate = null;
        foreach (var dir in root.WalkDirs()) {
            if (dir.CachedSize >= neededSpace && (candidate == null || candidate.CachedSize > dir.CachedSize)) {
                candidate = dir;
            }
        }
        
        return candidate!.CachedSize;
    }
}