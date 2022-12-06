class Day6Part1
{
    internal static async Task<string> Solution2()
    {
        var line = await File.ReadAllTextAsync("Day6Input.txt");
        const int markerSize = 4;
        var list = new LinkedList<char>(line.Take(markerSize));

        var result = 0;
        for (int i = markerSize; i < line.Length; i++)
        {
            if (list.Distinct().Count() == markerSize)
            {
                result = i;
                break;
            }
            list.RemoveFirst();
            list.AddLast(line[i]);
        }
        return result.ToString();
    }

    internal static async Task<string> Solution()
    {
        var line = await File.ReadAllTextAsync("Day6Input.txt");
        const int markerSize = 4;
        var result = 0;
        for (int i = 0; i < line.Length; i++)
        {
            if (line.Substring(i, markerSize).Distinct().Count() == markerSize)
            {
                result = i + markerSize;
                break;
            }

        }
        return result.ToString();
    }
}
