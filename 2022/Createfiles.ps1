function FunctionName {
    param (
        $name,
        $day
    )    
    return @"
    class $name
    {
        internal static async Task<int> Solution()
        {
            var lines = await File.ReadAllLinesAsync("InputDay$day.txt");
            var result = 0;
            return result;
        }
    }
"@
}
//test
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
}

