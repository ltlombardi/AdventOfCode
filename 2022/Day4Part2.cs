using System.Text;

class Day4Part2
{
    internal static async Task<int> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day4Input.txt");
        var result = 0;
        foreach (var line in lines)
        {
            var pairs = line.Split(',');
            var (first, second) = (pairs[0], pairs[1]);
            var firstRange = BuildRange(first.Split('-').Select(int.Parse).ToArray());
            var secondRange = BuildRange(second.Split('-').Select(int.Parse).ToArray());

            if (firstRange.Any(secondRange.Contains))
            {
                result++;
            }
        }
        return result;
    }

    private static ISet<int> BuildRange(int[] range)
    {
        return Enumerable.Range(range[0], range[1]-range[0]+1).ToHashSet();
    }
}