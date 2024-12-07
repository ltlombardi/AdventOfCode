namespace AdventOfCode2024.Day05;
internal class Part2
{
    internal static async Task<dynamic> Run()
    {
        var lines = await File.ReadAllLinesAsync(@"../../../Day05/Input.txt");
        var pageOrderingRules = lines.TakeWhile(l => !string.IsNullOrEmpty(l))
                         .Select(l => l.Split('|').Select(int.Parse).ToList())
                         .Select(i => (i[0], i[1]))
                         .ToList();
        var updates = lines.SkipWhile(l => !string.IsNullOrEmpty(l)).Skip(1).Select(l => l.Split(',').Select(int.Parse).ToList()).ToList();
        var sum = 0;
        foreach (var pagesToProduce in updates)
        {
            var illegal = false;
            for (var p = 0; p < pagesToProduce.Count; p++)
            {
                foreach (var rule in pageOrderingRules)
                {
                    var pagesBeforeCurrent = pagesToProduce.Take(p).ToList();
                    var pageUnderAnalysis = pagesToProduce[p];
                    var invalidPageIndex = pagesBeforeCurrent.IndexOf(rule.Item2);
                    if (rule.Item1 == pageUnderAnalysis && invalidPageIndex != -1)
                    {
                        pagesToProduce.Insert(p + 1, rule.Item2);
                        pagesToProduce.RemoveAt(invalidPageIndex);
                        p--;
                        illegal = true;
                    }
                }
            }
            if (illegal) sum += pagesToProduce[(pagesToProduce.Count - 1) / 2];
        }
        return sum;
    }
}

