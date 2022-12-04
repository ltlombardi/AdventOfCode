using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Day2
    {
        public static void Do()
        {
            string[] input = System.IO.File.ReadAllLines(@"../../Day02.txt");
            var rows = input.Select(i =>
            Array.ConvertAll(i.Split('\t'), int.Parse)
            ).ToList();
            var sum = 0;
            foreach (var row in rows)
            {
                sum += row.Max() - row.Min();
            }
            Console.WriteLine("Day2 Part1: Checksum is " + sum);

            sum = 0;
            foreach (var row in rows)
            {
                for (var i = 0; i < row.Count(); i++)
                {
                    for (var j = i + 1; j < row.Count(); j++)
                    {
                        if (row[i] % row[j] == 0)
                        {
                            sum += row[i] / row[j];
                        }
                        else if (row[j] % row[i] == 0)
                        {
                            sum += row[j] / row[i];
                        }
                    }
                }
            }
            Console.WriteLine("Day2 Part2: Sum is " + sum);
        }
    }
}


