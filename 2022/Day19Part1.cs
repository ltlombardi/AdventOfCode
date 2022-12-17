class Day19Part1
{
    internal static async Task<(string, string)> Solution()
    {
        return (await Solve("Day19ExampleInput.txt"), await Solve("Day19Input.txt"));
    }

    internal static async Task<string> Solve(string inputFileName)
    {
        var lines = await File.ReadAllLinesAsync(inputFileName);
        var result = 0;
        return result.ToString();
    }
}
