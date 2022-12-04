class Day4Part1
{
    internal static async Task<int> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day4Input.txt");
        var result = 0;
        foreach (var line in lines)
        {
            var pairs = line.Split(',');
            var  (first, second) = (pairs[0], pairs[1]);
            var firstRange =  first.Split('-').Select(int.Parse).ToArray();
            var secondRange = second.Split('-').Select(int.Parse).ToArray();
            
            if (firstRange[0] <= secondRange[0] && firstRange[1] >= secondRange[1])
            {
                result++;
            }else if (firstRange[0] >= secondRange[0] && firstRange[1] <= secondRange[1])
            {
                result++;
            }
        }
        return result;
    }

    struct Range
    {
        public int L { get; }
        public int U { get; }
        public Range(string l, string u)
        {
            L = int.Parse(l);
            U = int.Parse(u);
        }
    }
} 