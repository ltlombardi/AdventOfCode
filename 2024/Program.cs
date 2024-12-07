using System.Reflection;

Setup.CreateFolderStructure();
var (day, part) = AskWhatToRun();
await Run(day, part);


static (int day, int part) AskWhatToRun()
{
    int previousDay = 1;
    int previousPart = 1;

    string lastRunInfoPath = Path.Combine(Setup.GetSolutionDirectory(), "lastRunInfo.txt");
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


static async Task Run(int day, int part)
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

        var resultTask = (Task<dynamic>)method.Invoke(null, null);
        var result = await resultTask;

        Console.WriteLine($"Result: {result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex}");
    }
}

