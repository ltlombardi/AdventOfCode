$name = ""
for ($i = 1; $i -le 1; $i++) {
    $name = "Day${i}Part1" 
    New-Item "$name.cs" -value $template
    # New-Item "Day" + $i + "Part2.cs"
    # New-Item "Day" + $i + "Input.txt"
}

$template = @"
class ${name}
{
    internal static async Task<int> Solution()
    {
        var lines = await File.ReadAllLinesAsync("InputDay$i.txt");
        var result = 0;
        return result;
    }
}
"@