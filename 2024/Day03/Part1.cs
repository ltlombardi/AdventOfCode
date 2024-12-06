using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day03;
internal class Part1
{
    internal static async Task<string> Run()
    {
        var line = await File.ReadAllTextAsync(@"../../../Day03/Input.txt");
        var pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var match = Regex.Match(line, pattern);
        var sum = 0;
        while (match.Success)
        {
            sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            match = match.NextMatch();
        }
        return sum.ToString();
    }
}

