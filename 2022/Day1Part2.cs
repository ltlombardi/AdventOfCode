class Day1Part2
{
    internal static async Task<int> Solution()
    {
        var lines = await File.ReadAllLinesAsync("InputDay1.txt");
        int max1, max2, max3, sum;
        max1 = max2 = max3 = sum = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            bool isLastLine = i == lines.Length + 1;
            if (lines[i] == "" || isLastLine)
            {
                if (sum > max1)
                {
                    max3 = max2;
                    max2 = max1;
                    max1 = sum;
                }
                else if (sum > max2)
                {
                    max3 = max2;
                    max2 = sum;
                }
                else if (sum > max3)
                {
                    max3 = sum;
                }
                sum = 0;
            }
            else
            {
                sum += int.Parse(lines[i]);
            }
        }
        var result = max1 + max2 + max3;
        return result;
    }

    internal static async Task<int> Solution2()
    {
        var text = await File.ReadAllTextAsync("InputDay1.txt");
        var result = text
            .Split("\r\n\r\n")
            .Select(t => t.Split("\r\n").Select(l => l == "" ? 0 : int.Parse(l)))
            .Select(l => l.Aggregate((a, b) => a + b))
            .OrderDescending()
            .Take(3)
            .Sum();

        return result;
    }
}