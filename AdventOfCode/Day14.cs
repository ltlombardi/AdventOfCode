using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day14 : IDoable
    {
        public void Do()
        {
            var line = System.IO.File.ReadAllText("../../Day14.txt");
            var occupiedSquares = 0;
            var size = 128;
            var grid = new int[size][];
            for (var i = 0; i < 128; i++)
            {
                var resultHex = Day10.DenseHash(line + "-" + i);
                var resultInBinary = resultHex.Select(r => Convert.ToString(Convert.ToInt32(r.ToString(), 16), 2).PadLeft(4, '0')).Aggregate((a, next) => a + next);
                occupiedSquares += resultInBinary.Select(c => int.Parse(c.ToString())).Aggregate((prev, next) => prev += next);
                grid[i] = resultInBinary.Select(c => int.Parse(c.ToString())).ToArray();
            }



            Console.WriteLine("Day14 Part1: result is " + occupiedSquares);
            Console.WriteLine("Day14 Part2: result is " + occupiedSquares);
        }

        private void Group(int[][] grid)
        {
            int groups = 1;
            if (item == '1') { }
        }
    }
}
