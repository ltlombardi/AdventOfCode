using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day1
    {
        public static void Do()
        {
            string input = System.IO.File.ReadAllText(@"../../Day01.txt");
            var sum = 0;
            for (var i = 0; i < input.Count(); i++)
            {
                var next = (i == input.Count() - 1) ? input[0] : input[i + 1];
                sum += input[i] == next ? int.Parse(input[i].ToString()) : 0;
            }
            Console.WriteLine("Day1 Part1: Sum is " + sum);

            sum = 0;
            var halfRange = input.Count() / 2;
            for (var i = 0; i < halfRange; i++)
            {
                var halfWayAround = input[i + halfRange];
                sum += input[i] == halfWayAround ? 2 * int.Parse(input[i].ToString()) : 0;
            }
            Console.WriteLine("Day1 Part2: Sum is " + sum);
        }
    }
}
