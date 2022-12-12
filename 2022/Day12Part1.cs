using System.Drawing;

class Day12Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day12Input.txt");
        var result = 0;
        var grid = lines.Select(l => l.ToCharArray()).ToArray();

        var start = new Point();
        var paths = Move(grid, start);

        result = paths.Min(p => p.Count());
        return result.ToString();
    }

    static List<string> Move(char[][] grid, Point current)
    {
        var currentHeight = GetHeight(grid, current);
        if (currentHeight == 'E')
        {
            return new List<string> { "E" };
        }

        var paths = new List<string>();
        var neighbors = new[] {
            new Point(current.X + 1, current.Y),
            new Point(current.X - 1, current.Y),
            new Point(current.X, current.Y + 1),
            new Point(current.X, current.Y -1),
             };
        foreach (var neighbor in neighbors.Where(p => Valid(p, grid.Count())))
        {
            paths.AddRange(GetPaths(grid, currentHeight, neighbor));
        }
        return paths;
    }

    private static bool Valid(Point p, int size)
    {
        return (p.X >= 0 && p.Y >= 0) && ((p.X < size && p.Y < size));
    }

    private static IEnumerable<string> GetPaths(char[][] grid, char currentHeight, Point neighbor)
    {
        var neighborHeight = GetHeight(grid, neighbor);
        if (IsOneHigher(currentHeight, neighborHeight))
        {
            return Move(grid, neighbor).Select(h => currentHeight.ToString() + h);
        }
        return new string[0];
    }

    private static char GetHeight(char[][] grid, Point p)
    {
        if (grid[p.X][p.Y] == 'S')
        {
            return 'a';
        }
        if (grid[p.X][p.Y] == 'E')
        {
            return 'z';
        }
        return grid[p.X][p.Y];
    }

    private static bool IsOneHigher(char currentHeight, char neighborHeight)
    {
        return neighborHeight == currentHeight || neighborHeight == currentHeight + 1;
    }
}
