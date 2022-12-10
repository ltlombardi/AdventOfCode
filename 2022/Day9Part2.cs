using System.Drawing;

class Day9Part2
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day9Input.txt");
        var result = 0;
        var knotPositions = Enumerable
            .Range(0, 9)
            .Select(_ => new List<Point> { new Point() })
            .ToArray();
        var headPosition = new Point();
        foreach (var line in lines)
        {
            var move = line.Split(' ');
            var direction = move[0];
            var steps = int.Parse(move[1]);
            for (int i = 0; i < steps; i++)
            {
                headPosition = MoveHead(headPosition, direction);
                for (int j = 0; j < knotPositions.Length; j++)
                {
                    var leadPosition = j == 0 ? headPosition : knotPositions[j - 1].Last();
                    knotPositions[j].Add(MoveKnot(leadPosition, knotPositions[j].Last()));
                }
            }
        }
        result = knotPositions.Last().Distinct().Count();
        return result.ToString();
    }

    private static Point MoveHead(Point headPosition, string direction)
    {
        // Point is a value type, not reference type. 
        // Changes here don't affect the original parameter
        if (direction == "R")
        {
            headPosition.X += 1;
        }
        if (direction == "L")
        {
            headPosition.X -= 1;
        }
        if (direction == "U")
        {
            headPosition.Y += 1;
        }
        if (direction == "D")
        {
            headPosition.Y -= 1;
        }
        return headPosition;
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

    private static Point MoveKnot(Point headPos, Point tailPos)
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
