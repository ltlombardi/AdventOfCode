// See https://aka.ms/new-console-template for more information

// Set the day you want to execute in the daysToExecute variable and run.
// If none is set, last solution will be run.
int? dayToExecute = default;
var solutions = new[,]
{
    { Day1Part1.Solution, Day1Part2.Solution},
    { Day2Part1.Solution, Day2Part2.Solution},
    { Day3Part1.Solution, Day3Part2.Solution},
    { Day4Part1.Solution, Day4Part2.Solution},
    { Day5Part1.Solution, Day5Part2.Solution},
    { Day6Part1.Solution, Day6Part2.Solution},
    { Day7Part1.Solution, Day7Part2.Solution},
    { Day8Part1.Solution, Day8Part2.Solution},
    { Day9Part1.Solution, Day9Part2.Solution},
    { Day10Part1.Solution, Day10Part2.Solution},
    { Day11Part1.Solution, Day11Part2.Solution},
    { Day12Part1.Solution, Day12Part2.Solution},
    { Day14Part1.Solution, Day14Part2.Solution},
};

var solutions2 = new[,]
{
    { Day15Part1.Solution, Day15Part2.Solution},
    { Day16Part1.Solution, Day16Part2.Solution},
};

var day = dayToExecute ?? solutions2.Length / 2;
var result = await solutions2[day - 1, 0].Invoke();
Console.WriteLine($"Day {day} part 1 example result is {result.Item1}");
Console.WriteLine($"Day {day} part 1 result is {result.Item2}");

result = await solutions2[day - 1, 1].Invoke();
Console.WriteLine($"Day {day} part 2 example result is {result.Item1}");
Console.WriteLine($"Day {day} part 2 result is {result.Item2}");

// TODO: save the example input and my input, to be easier to run both. 
// Maybe put an assert equals, with the expected result of the example.
// maybe call the solution() and inside it, the method would call a Solve(args), with the example input and my input