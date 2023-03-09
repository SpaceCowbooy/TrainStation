using System.Linq;
using System.Windows.Media;
using TrainStation.Models;

namespace TrainStation.Helpers
{
    internal class DrawingHelper
    {
        public DrawingHelper() {
            StationStructure.CreateStructure();
        }
        public DrawingImage GetStationImage() {
            var stations = StationStructure.Lines;
            var blackPen = new Pen(Brushes.Black, 0.5);
            var dGroup = new DrawingGroup();
            using (var dc = dGroup.Open()) {
                foreach (var station in stations) {
                    dc.DrawLine(blackPen, station.firstPoint, station.secondPoint);
                }
            }
            return new DrawingImage(dGroup);
        }

        public DrawingImage FillPark(StataionPark park, SolidColorBrush color) {
            var streamGeometry = new StreamGeometry();
            using (var geometryContext = streamGeometry.Open()) {
                geometryContext.BeginFigure(StationStructure.ParkFill[park.name].First(), true, true);
                geometryContext.PolyLineTo(StationStructure.ParkFill[park.name], true, true);
            }

            var blackPen = new Pen(Brushes.Black, 0.5);
            var dGroup = new DrawingGroup();
            using (var dc = dGroup.Open()) {
                foreach (var line in StationStructure.Lines) {
                    dc.DrawLine(blackPen, line.firstPoint, line.secondPoint);
                }
                dc.PushOpacity(0.5);
                dc.DrawGeometry(color, blackPen, streamGeometry);
                dc.Pop();
            }

            return new DrawingImage(dGroup);
        }
    }
}
