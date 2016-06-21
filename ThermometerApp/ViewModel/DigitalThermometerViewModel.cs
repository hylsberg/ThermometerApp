namespace ThermometerApp
{
    /// <summary>
    /// ViewModel for DigitalThermomerter View.
    /// Her bliver tekstformatet til temperaturvisningen sat.
    /// </summary>
    public class DigitalThermometerViewModel:ThermometerViewModel
    {
        #region Properties 
        private string txtTemperature;

        public string TxtTemperature
        {
            get {
                return txtTemperature;
            }
            set
            {
                txtTemperature = value;
                OnPropertyChanged("TxtTemperature");

            }
        }

        #endregion

        public DigitalThermometerViewModel(Thermometer t) : base(t)
        {
            update();
        }

        #region Methods (kun en)
        protected override void update()
        {
            TxtTemperature = Thermometer.Temperature.ToString("0.00")+ " °C";
        }
        #endregion
    }
}
