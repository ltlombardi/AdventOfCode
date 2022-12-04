using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day10 : IDoable
    {
        public void Do()
        {
            var line = System.IO.File.ReadAllText("../../Day10.txt");
            var lengths = line.Split(',').Select(l => int.Parse(l)).ToList();
            int skipSize = 0, result = 0, currentPos = 0;
            var baseOrSomething = new List<int>(Enumerable.Range(0, 256));

            RoundOfHash(lengths, ref skipSize, ref currentPos, baseOrSomething);
            result = baseOrSomething[0] * baseOrSomething[1];
            Console.WriteLine("Day10 Part1: result is " + result);

            string result2 = DenseHash(line);
            Console.WriteLine("Day10 Part2: result is " + result2);
        }

        public static string DenseHash(string hashLengths)
        {
            var baseOrSomething = new List<int>(Enumerable.Range(0, 256));
            int skipSize = 0, currentPos = 0;
            var lengthsFromChar = hashLengths.Select(i => (int)i).ToList(); // instead of parsing to int, cast to int. This way the 16bits used to map a Char are interpreted as int number
            lengthsFromChar.AddRange(new int[] { 17, 31, 73, 47, 23 });

            for (int j = 0; j < 64; j++)
            {
                RoundOfHash(lengthsFromChar, ref skipSize, ref currentPos, baseOrSomething);
            }
            var denseHash = SplitList(baseOrSomething, 16).Select(l => l.Aggregate((a, next) => a ^ next)).ToList();
            return denseHash.Select(i => i.ToString("X2")).Aggregate((a, next) => a + next);
        }

        public static void RoundOfHash(IList<int> lengths, ref int skipSize, ref int currentPos, List<int> inputToBeHashed)
        {
            foreach (var length in lengths)
            {
                var reverseList = new List<int>(length);
                for (var i = currentPos + length - 1; i >= currentPos; i--)
                {
                    reverseList.Add(inputToBeHashed[i % inputToBeHashed.Count]);
                }
                for (var i = 0; i < length; i++)
                {
                    inputToBeHashed[(i + currentPos) % inputToBeHashed.Count] = reverseList[i];
                }
                currentPos += length + skipSize;
                skipSize++;
            }
        }

        public static IEnumerable<List<T>> SplitList<T>(List<T> list, int nSize)
        {
            for (int i = 0; i < list.Count; i += nSize)
            {
                yield return list.GetRange(i, Math.Min(nSize, list.Count - i));
            }
        }
    }
}
