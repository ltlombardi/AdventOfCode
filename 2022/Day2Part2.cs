class Day2Part2
{
    internal static async Task<int> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day2Input.txt");
        var result = 0;
        foreach (var line in lines)
        {
            result += GetOutcomeScore(line[2]) + GetShapeScore(GetRequiredShape(line[0], line[2]));
        }
        return result;
    }

    static int GetShapeScore(char letter)
    {
        switch (letter)
        {
            case 'A': return 1;
            case 'B': return 2;
            case 'C': return 3;
            default: throw new Exception();
        }
    }

    static char GetRequiredShape(char theirs, char outcome)
    {
        if (outcome == 'Y')
        {
            return theirs;
        }
        switch (theirs)
        {
            case 'A': return outcome == 'Z' ? 'B' : 'C';
            case 'B': return outcome == 'Z' ? 'C' : 'A';
            case 'C': return outcome == 'Z' ? 'A' : 'B';
            default: throw new Exception();
        }
    }

    static int GetOutcomeScore(char result)
    {
        switch (result)
        {
            case 'X': return 0;
            case 'Y': return 3;
            case 'Z': return 6;
            default: throw new Exception();
        }
    }
}