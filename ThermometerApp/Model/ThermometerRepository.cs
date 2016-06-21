using System.Collections.Generic;
using System.ComponentModel;

namespace ThermometerApp
{
    /// <summary>
    /// Denne klasse samler termometrene i en liste og står for oprettelse af nye termometre
    /// </summary>
    public class ThermometerRepository:CommonBase
    {
        PropertyChangedEventHandler handler;
        #region Properties & fields


        private List<Thermometer> thermometers;
        public List<Thermometer> Thermometers
        {
            get
            {
                if (thermometers == null)
                {
                    thermometers = new List<Thermometer>();
                }
                return thermometers;
                
            }
            private set
            {
                thermometers = value;
                OnPropertyChanged("Repository");
            }

        }
        #endregion

        #region Static members
        private static ThermometerRepository instance = null;

        public static ThermometerRepository GetInstance()
        { 
            return instance ?? (instance = new ThermometerRepository());
        }
        #endregion

        //private constructor kaldes af getInstance (static method)
        private ThermometerRepository()
        {
            thermometers = new List<Thermometer>();
            handler= (s, e) =>
            {
                Thermometer t = (Thermometer)s;
                if (t.Delete)
                { 
                
                    Remove(t);

                }
            };

        }
        
        #region Methods
        //create thermometer (Model thermometer depenpendency hmmm) og  addere det til listen.
        public Thermometer CreateThermometer(int serialnr, double warning, double alarm, MeasureType measureType)
        {

            Thermometer thermometer;
            thermometer = new ModelThermometer(serialnr, warning, alarm, measureType);
            thermometer.PropertyChanged += handler;
          
            Thermometers.Add(thermometer);
            OnPropertyChanged("Repository");
            return thermometer;
        }
        // fjerner termometer fra listen.
        private void Remove(Thermometer t)
        {
            t.PropertyChanged -= handler;
            Thermometers.Remove(t);
            OnPropertyChanged("Repository");
        }
        #endregion
    }
}
