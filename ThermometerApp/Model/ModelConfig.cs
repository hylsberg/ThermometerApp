using System;
using System.Collections.Generic;

namespace ThermometerApp
{
    /// <summary>
    /// Denne klasse Indeholder opsætning af de forskllige Model termometre.
    /// </summary>
    public static class ModelConfig
    {
        #region MaxTemperatures
        public static Dictionary<MeasureType, double> MaxTemperatures = new Dictionary<MeasureType, double>()
        {
            {MeasureType.Normal , 100 },
            {MeasureType.Funky , 100 },
            {MeasureType.LargeScale , 150 },
            {MeasureType.SmallScale, 45}
           

        };
        #endregion

        #region MinTemperatures
        public static Dictionary<MeasureType, double> MinTemperatures = new Dictionary<MeasureType, double>()
        {
            {MeasureType.Normal , 0 },
            {MeasureType.Funky , 0 },
            {MeasureType.LargeScale , -50 },
            {MeasureType.SmallScale, 35}

        };
        #endregion

        #region DispatcherTimerMilliSeconds
        public static Dictionary<MeasureType, int> DispatcherTimerMilliSeconds = new Dictionary<MeasureType, int>()
        {
            {MeasureType.Normal , 100 },
            {MeasureType.Funky , 50 },
            {MeasureType.LargeScale , 150 },
            {MeasureType.SmallScale, 20}

        };
        #endregion

        #region TemperatureIncrementFunction (Delegate)
        public static Dictionary<MeasureType, Func< int, double>> TemperatureIncrementFunctions = new Dictionary<MeasureType, Func<int, double>>()
        {
            {MeasureType.Normal , c=> 100 - Math.Abs(100 - c)},
            {MeasureType.Funky , c=>
                {
                    Random rnd = new Random();
                    return 100 * Math.Sin((c + 10 * Math.Sin(c * Math.PI / 20) + (rnd.Next(0, 100) - 50) / 25) * Math.PI / 400);
                }
            },
            {MeasureType.LargeScale , c=> 150 - Math.Abs(200 - c) },
            {MeasureType.SmallScale, c=> 45 - Math.Abs(100 - c)/10.0}

        };
        #endregion

        #region CouterPeriod
        public static Dictionary<MeasureType, int> CounterPeriod = new Dictionary<MeasureType, int>()
        {
            {MeasureType.Normal , 200},
             {MeasureType.Funky , 400},
             {MeasureType.LargeScale , 400},
            {MeasureType.SmallScale,200 }
        };
        #endregion
    }
}
