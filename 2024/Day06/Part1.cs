namespace AdventOfCode2024.Day06;
internal class Part1
{
    internal static async Task<dynamic> Run()
    {
        var rawLines = await File.ReadAllLinesAsync(@"../../../Day06/Input.txt");
        var lines = rawLines.Select(x => x.ToCharArray()).ToArray();
        var pos = (row: -1, col: -1);
        for (var row = 0; row < lines.Length; row++)
        {
            for (var col = 0; col < lines[0].Length; col++)
            {
                if (lines[row][col] == '^')
                {
                    pos = (row, col);
                }
            }
        }
        var direction = Direction.Up;
        var visited = 0;
        while (pos.row < lines.Length && pos.row >= 0 && pos.col >= 0 && pos.col < lines[0].Length)
        {
            if (direction == Direction.Up)
            {
                if (lines[pos.row][pos.col] != '#')
                {
                    if (lines[pos.row][pos.col] != 'X')
                    {
                        lines[pos.row][pos.col] = 'X';
                        visited++;
                    }
                    pos.row--;
                }
                else
                {
                    pos.row++;
                    direction = Direction.Right;
                }
            }
            else if (direction == Direction.Right)
            {
                if (lines[pos.row][pos.col] != '#')
                {
                    if (lines[pos.row][pos.col] != 'X')
                    {
                        lines[pos.row][pos.col] = 'X';
                        visited++;
                    }
                    pos.col++;
                }
                else
                {
                    pos.col--;
                    direction = Direction.Down;
                }
            }
            else if (direction == Direction.Down)
            {
                if (lines[pos.row][pos.col] != '#')
                {
                    if (lines[pos.row][pos.col] != 'X')
                    {
                        lines[pos.row][pos.col] = 'X';
                        visited++;
                    }
                    pos.row++;
                }
                else
                {
                    pos.row--;
                    direction = Direction.Left;
                }
            }
            else if (direction == Direction.Left)
            {
                if (lines[pos.row][pos.col] != '#')
                {
                    if (lines[pos.row][pos.col] != 'X')
                    {
                        lines[pos.row][pos.col] = 'X';
                        visited++;
                    }
                    pos.col--;
                }
                else
                {
                    pos.col++;
                    direction = Direction.Up;
                }
            }
        }
        foreach (var line in lines)
        {
            foreach (var letter in line)
            {
                Console.Write(letter);
            }
            Console.WriteLine();
        }
        return visited;
    }

    enum Direction
    {
        Left, Right, Up, Down
    }
}

