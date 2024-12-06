using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day04;
internal class Part1
{
    internal static async Task<string> Run()
    {
        var lines = await File.ReadAllLinesAsync(@"../../../Day04/Input.txt");
        var pattern = @"(?=(XMAS|SAMX))";
        var sum = 0;
        sum += Count(lines, pattern);
        sum += Count(Transpose(lines), pattern);
        sum += Count(GenerateDiagonalTopToBottom(lines), pattern);
        sum += Count(GenerateDiagonalBottomToTop(lines), pattern);
        return sum.ToString();
    }

    private static IEnumerable<string> GenerateDiagonalBottomToTop(string[] lines)
    {
        int rows = lines.Length;
        int cols = lines[0].Length;
        List<string> diagonals = new List<string>();
        var sb = new StringBuilder();

        // image each column being tilted to become a diagonal. Covers the upper triangle + midle diagonal
        for (int col = cols - 1; col >= 0; col--)
        {
            for (int row = 0, c = col; row < rows && c >= 0; row++, c--)
            {
                sb.Append(lines[row][c]);
            }
            diagonals.Add(sb.ToString());
            sb.Clear();
        }

        // image each row being tilted to become a diagonal, starting at second row. Covers bottom triangle
        for (int row = 1; row < rows; row++)
        {
            for (int r = row, col = cols - 1; r < rows && col >= 0; r++, col--)
            {
                sb.Append(lines[r][col]);
            }
            diagonals.Add(sb.ToString());
            sb.Clear();
        }

        return diagonals;
    }

    private static IEnumerable<string> GenerateDiagonalTopToBottom(string[] lines)
    {
        int rows = lines.Length;
        int cols = lines[0].Length;
        List<string> diagonals = new List<string>();
        var sb = new StringBuilder();

        // image each column being tilted to become a diagonal. Covers the upper triangle + midle diagonal
        for (int col = 0; col < cols; col++)
        {
            for (int row = 0, c = col; row < rows && c < cols; row++, c++)
            {
                sb.Append(lines[row][c]);
                //Console.WriteLine((row, c));
            }
            diagonals.Add(sb.ToString());
            sb.Clear();
        }

        // image each row being tilted to become a diagonal, starting at second row. Covers bottom triangle
        for (int row = 1; row < rows; row++)
        {
            for (int r = row, col = 0; r < rows && col < cols; r++, col++)
            {
                sb.Append(lines[r][col]);
                //Console.WriteLine((r, col));
            }
            diagonals.Add(sb.ToString());
            sb.Clear();
        }

        return diagonals;
    }

    private static List<string> Transpose(string[] lines)
    {
        var newLines = new List<string>();
        var sb = new StringBuilder();
        for (var col = 0; col < lines[0].Length; col++)
        {
            foreach (var line in lines)
            {
                sb.Append(line[col]);
            }
            newLines.Add(sb.ToString());
            sb.Clear();
        }
        return newLines;
    }

    private static int Count(IEnumerable<string> lines, string pattern)
    {
        var sum = 0;
        foreach (var line in lines)
        {
            sum += Regex.Count(line, pattern);
        }
        return sum;
    }
}

