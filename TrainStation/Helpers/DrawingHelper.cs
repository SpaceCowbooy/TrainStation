using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using TrainStation.Models;

namespace TrainStation.Helpers
{
    /// <summary>
    /// Класс для отображения визуала
    /// </summary>
    public class DrawingHelper
    {
        private readonly Pen blackPen = new Pen(Brushes.Black, 0.5);
        private readonly Pen redPen = new Pen(Brushes.Red, 0.5);
        private readonly System.Globalization.CultureInfo ruCulture = new System.Globalization.CultureInfo("ru-ru");
        private readonly Typeface typeface = new Typeface("Verdana");

        /// <summary>
        /// Рисует схему станции
        /// </summary>
        /// <returns> Image со схемой станции </returns>
        public DrawingImage GetStationImage() {
            var dGroup = new DrawingGroup();
            using (var dc = dGroup.Open()) {
                foreach (var line in StationStructure.Lines) {
                    dc.DrawLine(blackPen, line.firstPoint.point, line.secondPoint.point);
                }
                foreach (var point in StationStructure.Points) {
                    dc.DrawText(new FormattedText(point.index.ToString(), ruCulture,
                       System.Windows.FlowDirection.LeftToRight, typeface, 6, Brushes.Black, 2), point.point);
                }
            }
            return new DrawingImage(dGroup);
        }

        /// <summary>
        /// Отображает заливку парка
        /// </summary>
        /// <param name="park"> Выбранный парк </param>
        /// <param name="color"> Выбранный цвет </param>
        /// <returns> Image с залитым парком </returns>
        public DrawingImage FillPark(StataionPark park, SolidColorBrush color) {
            var streamGeometry = new StreamGeometry();
            using (var geometryContext = streamGeometry.Open()) {
                geometryContext.BeginFigure(StationStructure.ParkFill[park.name].First().point, true, true);
                var pointList = StationStructure.ParkFill[park.name].Select(x => x.point).ToList();
                geometryContext.PolyLineTo(pointList, true, true);
            }

            var dGroup = new DrawingGroup();
            using (var dc = dGroup.Open()) {
                foreach (var line in StationStructure.Lines) {
                    dc.DrawLine(blackPen, line.firstPoint.point, line.secondPoint.point);
                }

                foreach (var point in StationStructure.Points) {
                    dc.DrawText(new FormattedText(point.index.ToString(), ruCulture,
                       System.Windows.FlowDirection.LeftToRight, typeface, 6, Brushes.Black, 2), point.point);
                }

                dc.PushOpacity(0.5);
                dc.DrawGeometry(color, blackPen, streamGeometry);
                dc.Pop();
            }

            return new DrawingImage(dGroup);
        }

        /// <summary>
        /// Отображает кратчайший путь между участками
        /// </summary>
        /// <param name="pathIndexes"> Список всех точек пути </param>
        /// <returns> Image с подсвеченным путем </returns>
        public DrawingImage HighlightShortestWay (List<int> pathIndexes) {
            var dGroup = new DrawingGroup();
            using (var dc = dGroup.Open()) {
                foreach (var line in StationStructure.Lines) {
                    dc.DrawLine(blackPen, line.firstPoint.point, line.secondPoint.point);
                }

                foreach (var point in StationStructure.Points) {
                    dc.DrawText(new FormattedText(point.index.ToString(), ruCulture,
                       System.Windows.FlowDirection.LeftToRight, typeface, 6, Brushes.Black, 2), point.point);
                }

                for (int index = 0; index < pathIndexes.Count - 1; index++) {
                    dc.DrawLine(redPen, StationStructure.Points[pathIndexes[index]].point, StationStructure.Points[pathIndexes[index + 1]].point);
                }
            }

            return new DrawingImage(dGroup);
        }
    }
}
