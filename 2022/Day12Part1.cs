class Day12Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day12Input.txt");
        var result = 0;
        var grid = lines.Select(l => l.ToCharArray()).ToArray();
        var paths = new List<List<char>>();
        var start = (0, 0);

        return result.ToString();
    }

    string Do(char[][] grid)
    {
        foreach (var direction in directions)
        {
            return "chosendirection" + Do(grid);
        }
    }
}
