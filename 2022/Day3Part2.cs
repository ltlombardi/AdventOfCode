class Day3Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day3Input.txt");
        var result = 0;
        for (var i = 0; i < lines.Length; i += 3)
        {
            var first = lines[i];
            var second = lines[i + 1];
            var third = lines[i + 2];
            var tested = new HashSet<char>();
            foreach (var letter in first)
            {
                if (!tested.Contains(letter) && second.Contains(letter) && third.Contains(letter))
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