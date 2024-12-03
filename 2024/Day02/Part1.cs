using static System.Net.Mime.MediaTypeNames;
using System.Runtime.ConstrainedExecution;

namespace AdventOfCode2024.Day02;
internal class Part1
{
    internal static async Task<string> Run()
    {
        var lines = await File.ReadAllLinesAsync(@"../../../Day02/Input.txt");
        var numberOfSafeReports = 0;
        foreach (var line in lines)
        {
            var levels = line.Split(' ').Select(int.Parse).ToList();
            var isSafe = true;
            var initialDirection = levels[1] - levels[0] > 0;
            for (var i = 0; i < levels.Count - 1; i++)
            {
                var cur = levels[i];
                var next = levels[i + 1];
                var direction = next - cur > 0;
                isSafe &= Math.Abs(next - cur) >= 1 && Math.Abs(next - cur) <= 3 && (direction == initialDirection);
            }
            if (isSafe) numberOfSafeReports++;
        }
        return numberOfSafeReports.ToString();
    }
}

