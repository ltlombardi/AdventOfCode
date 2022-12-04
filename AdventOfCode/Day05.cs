using System;

namespace AdventOfCode
{
    class Day5
    {
        public static void Do()
        {
            var input = System.IO.File.ReadAllLines("../../Day05.txt");
            var inputInt = Array.ConvertAll(input, int.Parse);
            var result = 0;

            var jump = 0;
            for (var i = 0; true;)
            {
                jump = inputInt[i]++;
                i += jump;
                result++;
                if (i >= inputInt.Length) break;
            }
            Console.WriteLine("Day5 Part1: result is " + result);

            input = System.IO.File.ReadAllLines("../../Day05.txt");
            inputInt = Array.ConvertAll(input, int.Parse);
            result = 0;
            jump = 0;
            for (var i = 0; true;)
            {
                jump = inputInt[i];
                if (jump >= 3)
                {
                    inputInt[i]--;
                }
                else
                {
                    inputInt[i]++;
                }
                i += jump;
                result++;
                if (i >= inputInt.Length) break;
            }
            Console.WriteLine("Day5 Part2: result is " + result);
        }
    }


}
