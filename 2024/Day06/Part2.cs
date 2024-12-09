using System;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;

namespace AdventOfCode2024.Day06;
internal class Part2
{
    internal static async Task<dynamic> Run()
    {
        var rawLines = await File.ReadAllLinesAsync(@"../../../Day06/Input.txt");
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
        var initialPos = pos;
        var direction = Direction.Up;
        var options = new HashSet<Position>();

        while (InsideMap(lines, pos))
        {
            if (lines[pos.row][pos.col] == '#')
            {
                pos = GoBack(pos, direction);
                direction = ChangeDirection(direction);
            }

            pos = Advance(pos, direction);
            if (pos != initialPos)
                CheckPossibleLoop(lines, pos, direction, options);
        }

        return options.Count;
    }

    private static Position GoBack(Position pos, char direction)
    {
        if (direction == Direction.Up)
        {
            pos.row++;
        }
        else if (direction == Direction.Right)
        {
            pos.col--;
        }
        else if (direction == Direction.Down)
        {
            pos.row--;

        }
        else if (direction == Direction.Left)
        {
            pos.col++;
        }
        return pos;
    }

    private static char ChangeDirection(char direction)
    {
        if (direction == Direction.Up)
        {
            direction = Direction.Right;
        }
        else if (direction == Direction.Right)
        {

            direction = Direction.Down;
        }
        else if (direction == Direction.Down)
        {

            direction = Direction.Left;

        }
        else if (direction == Direction.Left)
        {

            direction = Direction.Up;
        }
        return direction;
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
        var seenObstacle = new HashSet<(Position, char)>();
        while (InsideMap(lines, pos))
        {
            if (lines[pos.row][pos.col] == '#' || pos == fakeObstacle)
            {
                if (!seenObstacle.Add((pos, direction)))
                {
                    options.Add(fakeObstacle);
                    return;
                };
                pos = GoBack(pos, direction);
                direction = ChangeDirection(direction);
            }
            pos = Advance(pos, direction);
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
}