namespace AdventOfCode2024.Day02;
internal class Part2
{
    internal static async Task<string> Run()
    {
        var lines = await File.ReadAllLinesAsync(@"../../../Day02/Input.txt");
        var numberOfSafeReports = 0;
        foreach (var line in lines)
        {
            var levels = line.Split(' ').Select(int.Parse).ToList();
            var isSafe = true;
            var marginUsed = false;
            var levelToSkip = -1;
            var initialDirection = levels[1] - levels[0] > 0;
            for (var i = 0; i < levels.Count - 1; i++)
            {
                var cur = levels[i];
                if (i == levelToSkip)
                {
                    cur = levels[i - 1];
                }
                var next = levels[i + 1];
                var res = IsSafe(initialDirection, cur, next);
                if (!res && !marginUsed)
                {
                    levelToSkip = i + 1;
                    if (levelToSkip == levels.Count - 1) continue;
                    res = IsSafe(initialDirection, cur, levels[i + 2]);
                    marginUsed = true;
                }
                isSafe &= res;
            }
            if (isSafe) numberOfSafeReports++;
        }
        return numberOfSafeReports.ToString();
    }

    private static bool IsSafe(bool initialDirection, int cur, int next)
    {
        var direction = next - cur > 0;

        return Math.Abs(next - cur) >= 1 && Math.Abs(next - cur) <= 3 && (direction == initialDirection);
    }
}

