using System.Drawing;

class Day12Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day12Input.txt");
        var result = 0;
        var grid = lines.Select(l => l.ToCharArray()).ToArray();
        // Transpose so when accessing the grid via grid[][], the indexes use the x,y order of the cartesian plan
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

        var queue = new Queue<Point> { };
        var visited = new bool[transposed.Count(), transposed[0].Count()];
        var distance = new int[transposed.Count(), transposed[0].Count()];
        var parents = new Point[transposed.Count(), transposed[0].Count()];

        queue.Enqueue(start);
        visited[start.X, start.Y] = true;
        // parents[start.X, start.Y] = -1;
        while (queue.Any())
        {
            var current = queue.Dequeue();

            var neighbors = new[] {
                new Point(current.X + 1, current.Y),
                new Point(current.X - 1, current.Y),
                new Point(current.X, current.Y + 1),
                new Point(current.X, current.Y -1),
             };

            var currentHeight = GetHeight(transposed, current);

            var validNeighbors = neighbors
                .Where(p =>
                    IsOnGrid(p, transposed[0].Count(), transposed.Count())
                    && IsAtMostOneHigher(currentHeight, GetHeight(transposed, p)))
                .ToArray();

            foreach (var item in validNeighbors)
            {
                if (!visited[item.X, item.Y])
                {
                    visited[item.X, item.Y] = true;
                    queue.Enqueue(item);
                    distance[item.X, item.Y] = distance[current.X, current.Y] + 1;
                    parents[item.X, item.Y] = current;
                }
            }
        }
        result = distance[end.X, end.Y];
        return result.ToString();
    }

    private static bool IsOnGrid(Point p, int rows, int columns)
    {
        return p.X >= 0
            && p.Y >= 0
            && p.X < columns
            && p.Y < rows;
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
