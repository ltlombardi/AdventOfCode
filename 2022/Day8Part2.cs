class Day8Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day8Input.txt");
        var result = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                var score = CalcScenicScore(lines, i, j);
                if (score > result)
                {
                    result = score;
                }
            }
        }

        return result.ToString();
    }

    private static int CalcScenicScore(string[] lines, int i, int j)
    {
        var current = lines[i][j];
        var topViewDistance = 0;
        for (int row = i - 1; row >= 0; row--)
        {
            topViewDistance++;
            if (lines[row][j] >= current)
            {
                break;
            }
        }

        var botViewDistance = 0;
        for (int row = i + 1; row < lines.Length; row++)
        {
            botViewDistance++;
            if (lines[row][j] >= current)
            {
                break;
            }
        }

        var leftViewDistance = 0;
        for (int col = j - 1; col >= 0; col--)
        {
            leftViewDistance++;
            if (lines[i][col] >= current)
            {
                break;
            }
        }

        var rightViewDistance = 0;
        for (int col = j + 1; col < lines[i].Length; col++)
        {
            rightViewDistance++;
            if (lines[i][col] >= current)
            {
                break;
            }
        }

        return botViewDistance * topViewDistance * rightViewDistance * leftViewDistance;
    }
}
