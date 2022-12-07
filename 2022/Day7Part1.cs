class Day7Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day7Input.txt");
        var result = 0;
        var dict = new Dictionary<string, int>();
        var dirTree = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var sections = line.Split(' ');
            if (sections[1] == "cd")
            {
                if (sections[2] == "..")
                    dirTree.RemoveAt(dirTree.Count - 1);
                else
                    dirTree.Add(sections[2]);
            }
            if (char.IsNumber(sections[0][0]))
            {
                var fileSize = int.Parse(sections[0]);
                for (int j = 0; j < dirTree.Count; j++)
                {
                    AddSize(dict, fileSize, dirTree[j]);
                }
            }
        }
        var sizeLimit = 100000;
        result = dict.Where(a => a.Value < sizeLimit).Select(a => a.Value).Aggregate((a, b) => a + b);
        return result.ToString();
    }

    private static void AddSize(Dictionary<string, int> dict, int fileSize, string curDir)
    {
        if (dict.TryGetValue(curDir, out var size))
        {
            size += fileSize;
            dict[curDir] = size;
        }
        else
        {
            dict.Add(curDir, fileSize);
        }
    }
}
