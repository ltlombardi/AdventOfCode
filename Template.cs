class DayXPartX
{
    internal static async Task Solution1()
    {
        var lines = await File.ReadAllLinesAsync("InputDayX.txt");
        var result = 0;

        Console.WriteLine($"{nameof(Day1Part2)}: result is " + result);
    }
}