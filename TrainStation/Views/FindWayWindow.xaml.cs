using System.Windows;
using TrainStation.ViewModels;

namespace TrainStation.Views
{
    /// <summary>
    /// Interaction logic for FindWayWindow.xaml
    /// </summary>
    public partial class FindWayWindow : Window
    {
        public FindWayWindow() {
            InitializeComponent();
            DataContext = new FindWayViewModel();
        }
    }
}
