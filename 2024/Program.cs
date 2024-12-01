AskWhatToRun();

static void AskWhatToRun()
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
}

static string GetSolutionDirectory()
{
    var onVisualStudio = true;
    var currentDirectory = Directory.GetCurrentDirectory();
    if (onVisualStudio)
    {
        currentDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName;
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

            string[] fileNames = { "Part1.cs", "Part2.cs", "ExampleInput.txt", "Input.txt" };
            foreach (var fileName in fileNames)
            {
                using (var stream = File.Create(Path.Combine(folderName, fileName))) { };
            }
        }

        Console.WriteLine("Folders and files created successfully!");
    }
}