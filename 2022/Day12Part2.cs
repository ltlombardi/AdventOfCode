using System.Drawing;

class Day12Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day12Input.txt");
        var result = 0;
        var grid = lines.Select(l => l.ToCharArray()).ToArray();
        // Transpose so when accessing the grid via grid[][], the indexes use the x,y order of the cartesian plan
        var transposed = grid[0].Select((c, i) => grid.Select(row => row.ElementAt(i)).ToArray()).ToArray();
        var (start, end) = FindStartEndPoints(transposed);

        // This uses BFS (Breadth First Search) algorithm.
        // As a result of how the algorithm works, the path found by breadth first search to any node is the 
        // shortest path to that node, i.e the path that contains the smallest number of edges in unweighted graphs.
        // explanation: https://cp-algorithms.com/graph/breadth-first-search.html#implementation

        // Because of how BFS works, by starting at the End point, the smaller path produced will be the solution.
        // So I can get the distance to every point with height 'a' and get the minimum, or traverse 

        var queue = new Queue<Point> { };
        var visited = new bool[transposed.Count(), transposed[0].Count()];
        var distance = new int[transposed.Count(), transposed[0].Count()];
        var parents = new Point[transposed.Count(), transposed[0].Count()];

        queue.Enqueue(end);
        visited[end.X, end.Y] = true;

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

        result = GetResult(transposed, distance,visited);
        return result.ToString();
    }

    private static int GetResult(char[][] grid, int[,] distance, bool[,] visited)
    {
        // Find all potential starting points (elevation == 0 / 'a' or 'S')
        var startingPoints = FindStartingPoints(grid);

        // Find the shortest path from any of the starting points
        var shortestPathLength = int.MaxValue;
        foreach (var startingPoint in startingPoints.Where(s=> visited[s.X, s.Y]))
        {
            var dir = distance[startingPoint.X, startingPoint.Y];
            if (dir < shortestPathLength)
            {
                shortestPathLength = dir;
            }
        }
        return shortestPathLength;
    }

    private static IEnumerable<Point> FindStartingPoints(char[][] grid)
    {
        for (var row = 0; row < grid[0].Count(); row++)
        {
            for (var col = 0; col < grid.Count(); col++)
            {
                if (grid[col][row] == 'a' || grid[col][row] == 'S')
                {
                    yield return new Point(col, row);
                }
            }
        }
    }

    private static (Point, Point) FindStartEndPoints(char[][] transposed)
    {
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
        return (start, end);
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
        // this is the opposite of part 1
        return neighborHeight >= currentHeight || neighborHeight == currentHeight - 1;
    }
}
