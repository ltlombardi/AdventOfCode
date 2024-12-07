class Setup
{
    internal static string GetSolutionDirectory()
    {
        var onVisualStudio = true;
        var currentDirectory = Directory.GetCurrentDirectory();
        if (onVisualStudio)
        {
            currentDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName;
        }
        else
        {
            // VSCode probably doesn't need all this. It read the files considering the current source code path.
        }

        return currentDirectory;
    }

    internal static void CreateFolderStructure()
    {
        string currentDirectory = GetSolutionDirectory();

        var existingFiles = Path.Combine(currentDirectory, "Day31");
        if (!Directory.Exists(existingFiles))
        {
            for (int day = 1; day <= 31; day++)
            {
                var folderName = Path.Combine(currentDirectory, $"Day{day:D2}");
                _ = Directory.CreateDirectory(folderName);

                File.WriteAllText(Path.Combine(folderName, "Part1.cs"), CreateClassContent("Part1", day));
                File.WriteAllText(Path.Combine(folderName, "Part2.cs"), CreateClassContent("Part2", day));

                using (var stream = File.Create(Path.Combine(folderName, "ExampleInput.txt"))) { };
                using (var stream = File.Create(Path.Combine(folderName, "Input.txt"))) { };
            }

            Console.WriteLine(" Since this is first run, folders and files were created successfully! Re run app to run first exercise.");
            Environment.Exit(0);
        }
    }

    internal static string CreateClassContent(string className, int day)
    {
        return $@"namespace AdventOfCode2024.Day{day:D2};
internal class {className}
{{
    internal static async Task<dynamic> Run()
    {{
        var lines = await File.ReadAllLinesAsync(@""../../../Day{day:D2}/Input.txt"");
        return """";
    }}
}}

";
    }
}