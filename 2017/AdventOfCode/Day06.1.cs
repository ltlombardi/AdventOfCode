using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day6rev1
    {
        public void Do()
        {
            var input = System.IO.File.ReadAllLines("../../Day06.txt");
            var memmoryBanks = Array.ConvertAll(input[0].Split('\t'), int.Parse);
            var memStatesSet = new HashSet<string>();
            var cycles = 0;
            while (memStatesSet.Add(ConvertToString(memmoryBanks)))
            {
                var blocksToDistribute = memmoryBanks.Max(); // this could be improved using linq, to get index and value in select
                var maxIndex = Array.IndexOf(memmoryBanks, blocksToDistribute);
                memmoryBanks[maxIndex] = 0;
                for (var i = maxIndex + 1; blocksToDistribute > 0; i++, blocksToDistribute--)
                {
                    if (i == memmoryBanks.Length) { i = 0; } // circular list
                    memmoryBanks[i] += 1;
                }
                cycles++;
            }
            Console.WriteLine("Day6 Part1: result is " + cycles);

            memmoryBanks = Array.ConvertAll(input[0].Split('\t'), int.Parse);
            memStatesSet = new HashSet<string>();
            var isInLoop = false;
            var firstLoopString = "";
            var firstOccurenceOfLooop = 0;
            cycles = 0;
            while (true)
            {
                var blocksToDistribute = memmoryBanks.Max();
                var maxIndex = Array.IndexOf(memmoryBanks, blocksToDistribute);
                memmoryBanks[maxIndex] = 0;

                for (var i = maxIndex + 1; blocksToDistribute > 0; i++, blocksToDistribute--)
                {
                    if (i == memmoryBanks.Length) { i = 0; } // circular list
                    memmoryBanks[i] += 1;
                }
                cycles++;
                string memStringUnified = ConvertToString(memmoryBanks);
                if (isInLoop && memStringUnified == firstLoopString) { break; }
                if (!memStatesSet.Add(memStringUnified) && !isInLoop)
                {
                    firstLoopString = memStringUnified;
                    firstOccurenceOfLooop = cycles;
                    isInLoop = true;
                }
            }
            Console.WriteLine("Day6 Part2: result is " + (cycles - firstOccurenceOfLooop));
        }

        private static string ConvertToString(int[] array)
        {
            return string.Join(",", array.Select(x => x.ToString()));
        }

        private static string ConvertToString2(int[] array)
        {
            var builder = new StringBuilder();
            array.Select(x => builder.Append(x.ToString() + ","));
            return builder.ToString();
        }

    }


}
