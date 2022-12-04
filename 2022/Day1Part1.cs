class Day1Part1
{
    internal static async Task<int> Solution()
    {
        var lines = await File.ReadAllLinesAsync("InputDay1.txt");
        var max = 0;
        var sum = 0;
        foreach (var line in lines)
        {
            if (line != "")
            {
                sum += int.Parse(line);
            }
            else
            {
                max = Math.Max(max, sum);
                sum = 0;
            }
        }
        max = Math.Max(max, sum); // needs to check last sum
        var result = max;
        return result;
    }


    internal static async Task<int> Solution2()
    {
        var lines = await File.ReadAllLinesAsync("InputDay1.txt");
        var count = 0;
        var result = lines
            .Select(l => new
            {
                value = l == "" ? 0 : int.Parse(l),
                selector = l == "" ? count++ : count
            })
            .GroupBy(k => k.selector)
            .Select(g => g.Select(i => i.value))
            .Select(v => v.Aggregate((a, b) => a + b))
            .Max();

        return result;
    }

    internal static async Task<int> Solution3()
    {
        var text = await File.ReadAllTextAsync("InputDay1.txt");
        var result = text
            .Split("\r\n\r\n")
            .Select(t => t.Split("\r\n").Select(l => l == "" ? 0 : int.Parse(l)))
            .Select(l => l.Aggregate((a, b) => a + b))
            .Max();

        return result;
    }
}