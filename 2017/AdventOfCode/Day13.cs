using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day13
    {
        public void Do()
        {
            var lines = System.IO.File.ReadLines("../../Day13.txt");
            var count = 0;
            foreach (var line in lines)
            {
                var input = line.Split(':');
                var depth = int.Parse(input[0]);
                var range = int.Parse(input[1]);
                if (GetCurrentScannerPosition(depth, range) == 0) { count += depth * range; } //at the picosecond equal to the layer's depth, the packet will be at position 0
            }
            Console.WriteLine("Day13 Part1: result is " + count);

            var rangeByFirewallDepth = lines.Select(l => l.Split(':').Select(int.Parse).ToArray()).ToDictionary(p => p[0], p => p[1]); // avoid reading file multiple times
            int delay = 0;
            while (count != 0)
            {
                count = 0;
                delay++;
                foreach (var firewallDictEntry in rangeByFirewallDepth)
                {
                    var depth = firewallDictEntry.Key;
                    var range = firewallDictEntry.Value;
                    if (GetCurrentScannerPosition(depth + delay, range) == 0)
                    {
                        count += depth * range;
                        if (depth == 0) { count++; } // make sure don't get caught even if score would be zero;
                    }
                }
            }

            Console.WriteLine("Day13 Part2: result is " + delay);
        }

        int GetCurrentScannerPosition(int picoSeconds, int range)
        {
            var stepsBeforeCycleRepeat = 2 * range - 2;
            var netStepsTaken = picoSeconds % stepsBeforeCycleRepeat;
            var stepsGoingUp = netStepsTaken - range;
            if (netStepsTaken < range) { return netStepsTaken; } // going Down
            else { return range - 2 - stepsGoingUp; } // going Up
        }
    }
}
/*
 - check current scanner position considering how many iteration happened and the layer range. If is top, increase caught severity
 -
     
     */
