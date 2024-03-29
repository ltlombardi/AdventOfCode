using System.Drawing;

class Day12Part1Solution2
{
    // This was my attempt using recursion. Don't know why it did work in the example and not with my input (never finished running)
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day12Input.txt");
        var result = 0;
        var grid = lines.Select(l => l.ToCharArray()).ToArray();

        var transposed = grid[0].Select((c, i) => grid.Select(row => row.ElementAt(i)).ToArray()).ToArray();

        var start = new Point();
        var end = new Point();
        for (int i = 0; i < transposed.Count(); i++)
        {
            for (int j = 0; j < transposed[i].Count(); j++)
            {
                if (transposed[i][j] == 'S')
                {
                    start = new Point(i, j);
                }
                if (transposed[i][j] == 'E')
                {
                    end = new Point(i, j);
                }
            }
        }

        var previous = new HashSet<Point> { };
        var previousGeneral = new HashSet<Point> { };
        var paths = BuildPaths(transposed, end, previous, previousGeneral);

        result = paths.Where(p => p.First() == 'S').Min(p => p.Count()) - 1; // remove first height
        return result.ToString();
    }

    static IEnumerable<string> BuildPaths(char[][] grid, Point current, ISet<Point> previous, ISet<Point> previousGeneral)
    {
        if (grid[current.X][current.Y] == 'S')
        {
            return new List<string> { "S" };
        }
        previous = new HashSet<Point>(previous); // influence next path, not past ones do to reference type.
        previous.Add(current);

        var neighbors = new[] {
            new Point(current.X + 1, current.Y),
            new Point(current.X - 1, current.Y),
            new Point(current.X, current.Y + 1),
            new Point(current.X, current.Y -1),
             };

        var currentHeight = GetHeight(grid, current);

        var validNextSteps = neighbors
            .Where(p =>
                IsOnGrid(p, grid[0].Count(), grid.Count())
                && IsNew(p, previous, previousGeneral)
                && IsAtMostOneHigher(currentHeight, GetHeight(grid, p)))
            .ToArray();

        var paths = new List<string>();
        foreach (var next in validNextSteps)
        {
            var pathSuffix = BuildPaths(grid, next, previous, previousGeneral);
            paths.AddRange(pathSuffix.Select(h => currentHeight.ToString() + h));
            previousGeneral.Add(current);
        }
        return paths;
    }

    private static bool IsOnGrid(Point p, int rows, int columns)
    {
        return p.X >= 0
            && p.Y >= 0
            && p.X < columns
            && p.Y < rows;
    }

    private static bool IsNew(Point p, ISet<Point> previous, ISet<Point> previousGeneral)
    {
        return !previous.Contains(p) && !previousGeneral.Contains(p);
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

    private static bool IsAtMostOneHigher(char currentHeight, char neighborHeight)
    {
        return neighborHeight == currentHeight || neighborHeight == currentHeight + 1;
    }
}
