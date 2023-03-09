using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TrainStation.Models;
using TrainStation.Helpers;

namespace TrainStation
{
    internal class TrainStationViewModel : ObservableObject
    {
        public class ColorDisplay {
            public SolidColorBrush color;
            public string name;
            public ColorDisplay(SolidColorBrush color, string name) {
                this.color = color;
                this.name = name;
            }
        }
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



        private ObservableCollection<SolidColorBrush> colors
         = new ObservableCollection<SolidColorBrush> { Brushes.Green, Brushes.Red, Brushes.Yellow, Brushes.Blue };
        public ObservableCollection<SolidColorBrush> Colors {
            get => colors;
            set => SetProperty(ref colors, value);
        }

        private SolidColorBrush selectedColor = Brushes.Green;

        public SolidColorBrush SelectedColor {
            get => selectedColor;
            set => SetProperty(ref selectedColor, value);
        }

        private StataionPark selectedPark;

        public StataionPark SelectedPark {
            get => selectedPark;
            set => SetProperty(ref selectedPark, value);
        }

        private readonly DrawingHelper drawingHelper = new DrawingHelper();
        public TrainStationViewModel() {

            FillSelectedParkCommand = new RelayCommand(FillPark);
            StationStructure.CreateStructure();
            Parks = new ObservableCollection<StataionPark>(StationStructure.Parks);
            ImageSource = drawingHelper.GetStationImage();
        }

        public void FillPark () {
            if (SelectedPark == null) {
                MessageBox.Show("Выберите парк для заливки", "Предупреждение");
                return;
            }
            ImageSource = drawingHelper.FillPark(SelectedPark, SelectedColor);
        }
        public ICommand FillSelectedParkCommand { get; }
    }
}
