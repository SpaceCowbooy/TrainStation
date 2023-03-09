using System.Collections.Generic;
using System.Windows;

namespace TrainStation.Models
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
        public double length;
        public int index;
        public StationLine(int index, Point firstPoint, Point secondPoint, double length) {
            this.firstPoint = firstPoint;
            this.secondPoint = secondPoint;
            this.length = length;
            this.index = index;
        }
        public StationLine(double x1, double y1, double x2, double y2, double length) {
            firstPoint = new Point(x1, y1);
            secondPoint = new Point(x2, y2);
            this.length = length;
        }
    }

    public class StataionWay {
        public List<StationLine> lines = new List<StationLine>();
        public StataionWay(List<StationLine> lines) {
            this.lines.AddRange(lines);
        }
    }

    public class StataionPark
    {
        public string name;
        public List<StataionWay> linesOfWay = new List<StataionWay>();
        public StataionPark(string name, List<StataionWay> ways) {
            this.name = name;
            linesOfWay.AddRange(ways);
        }
    }

    public static class StationStructure {
       
        //public static List<StationLine> Lines = new List<StationLine>();
        public static List<StataionPark> Parks = new List<StataionPark>();
        public static Dictionary<int, Point> Points = new Dictionary<int, Point> {
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
        public static readonly Dictionary<string, List<Point>> ParkFill = new Dictionary<string, List<Point>> {
            {"Красивый", new List<Point>{
                Points[1],
                Points[5],
                Points[9],
                Points[7],
            } },
            {"Сочный", new List<Point>{
                Points[11],
                Points[7],
                Points[9]
            } },
        };

        public static List<StationLine> Lines = new List<StationLine> {
                new StationLine(1, Points[1], Points[2], 30),
                new StationLine(2, Points[1], Points[3], 40),
                new StationLine(3, Points[1], Points[5], 15),

                new StationLine(4, Points[2], Points[7], 55),

                new StationLine(5, Points[3], Points[4], 25),
                new StationLine(6, Points[3], Points[5], 20),
                new StationLine(7, Points[3], Points[7], 80),

                new StationLine(8, Points[4], Points[6], 40),
                new StationLine(9, Points[4], Points[8], 20),
                new StationLine(10, Points[4], Points[9], 100),

                new StationLine(11, Points[5], Points[6], 10),

                new StationLine(12, Points[6], Points[10], 70),
                new StationLine(13, Points[6], Points[11], 70),

                new StationLine(14, Points[7], Points[8], 40),
            };
    public static void CreateStructure() {
            var l = new Dictionary<int, StationLine> {
                { 1, new StationLine(1, Points[1], Points[2], 30) },
                { 2, new StationLine(2, Points[1], Points[3], 40) },
                { 3, new StationLine(3, Points[1], Points[5], 15) },
                { 4, new StationLine(4, Points[2], Points[7], 55) },

                { 5, new StationLine(5, Points[3], Points[4], 25) },
                { 6, new StationLine(6, Points[3], Points[5], 20) },
                { 7, new StationLine(7, Points[3], Points[7], 80) },

                { 8, new StationLine(8, Points[4], Points[6], 40) },
                { 9, new StationLine(9, Points[4], Points[8], 20) },
                { 10, new StationLine(10, Points[4], Points[9], 100) },

                { 11, new StationLine(11, Points[5], Points[6], 10) },

                { 12, new StationLine(12, Points[6], Points[10], 70) },
                { 13, new StationLine(13, Points[6], Points[11], 70) },

                { 14, new StationLine(14, Points[7], Points[8], 40) }
            };

            var ways = new Dictionary<int, StataionWay> {
                {1,  new StataionWay(new List<StationLine> {l[2], l[5], l[10] }) },
                {2,  new StataionWay(new List<StationLine> {l[4], l[7], l[6], l[3] }) },
                {3,  new StataionWay(new List<StationLine> {l[13], l[12]}) },
                {4,  new StataionWay(new List<StationLine> {l[8], l[9], l[14] }) },
            };

            Parks = new List<StataionPark> {
                { new StataionPark ("Красивый", new List<StataionWay> {ways[1], ways[2] }) },
                { new StataionPark ("Сочный", new List<StataionWay> {ways[3], ways[4] }) },
            };
        }
    }
}