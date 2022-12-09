using System.Drawing;

class Day9Part1
{
    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day9Input.txt");
        var result = 0;
        var tailPositions = new List<Point> { new Point() };
        var posHead = new Point();
        foreach (var line in lines)
        {
            var move = line.Split(' ').Select(int.Parse).ToArray();
            var direction = move[0];
            var steps = move[1];
            var posTail = tailPositions.Last();
            if (posHead == posTail)
            {
                steps--;
            }
            // if steps == 0 continue
            var newTailPos = new Point();
            if (direction == 'R')
            {
                if (IsUpOrDown(posHead, posTail))
                {
                    steps--;
                }
                if (IsDiagonalLeft(posHead, posTail))
                {
                    newTailPos = new Point(posHead.X, posTail.Y + (posHead.Y - posTail.Y));
                }
                else
                {
                    newTailPos = new Point(posHead.X - 1 + steps, posHead.Y);
                }
            }
            if (direction == 'L')
            {
                if (IsUpOrDown(posHead, posTail))
                {
                    steps--;
                }
                if (IsDiagonalRight(posHead, posTail))
                {
                    newTailPos = new Point(posHead.X - 1, posTail.Y + (posHead.Y - posTail.Y));
                }
                else
                {
                    newTailPos = new Point(posHead.X - 1 - steps, posHead.Y);
                }
            }
            if (direction == 'U')
            {
                newTailPos = new Point(posHead.X, posHead.Y - 1 + steps);
            }
            if (direction == 'D')
            {
                newTailPos = new Point(posHead.X, posHead.Y - 1 - steps);
            }
            tailPositions.Add(newTailPos);
        }
        return result.ToString();
    }

    private static bool IsDiagonalRight(Point posHead, Point posTail)
    {
        return posHead.Y != posTail.Y && posHead.X < posTail.X;
    }

    private static bool IsUpOrDown(Point posHead, Point posTail)
    {
        var diagonalcommingback = posHead.X == posTail.X && posHead.Y != posTail.Y;
        return posHead.X == posTail.X && posHead.Y != posTail.Y;
    }

    private static bool IsDiagonalLeft(Point posHead, Point posTail)
    {
        return posHead.Y != posTail.Y && posHead.X > posTail.X;
    }
}
