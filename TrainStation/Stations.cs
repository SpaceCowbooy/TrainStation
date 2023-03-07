using System.Collections.Generic;
using System.Windows;

namespace TrainStation
{
    public class StationPoint
    {
        public Point point;
        public int index;
        public StationPoint(Point point, int index) {
            this.point = point;
            this.index = index;
        }
    }

    public class StationLine
    {
        public Point firstPoint;
        public Point secondPoint;
        public double Length;

        public StationLine(Point firstPoint, Point secondPoint, double length) {
            this.firstPoint = firstPoint;
            this.secondPoint = secondPoint;
            Length = length;
        }
        public StationLine(double x1, double y1, double x2, double y2, double length) {
            firstPoint = new Point(x1, y1);
            secondPoint = new Point(x2, y2);
            Length = length;
        }
    }

    public static class StationStructure {
        public static List<StationLine> GetStations() {
            var a = new Dictionary<int, Point> {
                { 1, new Point(160, 120) },
                { 2, new Point(200, 80) },
                { 3, new Point(240, 120) },
                { 4, new Point(280, 120) },
                { 5, new Point(200, 160) },
                { 6, new Point(240, 160) },
                { 7, new Point(280, 80) },
                { 8, new Point(320, 80) },
                { 9, new Point(400, 120) },
                { 10, new Point(320, 160) },
                { 11, new Point(200, 200) }
            };
            return new List<StationLine> {
                new StationLine(a[1], a[2], 30),
                new StationLine(a[1], a[3], 40),
                new StationLine(a[1], a[5], 15),

                new StationLine(a[2], a[1], 30),
                new StationLine(a[2], a[7], 55),

                new StationLine(a[3], a[1], 40),
                new StationLine(a[3], a[4], 25),
                new StationLine(a[3], a[5], 20),
                new StationLine(a[3], a[7], 80),

                new StationLine(a[4], a[3], 25),
                new StationLine(a[4], a[6], 40),
                new StationLine(a[4], a[8], 20),
                new StationLine(a[4], a[9], 100),

                new StationLine(a[5], a[1], 15),
                new StationLine(a[5], a[3], 20),
                new StationLine(a[5], a[6], 10),

                new StationLine(a[6], a[4], 40),
                new StationLine(a[6], a[5], 10),
                new StationLine(a[6], a[10], 70),

                new StationLine(a[7], a[2], 55),
                new StationLine(a[7], a[3], 80),
                new StationLine(a[7], a[8], 40),

                new StationLine(a[8], a[4], 20),
                new StationLine(a[8], a[7], 40),

                new StationLine(a[9], a[4], 100),

                new StationLine(a[10], a[6], 70),

                new StationLine(a[11], a[6], 50),
            };
        }
    }
}