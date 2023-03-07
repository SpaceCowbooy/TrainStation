using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TrainStation
{
    internal class TrainStationViewModel : ObservableObject
    {
        public TrainStationViewModel() {

            TestCommand = new RelayCommand(Test);
            var stations = StationStructure.GetStations();
            var shapeOutlinePen = new Pen(Brushes.Black, 0.1);
            var dGroup = new DrawingGroup();
            using (var dc = dGroup.Open()) {
                foreach (var station in stations) {
                    dc.DrawLine(shapeOutlinePen,station.firstPoint, station.secondPoint);
                }
            }

            var dImageSource = new DrawingImage(dGroup);
            Source = dImageSource;
        }
        private ImageSource source;
        public ImageSource Source {
            get => source;
            set => SetProperty(ref source, value);
        }

        public ICommand TestCommand { get; }
        private void Test() => MessageBox.Show("123");
    }
}
