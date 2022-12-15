using System.Drawing;
using System.Text;

class Day14Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day14Input.txt");
        var result = 0;
        var paths = lines.Select(l => l.Split(" -> ").Select(PointConverter).ToList()).ToList();
        var yMax = paths.SelectMany(p => p.Select(p => p.Y)).Max();
        var xMin = paths.SelectMany(p => p.Select(p => p.X)).Min();
        var xMax = paths.SelectMany(p => p.Select(p => p.X)).Max();
        paths = paths.Select(pa => pa.Select(po => new Point(po.X -= xMin, po.Y)).ToList()).ToList(); // make point x axis zero based to match grid

        int xSize = xMax - xMin + 1;
        int ySize = yMax + 1;
        var grid = new char[xSize, ySize];
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = '.';
            }
        }
        grid[500 - xMin, 0] = '+';

        foreach (var points in paths)
        {
            if (points.Count() == 1)
            {
                grid[points[0].X, points[0].Y] = '#';
                continue;
            }
            for (int i = 1; i < points.Count(); i++)
            {
                var xRange = points[i].X - points[i - 1].X;
                var yRange = points[i].Y - points[i - 1].Y;
                var distance = xRange + yRange;
                var length = Math.Abs(distance);
                var direction = distance / length;

                for (int j = 0; j < length; j++)
                {
                    var k = j * direction;
                    if (xRange != 0)
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
        Print(grid, xMin, xMax);
        return result.ToString();
    }

    private static void Print(char[,] grid, int minX, int maxX)
    {
        Console.WriteLine($"Min x: {minX}, Max x: {maxX}");
        var sb = new StringBuilder();
        for (int row = 0; row < grid.GetLength(1); row++)
        {
            Console.Write($"{row} ");
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
