using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day3
    {
        public static void Do()
        {
            string input = System.IO.File.ReadAllText(@"../../Day03.txt");
            var position = int.Parse(input);
            int ring = (int)Math.Ceiling(Math.Sqrt(position));
            var distFromRingdouble = ring * ring - position;
            var distInSides = distFromRingdouble / ring;
            var restOfDiv = distFromRingdouble % ring;
            var ringDoubleX = (ring - 1) / 2;
            var ringDoubleY = -(ring - 1) / 2;
            int steps = 0;
            if (distInSides == 0) // same side of the squared ring
            {
                steps = Math.Abs(ringDoubleX - distFromRingdouble) + Math.Abs(ringDoubleY);
            }
            else if (distInSides == 1)
            {
                if (restOfDiv < ring - 1) // side perpendicular to the left
                {
                    steps = Math.Abs(ringDoubleX - (ring - 1)) + Math.Abs(ringDoubleY + restOfDiv + 1);
                }
                else // side opposed
                {
                    steps = Math.Abs(ringDoubleX - (ring - 1) + (restOfDiv - (ring - 2))) + Math.Abs(ringDoubleY + (ring - 1));
                }
            }
            else if (distInSides == 2)
            {
                if (restOfDiv < ring - 4) // side opposed
                {
                    steps = Math.Abs(ringDoubleX - restOfDiv) + Math.Abs(ringDoubleY + (ring - 1));
                }
                else // side perpendicular to the right
                {
                    steps = Math.Abs(ringDoubleX) + Math.Abs(ringDoubleY + (ring - restOfDiv + 1));
                }
            }

            Console.WriteLine("Day3 Part1: # of steps is " + steps);



            var spiralByCoord = new Dictionary<Point, int>();
            var coordinates = new List<Point>() {
                new Point(1, 0), new Point(1, 1), new Point(0, 1), new Point(-1, 1),
                new Point(-1, 0), new Point(-1, -1),new Point(0, -1),new Point(1, -1)
            };
            spiralByCoord.Add(new Point(0, 0), 1);
            foreach (var point in Spiral())
            {
                var value = CalcValue(point, spiralByCoord, coordinates);
                if (value > position)
                {
                    Console.WriteLine("Day3 Part2: Value is " + value);
                    break;
                }
                spiralByCoord.Add(point, value);
            }
        }

        private static IEnumerable<Point> Spiral()
        {
            var point = new Point() { x = 0, y = 0 };
            var step = 1;
            while (true)
            {
                for (var i = 0; i < step; i++)
                {
                    point.x++;
                    yield return point;
                }
                for (var i = 0; i < step; i++)
                {
                    point.y++;
                    yield return point;
                }
                step++;
                for (var i = 0; i < step; i++)
                {
                    point.x--;
                    yield return point;
                }
                for (var i = 0; i < step; i++)
                {
                    point.y--;
                    yield return point;
                }
                step++;
            }
        }

        private static int CalcValue(Point point, Dictionary<Point, int> dict, List<Point> coordinates)
        {
            int value = 0;
            foreach (var item in coordinates)
            {
                int val = 0;
                if (dict.TryGetValue(new Point(point.x + item.x, point.y + item.y), out val))
                {
                    value += val;
                }
            }
            return value;
        }
    }

    struct Point
    {
        public int x; public int y;
        public Point(int x, int y)
        {
            this.x = x; this.y = y;
        }
    }

    struct Square
    {
        public int x; public int y; public int value;
    }
}
