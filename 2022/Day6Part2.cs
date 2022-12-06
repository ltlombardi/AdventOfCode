class Day6Part2
{
    internal static async Task<string> Solution()
    {
        var line = await File.ReadAllTextAsync("Day6Input.txt");
        var list = new LinkedList<char>(line.Take(14));

        var result = 0;
        for (int i = 14; i < line.Length; i++)
        {
            if (list.Distinct().Count() == 14)
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
