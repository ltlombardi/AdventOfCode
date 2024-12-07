namespace AdventOfCode2024.Day06;
internal class Part1
{
    internal static async Task<dynamic> Run()
    {
        var lines = await File.ReadAllLinesAsync(@"../../../Day06/Input.txt");
        var rules = lines.TakeWhile(l => !string.IsNullOrEmpty(l))
                         .Select(l => l.Split('|').Select(int.Parse).ToList())
                         .ToList();
        var updates = lines.SkipWhile(l => !string.IsNullOrEmpty(l)).Skip(1).Select(l => l.Split(',').Select(int.Parse).ToList()).ToList();
        var sum = 0;
        foreach (var update in updates)
        {
            var illegal = false;
            for (var p = 0; p < update.Count; p++)
            {
                var pagesBefore = update.Take(p).ToList();
                var page = update[p];
                if (rules.Any(r => r[0] == page && pagesBefore.Contains(r[1])))
                {
                    illegal = true;
                }
            }
            if (!illegal) sum += update[(update.Count - 1) / 2];
        }
        return sum;
    }
}

