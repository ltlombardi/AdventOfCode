using System.Drawing;

class Day15Part2
{
    private const int MaxAxisValue = 20;

    internal static async Task<string> Solution()
    {
        var lines = await File.ReadAllLinesAsync("Day15Input.txt");
        var sensors = lines.Select(Converter).ToList();
        var result = 0L;
        var minX = 0;
        var maxX = MaxAxisValue;
        var minY = 0;
        var maxY = MaxAxisValue;

        var distressSignalPos = new Point();
        for (var y = minY; y < maxY; y++)
        {
            for (var x = minX; x < maxX; x++)
            {
                var position = new Point(x, y);
                if (sensors.All(s => s.SensorRadius < s.DistanceTo(position) && position != s.BeaconPos))
                {
                    distressSignalPos = position;
                }
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
