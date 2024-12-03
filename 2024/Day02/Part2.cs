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
            var levelToSkip = -1;
            var initialDirection = levels[1] - levels[0] > 0;
            for (var i = 0; i < levels.Count - 1; i++)
            {
                var check = IsSafe(initialDirection, levels[i], levels[i + 1]);
                if (!check && levelToSkip == -1)
                {
                    levelToSkip = i + 1;
                    break;
                }
                isSafe &= check;
            }
            if (levelToSkip != -1 && levelToSkip != levels.Count - 1)
            {
                levels.RemoveAt(levelToSkip);
                initialDirection = levels[1] - levels[0] > 0;
                for (var i = 0; i < levels.Count - 1; i++)
                {
                    var check = IsSafe(initialDirection, levels[i], levels[i + 1]);
                    isSafe &= check;
                }
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

