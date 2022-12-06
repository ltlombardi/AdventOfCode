class Day6Part2
{
    internal static async Task<string> Solution()
    {
        var line = await File.ReadAllTextAsync("Day6Input.txt");
        const int markerSize = 14;
        var list = new LinkedList<char>(line.Take(markerSize));

        var result = 0;
        for (int i = markerSize; i < line.Length; i++)
        {
            if (list.Distinct().Count<char>() == markerSize)
            {
                result = i;
                break;
            }
            list.RemoveFirst();
            list.AddLast(line[i]);
        }
        return result.ToString();
    }
}
