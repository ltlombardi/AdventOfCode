class Day7Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day7Input.txt");
        var result = 0;
        var dict = new Dictionary<string, int>();
        var directoryTree = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            var sections = lines[i].Split(' ');
            if (sections[1] == "cd")
            {
                if (sections[2] == "..")
                    directoryTree.RemoveAt(directoryTree.Count - 1);
                else
                    directoryTree.Add(sections[2]);
            }
            if (char.IsNumber(sections[0][0]))
            {
                var fileSize = int.Parse(sections[0]);
                for (int j = 0; j < directoryTree.Count; j++)
                {
                    //folder names aren't unique, so need to use path, not just name.
                    var path = "";
                    for (int k = 0; k <= j; k++)
                    {
                        path += directoryTree[k];
                    }
                    AddSize(dict, fileSize, path);
                }
            }
        }
        var fileSystemSize = 70000000;
        var neededUpdateSpace = 30000000;
        var remainingSpace = fileSystemSize - dict["/"];
        var missing = neededUpdateSpace - remainingSpace;
        var list = dict.Select(a=> a.Value).ToList();
        list.Sort();
        result = list.First(a=> a > missing);
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
