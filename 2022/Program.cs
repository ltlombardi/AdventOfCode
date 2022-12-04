// See https://aka.ms/new-console-template for more information

// Set the day you want to execute in the daysToExecute variable and run.
// If none is set, last solution will be run.
int? dayToExecute = default;
var solutions = new[,]
{
    { Day1Part1.Solution, Day1Part2.Solution},
    { Day2Part1.Solution, Day2Part2.Solution},
    { Day3Part1.Solution, Day3Part2.Solution},
};
var day = dayToExecute ?? solutions.Length;
var result = await solutions[day - 1, 0].Invoke();
Console.WriteLine($"Result of part 1 is {result}");

result = await solutions[day - 1, 1].Invoke();
Console.WriteLine($"Result of part 2 is {result}");

