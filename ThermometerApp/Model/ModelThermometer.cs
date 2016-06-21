using System;
using System.Windows.Threading;

namespace ThermometerApp
{
    /// <summary>
    /// ModelThermometer er det termometer, der opfylder de krav der er stillet i opgaven
    /// </summary>
    public class ModelThermometer : Thermometer
    {
        #region Private fields
        private DispatcherTimer timer;

        private int counter;

        #endregion

        public ModelThermometer(int serial, double warning, double alarm, MeasureType measuretype)
            : base(serial, warning, alarm, ModelConfig.MinTemperatures[measuretype], ModelConfig.MaxTemperatures[measuretype])
        {
            ThermometerMeasureType = measuretype;

            counter = 0;
            Temperature = ModelConfig.MinTemperatures[measuretype];
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, ModelConfig.DispatcherTimerMilliSeconds[ThermometerMeasureType]);
            timer.Tick += (s, e) =>
            {
                Temperature = ModelConfig.TemperatureIncrementFunctions[ThermometerMeasureType](++counter % ModelConfig.CounterPeriod[ThermometerMeasureType]);

            };
            timer.Start();

        }

        #region Methods
        //bruges af OnMouseOverCommand
        public override void Stop()
        {
            timer.Stop();

        }
        //Bruges af OnMouseOutCommand
        public override void Start()
        {
            timer.Start();
        }

        #endregion

    }
}