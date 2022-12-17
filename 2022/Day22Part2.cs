class Day22Part2
{
    internal static async Task<(string, string)> Solution()
    {
        return (await Solve("Day22ExampleInput.txt"), await Solve("Day22Input.txt"));
    }

    internal static async Task<string> Solve(string inputFileName)
    {
        var lines = await File.ReadAllLinesAsync(inputFileName);
        var result = 0;
        return result.ToString();
    }
}
