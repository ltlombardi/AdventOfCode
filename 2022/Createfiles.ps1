function FunctionName {
    param (
        $name,
        $day
    )    
    return @"
class $name
{
    internal static async Task<(string, string)> Solution()
    {
        return (await Solve("Day${day}ExampleInput.txt"), await Solve("Day${day}Input.txt"));
    }

    internal static async Task<string> Solve(string inputFileName)
    {
        var lines = await File.ReadAllLinesAsync(inputFileName);
        var result = 0;
        return result.ToString();
    }
}
"@
}

for ($i = 1; $i -le 25; $i++) {
    $name = "Day${i}Part1" 
    $file = New-Item "$name.cs"
    $class = FunctionName $name  $i
    Set-Content $file -Value   $class 

    $name = "Day${i}Part2" 
    $file = New-Item "$name.cs"
    $class = FunctionName $name $i
    Set-Content $file -Value $class

    $name = "Day${i}Input" 
    $file = New-Item "$name.txt"

    $name = "Day${i}ExampleInput" 
    $file = New-Item "$name.txt"
}

