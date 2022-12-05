class Day5Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day5Input.txt");
        var columnSize = 4; // number of chars used to represent each crate in the stack in the input
        var numberOfStacks = (lines[0].Length + 1) / columnSize; // +1 because last column occupies only 3 chars
        var stackInput = Enumerable
            .Range(0, numberOfStacks)
            .Select(i => new Stack<string>())
            .ToArray();
        var divisionIndex = lines.Select((l, i) => new { line = l, index = i }).First(v => v.line == "").index;
        for (var i = 0; i < divisionIndex - 1; i++)
        {
            var line = lines[i];
            for (int j = 0; j < numberOfStacks; j++)
            {
                var crate = line[j * 4 + 1].ToString();
                if (!string.IsNullOrWhiteSpace(crate))
                    stackInput[j].Push(crate);
            }
        }

        var stacks = stackInput.Select(s => new Stack<string>(s)).ToArray();
        for (var i = divisionIndex + 1; i < lines.Length; i++)
        {
            var move = lines[i]
                .Split(' ')
                .Select(l => int.TryParse(l, out var n) ? n : 0)
                .Where(l => l > 0)
                .ToArray();
            var temp = new List<string>();
            for (int j = 0; j < move[0]; j++)
            {
                temp.Add(stacks[move[1] - 1].Pop());
            }
            temp.Reverse();
            for (int j = 0; j < temp.Count; j++)
            {
                stacks[move[2] - 1].Push(temp[j]);
            }
        }
        var result = stacks.Select(a => a.Peek()).Aggregate((a, b) => a + b);
        return result;
    }
}
