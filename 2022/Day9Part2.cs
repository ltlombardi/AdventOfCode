using System.Drawing;

class Day9Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day9Input.txt");
        var result = 0;
        var tailsPositions = Enumerable
            .Range(0, 9)
            .Select(_ => new List<Point> { new Point() })
            .ToArray();
        var headPos = new Point();
        foreach (var line in lines)
        {
            var move = line.Split(' ');
            var direction = move[0];
            var steps = int.Parse(move[1]);
            for (int i = 0; i < steps; i++)
            {
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
                for (int j = 0; j < tailsPositions.Length; j++)
                {
                    var lead = j == 0 ? headPos : tailsPositions[j-1].Last();
                    tailsPositions[j].Add(MoveAfterHead(lead, tailsPositions[j].Last()));
                }
            }
        }
        result = tailsPositions.Last().Distinct().Count();
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

    private static Point MoveAfterHead(Point headPos, Point tailPos)
    {
        if (!IsTouching(headPos, tailPos))
        {
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
        }
        return tailPos;
    }
}
