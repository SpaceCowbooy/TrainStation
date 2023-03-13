using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TrainStation.Helpers;
using TrainStation.Models;

namespace TrainStation.ViewModels
{
    /// <summary>
    /// ViewModel окна отображения кратчайшего пути
    /// </summary>
    internal class FindWayViewModel : ObservableObject
    {
        private readonly FindWayHelper findWayHelper = new FindWayHelper();
        private readonly DrawingHelper drawingHelper = new DrawingHelper();

        /// <summary>
        /// Source элемента Image, используемый для рисования
        /// </summary>
        public ImageSource ImageSource {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }
        private ImageSource imageSource;
        public FindWayViewModel() {
            FindWayCommand = new RelayCommand(FindWay);
            Points = new ObservableCollection<StationPoint>(StationStructure.Points);
            ImageSource = drawingHelper.GetStationImage();
        }

        /// <summary>
        /// Начальные точки участков
        /// </summary>
        public ObservableCollection<StationPoint> Points {
            get => points;
            set => SetProperty(ref points, value);
        }
        private ObservableCollection<StationPoint> points = new ObservableCollection<StationPoint>();

        /// <summary>
        /// Начальная точка
        /// </summary>
        public StationPoint SelectedPointFrom {
            get => selectedPointFrom;
            set => SetProperty(ref selectedPointFrom, value);
        }
        private StationPoint selectedPointFrom;
        
        /// <summary>
        /// Конечная точка
        /// </summary>
        public StationPoint SelectedPointTo {
            get => selectedPointTo;
            set => SetProperty(ref selectedPointTo, value);
        }
        private StationPoint selectedPointTo;

        /// <summary>
        /// Поиск пути из выбранных точек
        /// </summary>
        public void FindWay() {
            if (SelectedPointFrom != null && SelectedPointTo != null) {
                var pathLines = findWayHelper.FindShortestWay(SelectedPointFrom.index, SelectedPointTo.index);
                ImageSource = drawingHelper.HighlightShortestWay(pathLines);
            }
        }
        /// <summary>
        /// Команда поиска пути
        /// </summary>
        public ICommand FindWayCommand { get; }
    }
}
