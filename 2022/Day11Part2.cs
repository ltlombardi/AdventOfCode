class Day11Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day11Input.txt");
        var result = 0L;
        var monkeys = new List<Monkey>();
        for (int i = 0; i < lines.Count(); i += 7)
        {
            var items = lines[i + 1].Trim().Split(':')[1].Split(',').Select(long.Parse);
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
        int reliefFactor = CalculateReliefFactor(monkeys);
        var rounds = 10000;
        foreach (var _ in Enumerable.Range(0, rounds))
        {
            foreach (var monkey in monkeys)
            {
                monkey.Act(monkeys, reliefFactor);
            }
        }
        var monkeyInspectCounts = monkeys.Select(m => m.inspectCount).ToList();
        result = monkeyInspectCounts.OrderDescending().Take(2).Select(Convert.ToInt64).Aggregate((a, b) => a * b);
        return result.ToString();
    }

    private static int CalculateReliefFactor(List<Monkey> monkeys)
    {
        /* 
        The numbers become too big that the calculations with int do overflow and give wrong numbers.
        To fix this, we can use Modular Arithmetic (math operation properties) that allow us to apply "mod" on 
        the worry levels by a factor reducing the number, without changing the logic of the program / monkey business.
        This guys explains it fairly good https://www.youtube.com/watch?v=F4MCuPZDKog
        This is very complicate. Requires knowledge of https://en.wikipedia.org/wiki/Modular_arithmetic 
        Although I don't recommend learning if from wikipedia.         
        */
        return monkeys.Aggregate(1, (c, m) => c * m.divisibleBy);
    }

    class Monkey
    {
        // Using defaults accessors and fields to see how it looks like
        // downside if using field.. can't make inspectCount readonly because it is changed inside class. If it was a property could have used private set, protecting it from outside change.
        // So better to NEVER EVER use public fields... better to 
        public int inspectCount;
        public readonly Queue<long> itemWorries;
        public readonly int divisibleBy;
        int testPassMonkey;
        int testFailMonkey;
        string @operator;
        int operand;

        public Monkey(
            IEnumerable<long> worry,
            string @operator,
            int operand,
            int divisibleBy,
            int testPassMonkey,
            int testFailMonkey)
        {
            itemWorries = new Queue<long>(worry);
            this.divisibleBy = divisibleBy;
            this.testPassMonkey = testPassMonkey;
            this.testFailMonkey = testFailMonkey;
            this.@operator = @operator;
            this.operand = operand;
        }

        public void Act(List<Monkey> monkeys, int reliefFactor)
        {
            while (itemWorries.Any())
            {
                var item = itemWorries.Dequeue();
                var worry = Relief(Inspect(item), reliefFactor);
                var receivingMonkey = ThrowToWhichMonkey(worry);
                monkeys[receivingMonkey].itemWorries.Enqueue(worry);
            }
        }
        long Relief(long worry, int reliefFactor) => worry % reliefFactor;
        long Inspect(long worry)
        {
            inspectCount++;
            switch (@operator)
            {
                case "*": return worry * operand;
                case "^": return worry * worry;
                default: return worry + operand;
            }
        }
        int ThrowToWhichMonkey(long worry) => worry % divisibleBy == 0 ? testPassMonkey : testFailMonkey;
    }
}


