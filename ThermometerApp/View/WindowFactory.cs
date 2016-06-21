using System.Windows;

namespace ThermometerApp
{
    class WindowFactory : IWindowFactory
    {
        public void createNewWindow(ThermometerViewModel viewModel)
        {
            if (viewModel.GetType() == typeof(DigitalThermometerViewModel))
            {
                Window window = new DigitalThermometerView(viewModel as DigitalThermometerViewModel);
                window.Show();
            } else if (viewModel.GetType() == typeof(AnalogThermometerViewModel))
            {
                Window window = new AnalogThermometerView(viewModel as AnalogThermometerViewModel);
                window.Show();
            } 
        }

        public void createGridView()
        {
            Window GridView = new GridView();
            GridView.Show();
        }
    }
}
