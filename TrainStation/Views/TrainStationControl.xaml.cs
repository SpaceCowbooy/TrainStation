using System.Windows.Controls;

namespace TrainStation.Views
{
    /// <summary>
    /// Interaction logic for TrainStationControl.xaml
    /// </summary>
    public partial class TrainStationControl : UserControl
    {
        public TrainStationControl() {
            InitializeComponent();
            DataContext = new TrainStationViewModel();
        }
    }
}
