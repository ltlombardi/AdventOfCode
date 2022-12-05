class Day3Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day3Input.txt");
        var result = 0;
        foreach (var line in lines)
        {
            var first = line.Take(line.Length / 2);
            var second = line.Skip(line.Length / 2).ToArray();
            var tested = new HashSet<char>();
            foreach (var letter in first)
            {
                if (!tested.Contains(letter) && second.Contains(letter))
                    result += CalculatePriority(letter);
                tested.Add(letter);
            }
        }
        return result.ToString();
    }

    private static int CalculatePriority(char letter)
    {
        if (char.IsLower(letter))
        {
            return letter - 96;
        }
        return letter - 38;
    }
}