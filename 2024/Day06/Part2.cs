using System;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;

namespace AdventOfCode2024.Day06;
internal class Part2
{
    internal static async Task<dynamic> Run()
    {
        var rawLines = await File.ReadAllLinesAsync(@"../../../Day06/ExampleInput.txt");
        var lines = rawLines.Select(x => x.ToCharArray()).ToArray();
        var pos = new Position(-1, -1);
        for (var row = 0; row < lines.Length; row++)
        {
            for (var col = 0; col < lines[0].Length; col++)
            {
                if (lines[row][col] == '^')
                {
                    pos = new Position(row, col);
                }
            }
        }
        var direction = Direction.Up;
        var options = new HashSet<Position>();

        var optionsTask = new List<Task>();
        while (InsideMap(lines, pos))
        {
            if (lines[pos.row][pos.col] == '#')
            {
                GoBackAndChangeDirection(ref pos, ref direction);
            }
            else
            {
                //optionsTask.Add(Task.Run(() => CheckPossibleLoop(lines, pos, direction, options)));
                CheckPossibleLoop(lines, pos, direction, options);

                pos = Advance(pos, direction);
            }
        }
        //await Task.WhenAll(optionsTask);
        //Print(lines, pos);
        return options.Count;
    }

    private static void GoBackAndChangeDirection(ref Position pos, ref char direction)
    {
        if (direction == Direction.Up)
        {
            pos.row++;
            direction = Direction.Right;
        }
        else if (direction == Direction.Right)
        {
            pos.col--;
            direction = Direction.Down;
        }
        else if (direction == Direction.Down)
        {
            pos.row--;
            direction = Direction.Left;

        }
        else if (direction == Direction.Left)
        {
            pos.col++;
            direction = Direction.Up;
        }
    }

    private static Position Advance(Position pos, char direction)
    {
        if (direction == Direction.Up)
        {
            pos.row--;
        }
        else if (direction == Direction.Right)
        {
            pos.col++;
        }
        else if (direction == Direction.Down)
        {
            pos.row++;
        }
        else if (direction == Direction.Left)
        {
            pos.col--;
        }

        return pos;
    }

    private static void CheckPossibleLoop(char[][] lines, Position pos, char direction, HashSet<Position> options)
    {
        var fakeObstacle = pos;
        var visited = new HashSet<(Position, char)>();
        while (InsideMap(lines, pos))
        {
            if (visited.Contains((pos, direction)))
            {
                options.Add(fakeObstacle);
                return;
            }
            if (lines[pos.row][pos.col] == '#' || pos == fakeObstacle)
            {
                GoBackAndChangeDirection(ref pos, ref direction);
            }
            else
            {
                visited.Add((pos, direction));
                pos = Advance(pos, direction);
            }
        }
    }

    private static void Print(char[][] lines, Position position)
    {
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++)
            {
                var letter = line[j];
                if (new Position(i, j) == position)
                {
                    letter = 'O';
                }
                Console.Write(letter);
            }
            Console.WriteLine();
        }
        Console.WriteLine("_____________________");
    }

    private static bool InsideMap(char[][] lines, Position pos)
    {
        return pos.row < lines.Length && pos.row >= 0 && pos.col >= 0 && pos.col < lines[0].Length;
    }

    static class Direction
    {
        public static char Left = '<';
        public static char Right = '>';
        public static char Up = '^';
        public static char Down = 'v';
    }
}

internal record struct Position(int row, int col)
{
    public static implicit operator (int row, int col)(Position value)
    {
        return (value.row, value.col);
    }

    public static implicit operator Position((int row, int col) value)
    {
        return new Position(value.row, value.col);
    }
}