using System.Windows.Controls;

namespace TrainStation
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
