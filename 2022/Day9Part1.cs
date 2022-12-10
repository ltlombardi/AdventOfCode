using System.Drawing;

class Day9Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day9Input.txt");
        var result = 0;
        var headPosition = new Point();
        var tailPos = new Point();
        var tailPositions = new HashSet<Point> { tailPos };
        foreach (var line in lines)
        {
            var move = line.Split(' ');
            var direction = move[0];
            var steps = int.Parse(move[1]);
            for (int i = 0; i < steps; i++)
            {
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
                if (IsTouching(headPosition, tailPos))
                {
                    continue;
                }
                if (IsHeadUp(headPosition, tailPos))
                {
                    tailPos.Y++;
                }
                if (IsHeadDown(headPosition, tailPos))
                {
                    tailPos.Y--;
                }
                if (IsHeadRight(headPosition, tailPos))
                {
                    tailPos.X++;
                }
                if (IsHeadLeft(headPosition, tailPos))
                {
                    tailPos.X--;
                }
                tailPositions.Add(tailPos);
            }
        }
        result = tailPositions.Count();
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
