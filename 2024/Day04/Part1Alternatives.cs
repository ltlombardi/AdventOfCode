using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day04;
internal class Part1Alternatives
{
    public static IEnumerable<string> GenerateSlashDiagInOrder(string[] lines)
    {
        // This generates the diagonals in order, starting at the left bottom, with a single letter diagonal, going up,
        // and finishing with the diagonal in the upper right, with single letter..
        var newLines = new List<string>();

        var sb = new StringBuilder();
        for (var j = lines.Length - 1; j > -lines.Length; j--)
        {
            for (var x = (j < 0 ? 0 - j : 0); x < lines.Length - (j < 0 ? 0 : j); x++)
            {
                sb.Append(lines[x + j][x]);
                Console.WriteLine((x, x + j));
            }
            newLines.Add(sb.ToString());
        }
        return newLines;
    }

    public static IEnumerable<string> GenerateSlashDiagInOrderClearer(string[] lines)
    {
        var diagonals = new List<string>();

        for (var offset = lines.Length - 1; offset > -lines.Length; offset--)
        {
            var diagonal = GetDiagonalForOffset(lines, offset);
            diagonals.Add(diagonal);
        }

        return diagonals;
    }

    private static string GetDiagonalForOffset(string[] lines, int offset)
    {
        var sb = new StringBuilder();

        foreach (var (row, col) in GetDiagonalCoordinates(lines.Length, offset))
        {
            sb.Append(lines[row][col]);
            Console.WriteLine((row, col)); // Debug information
        }

        return sb.ToString();
    }

    private static IEnumerable<(int row, int col)> GetDiagonalCoordinates(int length, int offset)
    {
        var start = offset < 0 ? -offset : 0;
        var end = length - (offset < 0 ? 0 : offset);

        for (var x = start; x < end; x++)
        {
            yield return (x, x + offset);
        }
    }


}
