class Day20Part2
{
    internal static async Task<(string, string)> Solution()
    {
        return (await Solve("Day20ExampleInput.txt"), await Solve("Day20Input.txt"));
    }

    internal static async Task<string> Solve(string inputFileName)
    {
        var lines = await File.ReadAllLinesAsync(inputFileName);
        var result = 0;
        return result.ToString();
    }
}
