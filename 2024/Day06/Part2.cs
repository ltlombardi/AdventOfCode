using System;
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
        var options = 0;

        while (InsideMap(lines, pos))
        {
            if (direction == Direction.Up)
            {
                if (lines[pos.row][pos.col] != '#')
                {
                    if (lines[pos.row][pos.col] == Direction.Right)
                    {
                        options++;
                        lines[pos.row - 1][pos.col] = 'O';
                        Print(lines);
                    }

                    lines[pos.row][pos.col] = Direction.Up;
                    pos.row--;
                }
                else
                {
                    pos.row++;
                    lines[pos.row][pos.col] = Direction.Right;
                    direction = Direction.Right;
                }
            }
            else if (direction == Direction.Right)
            {
                if (lines[pos.row][pos.col] != '#')
                {
                    if (lines[pos.row][pos.col] == Direction.Down)
                    {
                        options++;
                        lines[pos.row][pos.col + 1] = 'O';
                        Print(lines);
                    }

                    lines[pos.row][pos.col] = Direction.Right;
                    pos.col++;
                }
                else
                {
                    pos.col--;
                    lines[pos.row][pos.col] = Direction.Down;
                    direction = Direction.Down;
                }
            }
            else if (direction == Direction.Down)
            {
                if (lines[pos.row][pos.col] != '#')
                {
                    if (lines[pos.row][pos.col] == Direction.Left)
                    {
                        options++;
                        lines[pos.row + 1][pos.col] = 'O';
                        Print(lines);
                    }

                    lines[pos.row][pos.col] = Direction.Down;
                    pos.row++;
                }
                else
                {
                    pos.row--;
                    lines[pos.row][pos.col] = Direction.Left;
                    direction = Direction.Left;
                }
            }
            else if (direction == Direction.Left)
            {
                if (lines[pos.row][pos.col] != '#')
                {
                    if (lines[pos.row][pos.col] == Direction.Up)
                    {
                        options++;
                        lines[pos.row][pos.col - 1] = 'O';
                        Print(lines);
                    }
                    lines[pos.row][pos.col] = Direction.Left;
                    pos.col--;
                }
                else
                {
                    pos.col++;
                    lines[pos.row][pos.col] = Direction.Up;
                    direction = Direction.Up;
                }
            }
            Print(lines);
        }

        return options;
    }

    private static void Print(char[][] lines)
    {
        foreach (var line in lines)
        {
            foreach (var letter in line)
            {
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