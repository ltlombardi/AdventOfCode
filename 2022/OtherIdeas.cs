class OtherIdeas
{

    async void InitializeApp1()
    {
        // have to manually update everyday and for every part. Sometimes I forget and run wrong part.
        var result = await Day3Part2.Solution();
        Console.WriteLine($"Result is {result}");
    }

    async void InitializeApp2()
    {
        // have to update only one variable and run both parts.
        // This uses jagged array, not multidimensional array.
        var dayToExecute = 3;
        var solutions = new[]
        {
        new [] { Day1Part1.Solution, Day1Part2.Solution},
        new [] { Day2Part1.Solution, Day2Part2.Solution},
        new [] { Day3Part1.Solution, Day3Part2.Solution},
        };
        var result = await solutions[dayToExecute - 1][0].Invoke();
        Console.WriteLine($"Result of part 1 is {result}");
        result = await solutions[dayToExecute - 1][1].Invoke();
        Console.WriteLine($"Result of part 2 is {result}");
    }
}