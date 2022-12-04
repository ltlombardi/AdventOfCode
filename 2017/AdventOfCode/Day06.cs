using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day6
    {
        public void Do()
        {
            var input = System.IO.File.ReadAllLines("../../Day06.txt");
            var memmoryBanks = Array.ConvertAll(input[0].Split('\t'), int.Parse);
            var memStatesSet = new HashSet<string>();

            var result = 0;
            while (true)
            {
                var blockToDistribuite = memmoryBanks.Max();
                var maxIndex = Array.IndexOf(memmoryBanks, blockToDistribuite);
                memmoryBanks[maxIndex] = 0;
                for (var i = maxIndex + 1; blockToDistribuite > 0;)
                {
                    if (i == memmoryBanks.Length)
                    {
                        i = 0;
                    }
                    memmoryBanks[i] += 1;
                    blockToDistribuite--;
                    i++;

                }
                result++;
                string memStringUnified = ConvertToString(memmoryBanks);
                if (memStatesSet.Contains(memStringUnified)) break;
                memStatesSet.Add(memStringUnified);
            }

            Console.WriteLine("Day6 Part1: result is " + result);

            memmoryBanks = Array.ConvertAll(input[0].Split('\t'), int.Parse);
            memStatesSet = new HashSet<string>();
            var loops = 0;
            var firstLoopString = "";
            var firstOccurenceOfLooop = 0;
            result = 0;
            while (true)
            {
                var blockToDistribuite = memmoryBanks.Max();
                var maxIndex = Array.IndexOf(memmoryBanks, blockToDistribuite);
                memmoryBanks[maxIndex] = 0;
                for (var i = maxIndex + 1; blockToDistribuite > 0;)
                {
                    if (i == memmoryBanks.Length)
                    {
                        i = 0;
                    }
                    memmoryBanks[i] += 1;
                    blockToDistribuite--;
                    i++;

                }
                result++;
                string memStringUnified = ConvertToString(memmoryBanks);
                if (memStringUnified == firstLoopString && loops > 0)
                {
                    break;
                }
                if (memStatesSet.Contains(memStringUnified) && loops == 0)
                {
                    firstLoopString = memStringUnified;
                    firstOccurenceOfLooop = result;
                    loops++;
                }
                memStatesSet.Add(memStringUnified);
            }


            Console.WriteLine("Day6 Part2: result is " + (result - firstOccurenceOfLooop));
        }

        private static string ConvertToString(int[] array)
        {
            var StringArray = array.Select(x => x.ToString()).ToArray();
            var stringUnified = string.Join(",", StringArray);
            return stringUnified;
        }

        private static string ConvertToString2(int[] array)
        {
            var builder = new StringBuilder();
            foreach (var item in array)
            {
                builder.Append(item.ToString() + ",");
            }
            Array.ForEach(array, x => builder.Append(x.ToString() + ","));
            return builder.ToString();
        }

        private static string ConvertToString3(int[] array)
        {
            var builder = new StringBuilder();
            Array.ForEach(array, x => builder.Append(x.ToString() + ","));
            return builder.ToString();
        }

        private static string ConvertToString4(int[] array)
        {
            var builder = new StringBuilder();
            array.Select(x => builder.Append(x.ToString() + ","));
            return builder.ToString();
        }

    }


}
