using System.Drawing;

class Day15Part2
{
    internal static async Task<(string, string)> Solution()
    {
        return (await Solve("Day15ExampleInput.txt", 20), await Solve("Day15Input.txt", 4000000));
    }

    internal static async Task<string> Solve(string inputFileName, int MaxAxisValue)
    {
        // doing like part 1 would take hours.. would need to check all the lines and all the columns in the grid
        // In a 4 million by 4 million square, there is only 1 point not in range of all the sensor. 
        // For that to be true, that point must be by de side of at least 3 ou 4 sensor ranges, just outside their range
        // 1 index of difference from the sensor edge. So, check all the points in all the edges +1 of all the sensor area
        var lines = await File.ReadAllLinesAsync(inputFileName);
        var sensors = lines.Select(Converter).ToList();
        var result = 0L;

        var offset = 1; // go the outside edge;
        var outsideEdgeOfSensorRange = sensors
            .SelectMany(s =>
            {
                var range = Enumerable.Range(0, s.SensorRadius + 1).ToList();
                var topLeft = range.Select(i => new Point(s.MinX - offset + i, s.Pos.Y - i));
                var botLeft = range.Select(i => new Point(s.MinX - offset + i, s.Pos.Y + i));
                var topRight = range.Select(i => new Point(s.MaxX + offset - i, s.Pos.Y - i));
                var botRight = range.Select(i => new Point(s.MaxX - offset - i, s.Pos.Y + i));
                return topLeft.Concat(botRight).Concat(botLeft).Concat(topRight);
            })
            .Where(p => p.X > 0 && p.X < MaxAxisValue
                        && p.Y > 0 && p.Y < MaxAxisValue)
            .ToList();
        var distressSignalPos = new Point();
        foreach (var position in outsideEdgeOfSensorRange)
        {
            if (sensors.All(s => s.DistanceTo(position) > s.SensorRadius))
            {
                distressSignalPos = position;
            }
        }
        result = 4000000L * distressSignalPos.X + distressSignalPos.Y;
        return result.ToString();
    }

    private static Sensor Converter(string item, int index)
    {
        var parts = item.Split(' ');
        var x = int.Parse(parts[2]["x=".Length..^1]);
        var y = int.Parse(parts[3]["y=".Length..^1]);
        var sensorPos = new Point(x, y);
        x = int.Parse(parts[8]["x=".Length..^1]);
        y = int.Parse(parts[9]["y=".Length..]);
        var beaconPos = new Point(x, y);
        return new Sensor(sensorPos, beaconPos);
    }

    class Sensor
    {
        public Point Pos { get; set; }
        public Point BeaconPos { get; set; }
        public int MinX { get; set; }
        public int MinY { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int SensorRadius { get; set; }

        public Sensor(Point pos, Point beacon)
        {
            Pos = pos;
            BeaconPos = beacon;
            var manhattanDistance = Math.Abs(pos.X - beacon.X) + Math.Abs(pos.Y - beacon.Y);
            SensorRadius = manhattanDistance;
            MinX = pos.X - manhattanDistance;
            MaxX = pos.X + manhattanDistance;
            MinY = pos.Y - manhattanDistance;
            MaxY = pos.Y + manhattanDistance;
        }

        public int DistanceTo(Point point)
        {
            return Math.Abs(Pos.X - point.X) + Math.Abs(Pos.Y - point.Y);
        }
    }
}
