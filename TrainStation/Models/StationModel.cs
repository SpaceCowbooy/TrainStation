using System.Collections.Generic;
using System.Windows;

namespace TrainStation.Models
{
    /// <summary>
    /// Точки пересечения участков (если представить станцию как граф, то это верщшины)
    /// </summary>
    public class StationPoint
    {
        /// <summary>
        /// Точка
        /// </summary>
        public Point point;

        /// <summary>
        /// Индекс
        /// </summary>
        public int index;
        public StationPoint(int index, Point point) {
            this.point = point;
            this.index = index;
        }

        /// <summary>
        /// Для отображения в ComboBox
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Начало участка #{index}";
    }

    /// <summary>
    /// Участки (в графе - ребра)
    /// </summary>
    public class StationLine
    {
        /// <summary>
        /// Первая точка
        /// </summary>
        public StationPoint firstPoint;

        /// <summary>
        /// Вторая точка
        /// </summary>
        public StationPoint secondPoint;

        /// <summary>
        /// Длина пути - используется для поиска кратчайшего пути
        /// </summary>
        public double length;

        /// <summary>
        /// Индекс участка/ребра
        /// </summary>
        public int index;
        public StationLine(int index, StationPoint firstPoint, StationPoint secondPoint, double length) {
            this.firstPoint = firstPoint;
            this.secondPoint = secondPoint;
            this.length = length;
            this.index = index;
        }

        public override string ToString() => $"Участок #{index}";
    }

    /// <summary>
    /// Путь
    /// </summary>
    public class StataionWay {
        public List<StationLine> lines = new List<StationLine>();
        public StataionWay(List<StationLine> lines) {
            this.lines.AddRange(lines);
        }
    }

    /// <summary>
    /// Парк
    /// </summary>
    public class StataionPark
    {
        public string name;
        public List<StataionWay> linesOfWay = new List<StataionWay>();
        public StataionPark(string name, List<StataionWay> ways) {
            this.name = name;
            linesOfWay.AddRange(ways);
        }

        public override string ToString() => name;
    }

    /// <summary>
    /// Для хранения сведений о станции
    /// </summary>
    public static class StationStructure {
        /// <summary>
        /// Список всех парков станции
        /// </summary>
        public static List<StataionPark> Parks = new List<StataionPark>();

        /// <summary>
        /// Список всех точек
        /// </summary>
        public static StationPoint[] Points = new StationPoint[] {
            new StationPoint(0, new Point(160, 120)),
            new StationPoint(1, new Point(200, 80)),
            new StationPoint(2, new Point(240, 120)),
            new StationPoint(3, new Point(280, 120)),
            new StationPoint(4, new Point(200, 160)),
            new StationPoint(5, new Point(240, 160)),
            new StationPoint(6, new Point(280, 80)),
            new StationPoint(7, new Point(320, 80)),
            new StationPoint(8, new Point(400, 120)),
            new StationPoint(9, new Point(320, 160)),
            new StationPoint(10, new Point(200, 200))
        };

        /// <summary>
        /// Хардкод точем, по которым заливается парк
        /// </summary>
        public static readonly Dictionary<string, List<StationPoint>> ParkFill = new Dictionary<string, List<StationPoint>> {
            {"Красивый", new List<StationPoint>{
                Points[0],
                Points[4],
                Points[8],
                Points[6],
            } },
            {"Сочный", new List<StationPoint>{
                Points[10],
                Points[6],
                Points[8]
            } },
        };
        
        /// <summary>
        /// Список всех участков
        /// </summary>
        public static StationLine[] Lines = new StationLine[] {
            new StationLine(0, Points[0], Points[1], 30),
            new StationLine(1, Points[0], Points[2], 40),
            new StationLine(2, Points[0], Points[4], 15),
            new StationLine(3, Points[1], Points[6], 55),

            new StationLine(4 ,Points[2], Points[3], 25),
            new StationLine(5, Points[2], Points[4], 20),
            new StationLine(6, Points[2], Points[6], 80),

            new StationLine(7, Points[3], Points[5], 40),
            new StationLine(8, Points[3], Points[7], 20),
            new StationLine(9, Points[3], Points[8], 100),

            new StationLine(10, Points[4], Points[5], 10),

            new StationLine(11, Points[5], Points[9], 70),
            new StationLine(12, Points[5], Points[10], 50),

            new StationLine(13, Points[6], Points[7], 40)
        };
        
        /// <summary>
        /// Заполнение парков и путей
        /// </summary>
        public static void CreateStructure() {
            var ways = new Dictionary<int, StataionWay> {
                {1,  new StataionWay(new List<StationLine> {Lines[1], Lines[4], Lines[9] }) },
                {2,  new StataionWay(new List<StationLine> {Lines[3], Lines[6], Lines[5], Lines[2] }) },
                {3,  new StataionWay(new List<StationLine> {Lines[12], Lines[11]}) },
                {4,  new StataionWay(new List<StationLine> {Lines[7], Lines[8], Lines[13] }) },
            };

            Parks = new List<StataionPark> {
                { new StataionPark ("Красивый", new List<StataionWay> {ways[1], ways[2] }) },
                { new StataionPark ("Сочный", new List<StataionWay> {ways[3], ways[4] }) },
            };
        }
    }
}