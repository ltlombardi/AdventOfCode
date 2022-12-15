using System.Drawing;
using System.Text;

class Day14Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day14Input.txt");
        var result = 0;
        var paths = lines.Select(l => l.Split(" -> ").Select(PointConverter).ToList()).ToList();

        var yMax = paths.SelectMany(p => p.Select(p => p.Y)).Max();

        yMax += 2;
        int yLength = yMax + 1;
        var triangleHeight = yLength - 1;
        var floorLength = (2 * triangleHeight) - 1; // check if this is like a general triangle property
        floorLength += 4; //give an extra 2 spaces to print good.

        var xMax = 500 + floorLength / 2;

        var xMin = 500 - floorLength / 2;
        // make points x axis zero based to match grid.
        paths = paths.Select(pa => pa.Select(po => new Point(po.X -= xMin, po.Y)).ToList()).ToList();
        // observing the example, its possible to get the measurements of the triangle formed by the sand
        // to get the dimension of the grid required to simulate.

        int xLength = floorLength;

        var grid = new char[xLength, yLength];
        var sandOrigin = new Point(500 - xMin, 0);

        AddAir(grid);
        AddRocks(paths, grid);
        AddFloorRocks(grid);
        result = MakeItFlowSand(grid, sandOrigin);
        Print(grid);
        return result.ToString();
    }

    private static int MakeItFlowSand(char[,] grid, Point sandOrigin)
    {
        grid[sandOrigin.X, 0] = '+'; // add origin of sand
        var counter = 0; // Because now, the last sand bill be on top of the origin
        for (var hasReachedOrigin = false; !hasReachedOrigin; counter++)
        {
            var x = sandOrigin.X;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                var nextY = y + 1;
                if (x + 1 >= grid.GetLength(0) || x - 1 < 0 || nextY >= grid.GetLength(1))
                {
                    throw new Exception("this should not be possible");
                }
                if (grid[x, nextY] == '.') { continue; }
                else if (grid[x, nextY] == 'O' || grid[x, nextY] == '#')
                {
                    if (grid[x - 1, nextY] == '.')
                    {
                        x -= 1;
                    }
                    else if (grid[x + 1, nextY] == '.')
                    {
                        x += 1;
                    }
                    else
                    {
                        grid[x, y] = 'O';
                        hasReachedOrigin = y == sandOrigin.Y;
                        break;
                    }
                }
            }
        }
        return counter;
    }

    private static void AddRocks(List<List<Point>> paths, char[,] grid)
    {
        foreach (var points in paths)
        {
            if (points.Count() == 1)
            {
                grid[points[0].X, points[0].Y] = '#';
                continue;
            }
            for (int i = 1; i < points.Count(); i++)
            {
                var xLineLength = points[i].X - points[i - 1].X;
                var yLineLength = points[i].Y - points[i - 1].Y;
                var lineLength = xLineLength + yLineLength; // one of the axis is always zero
                var absLineLength = Math.Abs(lineLength);
                var direction = lineLength / absLineLength;

                for (int j = 0; j <= absLineLength; j++)
                {
                    var k = j * direction;
                    if (xLineLength != 0)
                    {
                        grid[points[i - 1].X + k, points[i - 1].Y] = '#';
                    }
                    else
                    {
                        grid[points[i - 1].X, points[i - 1].Y + k] = '#';
                    }
                }
            }
        }
    }

    private static void AddFloorRocks(char[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            grid[i, grid.GetLength(1) - 1] = '#';
        }
    }

    private static void AddAir(char[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = '.';
            }
        }
    }

    private static void Print(char[,] grid)
    {
        var sb = new StringBuilder();
        for (int row = 0; row < grid.GetLength(1); row++)
        {
            Console.Write($"{row}\t\t\t");
            for (int column = 0; column < grid.GetLength(0); column++)
            {
                Console.Write($"{grid[column, row]}");
            }
            Console.Write(Environment.NewLine);
        }
    }

    private static Point PointConverter(string p)
    {
        var parts = p.Split(',')
            .Select(int.Parse)
            .ToList();
        return new Point(parts[0], parts[1]);
    }

}
