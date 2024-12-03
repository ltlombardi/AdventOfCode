namespace AdventOfCode2024.Day01;
internal class Part2
{
    internal static async Task<string> Run()
    {
        var lines = File.ReadLines(@"../../../Day01/Input.txt");
        var list1 = new List<int>();
        var list2 = new List<int>();
        foreach (var line in lines)
        {
            var ints = line.Split("   ").Select(int.Parse).ToArray();
            list1.Add(ints[0]);
            list2.Add(ints[1]);
            list1.Sort();
            list2.Sort();
        }
        var leftSet = list1.ToHashSet();
        var sum = 0;
        foreach (var uniqueLeft in leftSet)
        {
            var count = 0;
            foreach (var right in list2)
            {
                if (uniqueLeft == right)
                {
                    count++;
                }
            }
            sum += uniqueLeft * count;
        }


        return sum.ToString();
    }
}

