using System.Drawing;
using System.Text;

class Day14Part1
{
    //TODO: clean and improve... see if there is a way to avoid so much problems with index out of bounds in the grid..
    // should be maxY and MaxX not the way i put
    // look into this to improve https://stackoverflow.com/questions/15730346/array-with-negative-indexes
    // for this problem, that points are very far alway from point (0,0), and grid is sparcely populated, it's better to use dictionary

    // this is also interesting     // this helped me https://stackoverflow.com/questions/11837139/implementation-of-array-with-negative-indices
    internal static async Task<string> Solution()
    {
        // discovered that this code works on VS but not in VSCode.. Would need to check to see if file exist to use one or another..
        // var projectDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName; // so it works in VS2022 without having to mark every txt file are copy always.
        // var lines = await File.ReadAllLinesAsync(projectDir + "\\Day14Input.txt");
        var lines = await File.ReadAllLinesAsync("Day14Input.txt");
        var result = 0;
        var paths = lines.Select(l => l.Split(" -> ").Select(PointConverter).ToList()).ToList();
        var yMax = paths.SelectMany(p => p.Select(p => p.Y)).Max();
        var xMin = paths.SelectMany(p => p.Select(p => p.X)).Min();
        var xMax = paths.SelectMany(p => p.Select(p => p.X)).Max();
        // make points x axis zero based to match grid.
        paths = paths.Select(pa => pa.Select(po => new Point(po.X -= xMin, po.Y)).ToList()).ToList();
        int xLength = xMax - xMin + 1;
        int yLength = yMax + 1;
        var grid = new char[xLength, yLength];
        var sandOrigin = new Point(500 - xMin, 0);

        AddAir(grid);
        AddRocks(paths, grid);
        result = MakeItFlowSand(grid, sandOrigin);
        Print(grid);
        return result.ToString();
    }

    private static int MakeItFlowSand(char[,] grid, Point sandOrigin)
    {
        grid[sandOrigin.X, 0] = '+'; // add origin of sand
        var counter = -1;
        for (var isFlowingToAbyss = false; !isFlowingToAbyss; counter++)
        {
            var x = sandOrigin.X;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                var nextY = y + 1;
                if (x + 1 >= grid.GetLength(0) || x - 1 < 0 || nextY >= grid.GetLength(1))
                {
                    isFlowingToAbyss = true;
                    break;
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
                        if (y != sandOrigin.Y)
                        {
                            grid[x, y] = 'O';
                        }
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
