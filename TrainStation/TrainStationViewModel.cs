using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TrainStation
{
    internal class TrainStationViewModel : ObservableObject
    {
        private ImageSource imageSource;
        public ImageSource ImageSource {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        private ObservableCollection<StataionPark> parks;

        public ObservableCollection<StataionPark> Parks {
            get => parks;
            set => SetProperty(ref parks, value);
        }

        private StataionPark selectedPark;

        public StataionPark SelectedPark {
            get => selectedPark;
            set => SetProperty(ref selectedPark, value);
        }

        public TrainStationViewModel() {

            //TestCommand = new RelayCommand(Test);
            StationStructure.CreateStructure();
            var stations = StationStructure.Lines;
            Parks = new ObservableCollection<StataionPark>(StationStructure.Parks);
            var shapeOutlinePen = new Pen(Brushes.Black, 0.5);
            var dGroup = new DrawingGroup();
            using (var dc = dGroup.Open()) {
                foreach (var station in stations) {
                    dc.DrawLine(shapeOutlinePen, station.firstPoint, station.secondPoint);

                }
            }

            var dImageSource = new DrawingImage(dGroup);
            ImageSource = dImageSource;
        }

        public void FillPark (int index) {

        }
        public ICommand TestCommand { get; }
        private void Test() => MessageBox.Show("123");
    }
}
