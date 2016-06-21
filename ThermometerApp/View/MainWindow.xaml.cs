using System.Windows;

namespace ThermometerApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = window.Resources["vm"] as MainWindowViewModel;
            viewModel.WindowFactory = new WindowFactory();
        }
    }
}
