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
        
        return Char.GetNumericValue(lines[i][j]) > Char.GetNumericValue(lines[i][j + 1])
        || Char.GetNumericValue(lines[i][j]) > Char.GetNumericValue(lines[i][j - 1])
        || Char.GetNumericValue(lines[i][j]) > Char.GetNumericValue(lines[i - 1][j])
        || Char.GetNumericValue(lines[i][j]) > Char.GetNumericValue(lines[i + 1][j]);
    }
}
