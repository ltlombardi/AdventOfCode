namespace AdventOfCode2024.Day02;
internal class Part2
{ // 311 
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
                isSafe = IsSafe(initialDirection, levels[i], levels[i + 1]);
                if (!isSafe)
                {
                    levelToSkip = i;
                    break;
                }
            }
            if (!isSafe)
            {
                var newLevels = levels.ToList();
                newLevels.RemoveAt(levelToSkip);
                initialDirection = newLevels[1] - newLevels[0] > 0;
                for (var i = 0; i < newLevels.Count - 1; i++)
                {
                    isSafe = IsSafe(initialDirection, newLevels[i], newLevels[i + 1]);
                    if (!isSafe)
                    {
                        break;
                    }
                }
            }
            if (!isSafe)
            {
                var newLevels = levels.ToList();
                newLevels.RemoveAt(levelToSkip + 1);
                initialDirection = newLevels[1] - newLevels[0] > 0;
                for (var i = 0; i < newLevels.Count - 1; i++)
                {
                    isSafe = IsSafe(initialDirection, newLevels[i], newLevels[i + 1]);
                    if (!isSafe)
                    {
                        break;
                    }
                }
            }
            if (!isSafe)
            {
                var newLevels = levels.SkipLast(1).ToList();
                initialDirection = newLevels[1] - newLevels[0] > 0;
                for (var i = 0; i < newLevels.Count - 1; i++)
                {
                    isSafe = IsSafe(initialDirection, newLevels[i], newLevels[i + 1]);
                    if (!isSafe)
                    {
                        break;
                    }
                }
            }
            if (!isSafe)
            {
                var newLevels = levels.Skip(1).ToList();
                initialDirection = newLevels[1] - newLevels[0] > 0;
                for (var i = 0; i < newLevels.Count - 1; i++)
                {
                    isSafe = IsSafe(initialDirection, newLevels[i], newLevels[i + 1]);
                    if (!isSafe)
                    {
                        break;
                    }
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

