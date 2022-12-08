class Day8Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day8Input.txt");
        var result = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                bool isEdge = i == 0 || i == lines.Length - 1 || j == 0 || j == lines[i].Length - 1;
                if (isEdge || IsInteriorVisible(lines, i, j))
                {
                    result++;
                }
            }
        }

        return result.ToString();
    }

    private static bool IsInteriorVisible(string[] lines, int i, int j)
    {
        var current = lines[i][j];
        var isTopVisible = true;
        for (int row = 0; row < i; row++)
        {
            isTopVisible = isTopVisible && lines[row][j] < current;
        }

        var isBotVisible = true;
        for (int row = i + 1; row < lines.Length; row++)
        {
            isBotVisible = isBotVisible && lines[row][j] < current;
        }

        return lines[i].Substring(j + 1).All(l => l < current)
        || lines[i].Substring(0, j).All(l => l < current)
        || isTopVisible
        || isBotVisible;
    }
}
