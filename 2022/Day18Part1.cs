class Day18Part1
{
    internal static async Task<(string, string)> Solution()
    {
        return (await Solve("Day18ExampleInput.txt"), await Solve("Day18Input.txt"));
    }

    internal static async Task<string> Solve(string inputFileName)
    {
        var lines = await File.ReadAllLinesAsync(inputFileName);
        var result = 0;
        return result.ToString();
    }
}
