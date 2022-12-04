using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day11 : IDoable
    {
        public void Do()
        {
            var line = System.IO.File.ReadAllText("../../Day11.txt");
            var coords = line.Split(',');
            var pos = new Position();
            var farthest = 0;
            var result = 0;
            foreach (var item in coords)
            {
                pos.Move(item);
                result = pos.NetTotalSteps();
                farthest = Math.Max(result, farthest);
            }

            Console.WriteLine("Day11 Part1: result is " + result);
            Console.WriteLine("Day11 Part2: result is " + farthest);
        }

        class Position
        {
            CubePoint point = new CubePoint(0, 0, 0);
            Dictionary<string, CubePoint> directions = new Dictionary<string, CubePoint>() {
                {"n", new CubePoint(0,1, -1) },{"ne", new CubePoint(1,0, -1) },{"nw", new CubePoint(-1,1,0) },
                {"sw", new CubePoint(-1,0, 1) },{"s", new CubePoint(0,-1, 1) },{"se", new CubePoint( 1,-1,0) }
            };

            public void Move(string coord)
            {
                var step = directions[coord];
                point += step;
            }

            public int NetTotalSteps()
            {
                return (Math.Abs(point.x) + Math.Abs(point.y) + Math.Abs(point.z)) / 2;
            }
        }

        class CubePoint
        {
            public int x, y, z;
            public CubePoint(int x, int y, int z)
            {
                this.x = x; this.y = y; this.z = z;
            }

            public static CubePoint operator +(CubePoint point, CubePoint step)
            {
                point.x += step.x; point.y += step.y; point.z += step.z;
                return point;
            }
        }
    }
}
