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
            int operand = operation[5] == "old" ? 0 : int.Parse(operation[5]);
            var @operator = operation[5] == "old" ? "^" : operation[4];
            int divisibleBy = int.Parse(lines[i + 3].Trim().Split(' ')[3]);
            int testPassMonkey = int.Parse(lines[i + 4].Trim().Split(' ')[5]);
            int testFailMonkey = int.Parse(lines[i + 5].Trim().Split(' ')[5]);
            monkeys.Add(new Monkey(
                items,
                @operator,
                operand,
                divisibleBy,
                testPassMonkey,
                testFailMonkey
                ));
        }
        foreach (var _ in Enumerable.Range(0, 20))
        {
            foreach (var monkey in monkeys)
            {
                monkey.Act(monkeys);
            }
        }
        var temp = monkeys.Select(m => m.InspectCount).OrderDescending().ToList();
        result = temp.Take(2).Aggregate((a, b) => a * b);
        return result.ToString();
    }

    class Monkey
    { // This is an experiment in omitting accessors and using defaults
        public Queue<int> ItemWorries { get; }
        int DivisibleBy { get; }
        int TestPassMonkey { get; }
        int TestFailMonkey { get; }
        string Operator { get; }
        int Operand { get; }
        public int InspectCount { get; private set; }

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
                var worryLevel = Relief(Inspect(item));
                monkeys[Throw(worryLevel)].ItemWorries.Enqueue(worryLevel);
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
        int Throw(int worry) => Test(worry) ? TestPassMonkey : TestFailMonkey;
        bool Test(int worry) => worry % DivisibleBy == 0;
    }
}


