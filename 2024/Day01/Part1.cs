namespace AdventOfCode2024.Day01;
internal class Part1
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
        var sum = Enumerable.Zip(list1, list2)
            .Select(p => Math.Abs(p.First - p.Second))
            .Sum();


        return sum.ToString();
    }
}

