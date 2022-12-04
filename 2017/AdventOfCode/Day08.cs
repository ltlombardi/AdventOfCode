using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day8
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
                if (operation.comparer.BoolCompare(condRegVal, operation.condVal))
                {
                    int regVal = GetOrCreateRegValue(registersByName, operation.regName);
                    registersByName[operation.regName] = regVal + (operation.isInc ? operation.delta : -operation.delta);
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
                isInc = input[1] == "inc",
                delta = int.Parse(input[2]),
                comparer = input[5],
                condRegName = input[4],
                condVal = int.Parse(input[6])
            };
        }

        class Operation
        {
            public string regName = "";
            public int delta;
            public bool isInc;
            public string condRegName = "";
            public string comparer = "";
            public int condVal;
        }
    }

    public static class StringOperator
    {
        public static bool BoolCompare(this string symbol, int a, int b)
        {
            switch (symbol)
            {
                case ">=":
                    return a >= b;
                case "<=":
                    return a <= b;
                case ">":
                    return a > b;
                case "<":
                    return a < b;
                case "==":
                    return a == b;
                case "!=":
                    return a != b;
                default:
                    throw new InvalidOperationException(string.Format("Symbol {0} not supported", symbol));
            }
        }
    }
}
