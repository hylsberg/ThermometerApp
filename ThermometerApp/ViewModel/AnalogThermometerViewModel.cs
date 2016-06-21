namespace ThermometerApp
{
    /// <summary>
    /// ViewModel for AnalogThermometerView. 
    /// Her bliver labels sat i liste og TubeHeight beregnet baseret på temperatur.
    /// </summary>
    public class AnalogThermometerViewModel : ThermometerViewModel
    {
        #region Properties
        public double[] LabelContent { get; private set; }

        private double tubeScale;
        public double TubeScale
        {
            get { return tubeScale; }
            private set
            {
                tubeScale = value;
                OnPropertyChanged("TubeScale");
            }
     
        }
        #endregion

        public AnalogThermometerViewModel(Thermometer t) : base(t)
        {
            LabelContent = new double[11];
            for (int i=0; i <= 10; i++)
            {
                LabelContent[i]=thermometer.MaxTemperature - i * (thermometer.MaxTemperature - thermometer.MinTemperature)/10;
            }
        }

        #region Methods (kun en)
        protected override void update()       
        {
            TubeScale = 180 / (20.0 * (thermometer.MaxTemperature - thermometer.MinTemperature)) * thermometer.Temperature - 180 * thermometer.MinTemperature / (20.0 * (thermometer.MaxTemperature - thermometer.MinTemperature)) + 1.0;
        }
        #endregion
    }
}
