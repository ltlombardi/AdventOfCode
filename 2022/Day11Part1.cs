class Day11Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day11Input.txt");
        var result = 0;
        var monkeys = new List<Monkey>();
        for (int i = 0; i < lines.Count(); i += 7)
        {
            var items = lines[i + 1].Trim().Split(':')[1].Split(',').Select(int.Parse);
            var operation = lines[i + 2].Trim().Split(' ');
            var operand = operation[5] == "old" ? 0 : int.Parse(operation[5]);
            var @operator = operation[5] == "old" ? "^" : operation[4];
            var divisibleBy = int.Parse(lines[i + 3].Trim().Split(' ')[3]);
            var testPassMonkey = int.Parse(lines[i + 4].Trim().Split(' ')[5]);
            var testFailMonkey = int.Parse(lines[i + 5].Trim().Split(' ')[5]);
            monkeys.Add(new Monkey(
                items,
                @operator,
                operand,
                divisibleBy,
                testPassMonkey,
                testFailMonkey
                ));
        }

        const int Rounds = 20;
        foreach (var _ in Enumerable.Range(0, Rounds))
        {
            foreach (var monkey in monkeys)
            {
                monkey.Act(monkeys);
            }
        }
        var inspections = monkeys.Select(m => m.InspectCount).ToList();
        result = inspections.OrderDescending().Take(2).Aggregate((a, b) => a * b);
        return result.ToString();
    }

    class Monkey
    { // This is an experiment in omitting accessors and using defaults accessors
        public Queue<int> ItemWorries { get; }
        public int InspectCount { get; private set; }
        int DivisibleBy { get; }
        int TestPassMonkey { get; }
        int TestFailMonkey { get; }
        string Operator { get; }
        int Operand { get; }

        public Monkey(
            IEnumerable<int> worry,
            string @operator,
            int operand,
            int divisibleBy,
            int testPassMonkey,
            int testFailMonkey)
        {
            ItemWorries = new Queue<int>(worry);
            DivisibleBy = divisibleBy;
            TestPassMonkey = testPassMonkey;
            TestFailMonkey = testFailMonkey;
            Operator = @operator;
            Operand = operand;
        }

        public void Act(List<Monkey> monkeys)
        {
            while (ItemWorries.Any())
            {
                var item = ItemWorries.Dequeue();
                var worry = Relief(Inspect(item));
                monkeys[ThrowToWhichMonkey(worry)].ItemWorries.Enqueue(worry);
            }
        }
        int Relief(int worry) => worry / 3;
        int Inspect(int worry)
        {
            InspectCount++;
            switch (Operator)
            {
                case "*": return worry * Operand;
                case "^": return worry * worry;
                default: return worry + Operand;
            }
        }
        int ThrowToWhichMonkey(int worry) => worry % DivisibleBy == 0 ? TestPassMonkey : TestFailMonkey;
    }
}


