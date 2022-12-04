using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day7 : IDoable
    {
        public void Do()
        {
            var lines = System.IO.File.ReadLines("../../Day07.txt");
            var supporters = new Dictionary<string, Program>();
            var topPrograms = new Dictionary<string, Program>();
            var supportedSupporters = new HashSet<string>();
            foreach (var line in lines)
            {
                var inputArray = line.Split(' ');
                var prog = new Program
                {
                    name = inputArray[0],
                    weight = int.Parse(inputArray[1].Substring(1, inputArray[1].Length - 2))
                };
                if (inputArray.Length > 3)
                {
                    for (var i = 3; i < inputArray.Length; i++)
                    {
                        var name = inputArray[i].TrimEnd(',');
                        prog.subProgram.Add(new Program(name));
                        supportedSupporters.Add(name);
                    }
                    supporters.Add(prog.name, prog);
                }
                else
                {
                    topPrograms.Add(prog.name, prog);
                }
            }
            //var result = holders.Keys.Except(holders.Values.SelectMany(i => i.subTower.Select(x => x.name))).First();
            var result = supporters.Keys.Except(supportedSupporters).First();
            Console.WriteLine("Day7 Part1: result is " + result);

            var baseProg = supporters[result];

            result = getUnbalancedSubProgDiference(baseProg).ToString();
            Console.WriteLine("Day7 Part2: result is " + result);
        }


        private int getUnbalancedSubProgDiference(Program prog)
        {
            int weight = -1;
            foreach (var item in prog.subProgram)
            {
                if (weight < 0) { weight = item.weight; }
                else
                {
                    var difference = weight - item.weight;
                    if (difference > 0) { }
                }
            }
            return 0;
        }

        private class Program
        {
            public string name = "";
            public int weight;
            public List<Program> subProgram = new List<Program>();
            public Program() { }
            public Program(string name)
            {
                this.name = name;
            }
            public Program(string name, int weight) : this(name)
            {
                this.weight = weight;
            }
        }

    }


}
