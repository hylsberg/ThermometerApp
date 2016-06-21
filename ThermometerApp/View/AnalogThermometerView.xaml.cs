using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ThermometerApp
{
    /// <summary>
    /// Code behind i AnalogThermometerView - her bliver Datacontext sat og scaletransform styret
    /// </summary>
    public partial class AnalogThermometerView : Window
    {
        public AnalogThermometerView(AnalogThermometerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Binding tubeFullHeightBinding = new Binding { Path = new PropertyPath("TubeScale"), Source = viewModel };
            BindingOperations.SetBinding(scaleTransform, ScaleTransform.ScaleYProperty, tubeFullHeightBinding);
        }

    }
}
