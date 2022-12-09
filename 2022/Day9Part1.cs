using System.Drawing;

class Day9Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day9Input.txt");
        var result = 0;
        var tailPositions = new List<Point> { new Point() };
        var headPos = new Point();
        foreach (var line in lines)
        {
            var move = line.Split(' ');
            var direction = move[0];
            var steps = int.Parse(move[1]);
            for (int i = 0; i < steps; i++)
            {
                var lastTailPos = tailPositions.Last();
                var tailPos = new Point(lastTailPos.X, lastTailPos.Y);
                if (direction == "R")
                {
                    headPos.X += 1;
                }
                if (direction == "L")
                {
                    headPos.X -= 1;
                }
                if (direction == "U")
                {
                    headPos.Y += 1;
                }
                if (direction == "D")
                {
                    headPos.Y -= 1;
                }
                if (IsTouching(headPos, tailPos))
                {
                    continue;
                }
                if (IsHeadUp(headPos, tailPos))
                {
                    tailPos.Y++;
                }
                if (IsHeadDown(headPos, tailPos))
                {
                    tailPos.Y--;
                }
                if (IsHeadRight(headPos, tailPos))
                {
                    tailPos.X++;
                }
                if (IsHeadLeft(headPos, tailPos))
                {
                    tailPos.X--;
                }
                tailPositions.Add(tailPos);
            }
        }
        result = tailPositions.Distinct().Count();
        return result.ToString();
    }

    private static bool IsHeadLeft(Point headPos, Point tailPos)
    {
        return headPos.X < tailPos.X;
    }

    private static bool IsHeadRight(Point headPos, Point tailPos)
    {
        return headPos.X > tailPos.X;
    }

    private static bool IsHeadDown(Point headPos, Point tailPos)
    {
        return headPos.Y < tailPos.Y;
    }

    private static bool IsHeadUp(Point headPos, Point tailPos)
    {
        return headPos.Y > tailPos.Y;
    }

    private static bool IsTouching(Point headPos, Point tailPos)
    {
        return Math.Abs(headPos.X - tailPos.X) <= 1 && Math.Abs(headPos.Y - tailPos.Y) <= 1;
    }
}
