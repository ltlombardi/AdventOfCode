using System.Drawing;

class Day9Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day9Input.txt");
        var result = 0;
        var tailPositions = new List<Point> { new Point() };
        // var tailPositions = new List<List<Point>>();
        // Enumerable.Range(0, 9).ToList().ForEach(e => tailPositions.Add(new List<Point> { new Point() }));
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
                MoveAfterHead(ref headPos, ref tailPos, direction);
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

    private static void MoveAfterHead(ref Point leadPos, ref Point followerPos, string direction)
    {
        if (direction == "R")
        {
            leadPos.X += 1;
        }
        if (direction == "L")
        {
            leadPos.X -= 1;
        }
        if (direction == "U")
        {
            leadPos.Y += 1;
        }
        if (direction == "D")
        {
            leadPos.Y -= 1;
        }
        if (IsTouching(leadPos, followerPos))
        {
            return;
        }
        if (IsHeadUp(leadPos, followerPos))
        {
            followerPos.Y++;
        }
        if (IsHeadDown(leadPos, followerPos))
        {
            followerPos.Y--;
        }
        if (IsHeadRight(leadPos, followerPos))
        {
            followerPos.X++;
        }
        if (IsHeadLeft(leadPos, followerPos))
        {
            followerPos.X--;
        }
    }
}
