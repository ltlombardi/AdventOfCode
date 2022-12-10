class Day10Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day10Input.txt");
        var result = 0;
        var cycle = 0;
        var x = 1;
        var strengths = new List<int>();
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var lineParts = line.Split(' ');
            cycle++;
            NewMethod(cycle, x, strengths);
            if (lineParts[0] == "addx")
            {
                cycle++;
                NewMethod(cycle, x, strengths);
                x += int.Parse(lineParts[1]);
            }
            // if (lineParts[0] == "noop")
            // {
            //     NewMethod(cycle, x, strengths);
            // }
        }
        result = strengths.Aggregate((a, b) => a + b);
        return result.ToString();
    }

    private static void NewMethod(int cycle, int x, List<int> strengths)
    {
        if (cycle == 20 || cycle >= 60 && (cycle - 20) % 40 == 0)
        {
            strengths.Add(cycle * x);
        }
    }
}
