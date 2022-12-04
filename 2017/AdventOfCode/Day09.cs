using System;

namespace AdventOfCode
{
    class Day9
    {
        public void Do()
        {
            var line = System.IO.File.ReadAllText("../../Day09.txt");
            int openedGroups = 0;
            var score = 0;
            var isGarbage = false;
            var isCancelled = false;
            var totalGarbage = 0;
            foreach (var letter in line)
            {
                if (isGarbage)
                {
                    if (isCancelled) { isCancelled = false; }
                    else if (letter == '!') { isCancelled = true; }
                    else if (letter == '>') { isGarbage = false; }
                    else { totalGarbage++; }
                }
                else if (letter == '{') { openedGroups++; }
                else if (letter == '}') { score += openedGroups; openedGroups--; }
                else if (letter == '<') { isGarbage = true; }
            }
            Console.WriteLine("Day9 Part1: result is " + score);
            Console.WriteLine("Day9 Part2: result is " + totalGarbage);
        }
    }
}
