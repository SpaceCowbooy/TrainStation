using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using TrainStation.Helpers;
using TrainStation.Models;

namespace TrainStation.ViewModels
{
    internal class FindWayViewModel : ObservableObject
    {
        private ImageSource imageSource;
        public ImageSource ImageSource {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        private readonly DrawingHelper drawingHelper = new DrawingHelper();

        public FindWayViewModel() {
            var b = new Dictionary<string, StationLine>();
            foreach (var line in StationStructure.Lines) {
                b.Add(line.index.ToString(), line);
            }
            Lines = new ObservableCollection<StationLine>(StationStructure.Lines);
            ImageSource = drawingHelper.GetStationImage();
        }

        private ObservableCollection<StationLine> lines = new ObservableCollection<StationLine>();

        public ObservableCollection<StationLine> Lines {
            get => lines;
            set => SetProperty(ref lines, value);
        }

        private StationLine selectedLineFrom;

        public StationLine SelectedLineFrom {
            get => selectedLineFrom;
            set => SetProperty(ref selectedLineFrom, value);
        }

        private string text;

        public string Text {
            get => text;
            set => SetProperty(ref text, value);
        }
        private StationLine selectedLineTo;

        public StationLine SelectedLineTo {
            get => selectedLineTo;
            set => SetProperty(ref selectedLineTo, value);
        }
    }
}
