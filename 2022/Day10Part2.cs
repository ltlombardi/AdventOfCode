class Day10Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day10Input.txt");
        var result = 0;
        var cycle = 0;
        var x = 1; // position of middle of a 3 pixels wide horizontal sprite
        var strengths = new List<int>();
        var crtPixelPosition = -1;
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var lineParts = line.Split(' ');
            cycle++;
            crtPixelPosition++;

            Print(x, crtPixelPosition, cycle);

            if (lineParts[0] == "addx")
            {
                cycle++;
                crtPixelPosition++;

                Print(x, crtPixelPosition, cycle);
                x += int.Parse(lineParts[1]);
            }
        }
        return result.ToString();
    }

    private static void Print(int x, int crtPixelPosition, int cycle)
    {
        var onScreenPosition = (crtPixelPosition % 40);
        if (x - 1 <= onScreenPosition && onScreenPosition <= x + 1)
        {
            Console.Write('#');
        }
        else
        {
            Console.Write('.');
        }
        if (cycle >= 40 && cycle % 40 == 0)
        {
            Console.Write("\n");
        }
    }
}
