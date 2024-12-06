using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day03;
internal class Part2
{
    internal static async Task<string> Run()
    {
        var line = await File.ReadAllTextAsync(@"../../../Day03/Input.txt");
        var pattern = @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)";
        var match = Regex.Match(line, pattern);
        var sum = 0;
        var isEnabled = true;
        while (match.Success)
        {
            if (match.Value.Equals("do()"))
            {
                isEnabled = true;
            }
            else if (match.Value.Equals("don't()"))
            {
                isEnabled = false;
            }
            else if (isEnabled && match.Value.StartsWith("mul"))
            {
                sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }
            match = match.NextMatch();
        }
        return sum.ToString();
    }
}

