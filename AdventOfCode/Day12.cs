using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day12 : IDoable
    {
        Dictionary<int, int[]> connectionsById = new Dictionary<int, int[]>();

        public void Do()
        {
            var lines = System.IO.File.ReadLines("../../Day12.txt");
            foreach (var line in lines)
            {
                var id = int.Parse(line.Substring(0, line.IndexOf(' ')));
                var connections = line.Substring(line.IndexOf('>') + 2).Split(',').Select(i => int.Parse(i)).ToArray();
                connectionsById.Add(id, connections);
            }
            var groupContains0 = getAllConnections(0, new HashSet<int>() { });
            var result = groupContains0.Count;
            Console.WriteLine("Day12 Part1: result is " + result);

            var groupCount = 0;
            while (connectionsById.Count > 0)
            {
                getAllConnections(connectionsById.First().Key, new HashSet<int>() { }).ToList().ForEach(i => connectionsById.Remove(i));
                groupCount++;
            }
            Console.WriteLine("Day12 Part2: result is " + groupCount);
        }

        private HashSet<int> getAllConnections(int id, HashSet<int> set)
        {
            set.Add(id);
            var newItems = new List<int>();
            foreach (var item in connectionsById[id])
            {
                if (set.Add(item)) { newItems.Add(item); }
            }
            foreach (var item in newItems)
            {
                getAllConnections(item, set);
            }
            return set;
        }
    }
}
