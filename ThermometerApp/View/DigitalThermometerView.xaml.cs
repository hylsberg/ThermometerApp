
using System.Windows;

namespace ThermometerApp
{
    /// <summary>
    ///  Code behind i DigitalThermometerView - her bliver Datacontext sat 
    /// </summary>
    public partial class DigitalThermometerView : Window
    {
        public DigitalThermometerView(DigitalThermometerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}