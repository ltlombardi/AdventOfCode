using System.Text;

class Day4Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day4Input.txt");
        var result = 0;
        foreach (var line in lines)
        {
            var pairs = line.Split(',');
            var firstRange = BuildSet(pairs[0].Split('-').Select(int.Parse).ToArray());
            var secondRange = BuildSet(pairs[1].Split('-').Select(int.Parse).ToArray());

            if (firstRange.Any(secondRange.Contains))
            {
                result++;
            }
        }
        return result.ToString();
    }

    private static ISet<int> BuildSet(int[] range)
    {
        return Enumerable.Range(range[0], range[1] - range[0] + 1).ToHashSet();
    }
}