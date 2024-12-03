using System.Reflection;

SetupFolderStructure();
var (day, part) = AskWhatToRun();
await RunMethod(day, part);


static (int day, int part) AskWhatToRun()
{
    int previousDay = 1;
    int previousPart = 1;

    string lastRunInfoPath = Path.Combine(GetSolutionDirectory(), "lastRunInfo.txt");
    if (File.Exists(lastRunInfoPath))
    {
        var lines = File.ReadAllLines(lastRunInfoPath);
        if (lines.Length >= 2)
        {
            _ = int.TryParse(lines[0], out previousDay);
            _ = int.TryParse(lines[1], out previousPart);
        }
    }

    bool isValidInput = false;

    Console.WriteLine($"Running day {previousDay} part {previousPart}.");
    Console.WriteLine("Press Enter to continue or input day and part. e.g. 12 2 for day 12 part 2: ");
    while (!isValidInput)
    {
        var input = Console.ReadLine();
        var inputs = input.Split(' ');

        if (string.IsNullOrWhiteSpace(input))
        {
            isValidInput = true;
        }
        else if (inputs.Length == 2 &&
                 int.TryParse(inputs[0], out int dayValue) && dayValue >= 1 && dayValue <= 31 &&
                 int.TryParse(inputs[1], out int partValue) && partValue >= 1 && partValue <= 2)
        {
            previousDay = dayValue;
            previousPart = partValue;
            isValidInput = true;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid day (1-31) and part (1-2).");
        }
    }

    File.WriteAllLines(lastRunInfoPath, [previousDay.ToString(), previousPart.ToString()]);

    Console.WriteLine($"You entered: Day {previousDay}, Part {previousPart}");
    return (previousDay, previousPart);
}

static string GetSolutionDirectory()
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

static void SetupFolderStructure()
{
    string currentDirectory = GetSolutionDirectory();

    var existingFiles = Path.Combine(currentDirectory, "Day01");
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

static string CreateClassContent(string className, int day)
{
    return $@"namespace AdventOfCode2024.Day{day:D2};
internal class {className}
{{
    internal static async Task<string> Run()
    {{
        var lines = await File.ReadAllLinesAsync(@""../../../Day{day:D2}/Input.txt"");
        return """";
    }}
}}

";
}

static async Task RunMethod(int day, int part)
{
    var className = $"AdventOfCode2024.Day{day:D2}.Part{part}";
    var methodName = "Run";

    try
    {
        var assembly = Assembly.GetExecutingAssembly();
        var type = assembly.GetType(className);

        if (type == null)
        {
            Console.WriteLine($"Class '{className}' not found.");
            return;
        }

        var method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

        if (method == null)
        {
            Console.WriteLine($"Method '{methodName}' not found in class '{className}'.");
            return;
        }

        var resultTask = (Task<string>)method.Invoke(null, null);
        var result = await resultTask;

        Console.WriteLine($"Result: {result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

