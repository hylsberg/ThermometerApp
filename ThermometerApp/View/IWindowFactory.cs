namespace ThermometerApp
{
    public interface IWindowFactory
    {
        void createNewWindow(ThermometerViewModel viewModel);

        void createGridView();
    }
}
