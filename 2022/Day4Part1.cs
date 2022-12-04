class Day4Part1
{
    internal static async Task<int> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day4Input.txt");
        var result = 0;
        foreach (var line in lines)
        {
            var pairs = line.Split(',');
            var firstRange = pairs[0].Split('-').Select(int.Parse).ToArray();
            var secondRange = pairs[1].Split('-').Select(int.Parse).ToArray();

            if (firstRange[0] <= secondRange[0] && firstRange[1] >= secondRange[1])
            {
                result++;
            }
            else if (firstRange[0] >= secondRange[0] && firstRange[1] <= secondRange[1])
            {
                result++;
            }
        }
        return result;
    }
}