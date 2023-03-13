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
    /// <summary>
    /// ViewModel отображения станции и парков
    /// </summary>
    internal class TrainStationViewModel : ObservableObject
    {
        /// <summary>
        /// Класс для корректного отображения цветов в ComboBox
        /// </summary>
        public class ColorDisplay {
            public SolidColorBrush color;
            public string name;
            public ColorDisplay(SolidColorBrush color, string name) {
                this.color = color;
                this.name = name;
            }
            public override string ToString() => name;
        }

        private readonly DrawingHelper drawingHelper = new DrawingHelper();
        public TrainStationViewModel() {

            FillSelectedParkCommand = new RelayCommand(FillPark);
            Parks = new ObservableCollection<StataionPark>(StationStructure.Parks);
            ImageSource = drawingHelper.GetStationImage();
        }

        /// <summary>
        /// Source элемента Image, используемый для рисования
        /// </summary>
        public ImageSource ImageSource {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }
        private ImageSource imageSource;

        /// <summary>
        /// Список парков
        /// </summary>
        public ObservableCollection<StataionPark> Parks {
            get => parks;
            set => SetProperty(ref parks, value);
        }
        private ObservableCollection<StataionPark> parks;

        /// <summary>
        /// Список доступных цветов
        /// </summary>
        public ObservableCollection<ColorDisplay> Colors {
            get => colors;
            set => SetProperty(ref colors, value);
        }
        private ObservableCollection<ColorDisplay> colors
        = new ObservableCollection<ColorDisplay> {
             new ColorDisplay(Brushes.Red, "Красный"),
             new ColorDisplay(Brushes.Green, "Зеленый"),
             new ColorDisplay(Brushes.Yellow, "Желтый"),
             new ColorDisplay(Brushes.LightBlue, "Голубой"),
        };

        /// <summary>
        /// Выбранный в ComboBox цвет
        /// </summary>
        public ColorDisplay SelectedColor {
            get => selectedColor;
            set => SetProperty(ref selectedColor, value);
        }
        private ColorDisplay selectedColor = new ColorDisplay(Brushes.Green, "Зеленый");

        /// <summary>
        /// Выбранный в ComboBox парк
        /// </summary>
        public StataionPark SelectedPark {
            get => selectedPark;
            set => SetProperty(ref selectedPark, value);
        }
        private StataionPark selectedPark;

        /// <summary>
        /// Заливает парк выбранным цветом
        /// </summary>
        public void FillPark () {
            if (SelectedPark == null) {
                MessageBox.Show("Выберите парк для заливки", "Предупреждение");
                return;
            }
            ImageSource = drawingHelper.FillPark(SelectedPark, SelectedColor.color);
        }

        /// <summary>
        /// Команда для заливки
        /// </summary>
        public ICommand FillSelectedParkCommand { get; }
    }
}
