class Day5Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day5Input.txt");
        var columnSize = 4; // number of chars used to represent each crate in the stack in the input
        var lastColumnSpacer = 1;
        var numberOfStacks = (lines[0].Length + lastColumnSpacer) / columnSize;
        var stackInput = Enumerable
            .Range(0, numberOfStacks)
            .Select(i => new Stack<string>())
            .ToArray();

        var i = 0;
        for (; lines[i] != ""; i++)
        {
            var line = lines[i];
            for (int j = 0; j < numberOfStacks; j++)
            {
                var crate = line[j * 4 + 1].ToString();
                if (!string.IsNullOrWhiteSpace(crate) && char.IsLetter(crate.ToCharArray()[0]))
                    stackInput[j].Push(crate);
            }
        }

        var stacks = stackInput.Select(s => new Stack<string>(s)).ToArray();
        i++; //skip empty line
        for (; i < lines.Length; i++)
        {
            var move = lines[i]
                .Split(' ')
                .Select(l => int.TryParse(l, out var n) ? n : 0)
                .Where(l => l > 0)
                .ToArray();
            for (int j = 0; j < move[0]; j++)
            {
                stacks[move[2] - 1].Push(stacks[move[1] - 1].Pop());
            }
        }
        var result = stacks.Select(a => a.Peek()).Aggregate((a, b) => a + b);
        return result;
    }
}
