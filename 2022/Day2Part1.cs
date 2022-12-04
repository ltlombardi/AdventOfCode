class Day2Part1
{
    internal static async Task<int> Solution()
    {
        var lines = await File.ReadAllLinesAsync("InputDay2.txt");
        var result = 0;
        foreach (var line in lines)
        {
            result += GetShapeScore(line[2]) + GetOutcomeScore(line[0], line[2]);
        }
        return result;
    }

    static int GetShapeScore(char letter)
    {
        switch (letter)
        {
            case 'X': return 1;
            case 'Y': return 2;
            case 'Z': return 3;
            default: throw new Exception();
        }
    }

    static char Convert(char yours)
    {
        switch (yours)
        {
            case 'X': return 'A';
            case 'Y': return 'B';
            case 'Z': return 'C';
            default: throw new Exception();
        }
    }

    static int GetOutcomeScore(char theirs, char yours)
    {
        if (Convert(yours) == theirs)
        {
            return 3;
        }
        switch (theirs)
        {
            case 'A': return yours == 'Z' ? 0 : 6;
            case 'B': return yours == 'X' ? 0 : 6;
            case 'C': return yours == 'Y' ? 0 : 6;
            default: throw new Exception();
        }
    }
}