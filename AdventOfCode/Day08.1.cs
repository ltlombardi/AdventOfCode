using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day8dot1
    {
        public void Do()
        {
            var lines = System.IO.File.ReadLines("../../Day08.txt");
            var registersByName = new Dictionary<string, int>();
            var maxAtAnyPoint = 0;
            foreach (var line in lines)
            {
                var inputArray = line.Split(' ');
                var operation = GetOperation(inputArray);
                int condRegVal = GetOrCreateRegValue(registersByName, operation.condRegName);
                if (operation.shouldExecute(condRegVal))
                {
                    int regVal = GetOrCreateRegValue(registersByName, operation.regName);
                    registersByName[operation.regName] = regVal + operation.delta;
                }
                var max = registersByName.Values.Max();
                if (max > maxAtAnyPoint) maxAtAnyPoint = max;
            }

            var result = registersByName.Values.Max();
            Console.WriteLine("Day8 Part1: result is " + result);
            Console.WriteLine("Day8 Part2: result is " + maxAtAnyPoint);
        }

        private static int GetOrCreateRegValue(Dictionary<string, int> registersByName, string regName)
        {
            int registerValue = 0;
            if (!registersByName.TryGetValue(regName, out registerValue))
            {
                registersByName.Add(regName, registerValue);
            }
            return registerValue;
        }

        static Operation GetOperation(string[] input)
        {
            return new Operation()
            {
                regName = input[0],
                delta = input[1] == "inc" ? int.Parse(input[2]) : -int.Parse(input[2]),
                comparer = input[5],
                condRegName = input[4],
                condVal = int.Parse(input[6])
            };
        }

        class Operation
        {
            public string regName = "";
            public int delta;
            public string condRegName = "";
            public string comparer = "";
            public int condVal;

            public bool shouldExecute(int regValue)
            {
                switch (comparer)
                {
                    case ">=":
                        return regValue >= condVal;
                    case "<=":
                        return regValue <= condVal;
                    case ">":
                        return regValue > condVal;
                    case "<":
                        return regValue < condVal;
                    case "==":
                        return regValue == condVal;
                    case "!=":
                        return regValue != condVal;
                    default:
                        throw new InvalidOperationException(string.Format("Symbol {0} not supported", comparer));
                }
            }
        }
    }

}
