using System.Windows;

namespace TrainStation.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var win = new FindWayWindow();
            win.Show();
        }
    }
}
