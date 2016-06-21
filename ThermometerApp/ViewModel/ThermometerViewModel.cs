using System.ComponentModel;
using System.Windows.Input;

namespace ThermometerApp
{
    /// <summary>
    /// Base-klasse for ThermometreterviewModels (DigitalThermometerViewModel og AnalogThermometerViewModel).
    /// Her er de elementer, der er ens for de to termometerviews.
    /// </summary>
    public abstract class ThermometerViewModel : CommonBase 
    {
        #region Command Properties
        private RelayCommand onMouseOverCommand;
        public ICommand OnMouseOverCommand
        {
            get
            {
                if (onMouseOverCommand == null) {
                    onMouseOverCommand = new RelayCommand(o =>thermometer.Stop());
                }
                return onMouseOverCommand;
            }
        }
        private RelayCommand onMouseOutCommand;
        public ICommand OnMouseOutCommand
        {
            get
            {
                if (onMouseOutCommand == null)
                {
                    onMouseOutCommand = new RelayCommand(o => {
                        thermometer.Start();
                    
                    });
                }
                return onMouseOutCommand;
            }
        }

        private RelayCommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(o => Remove());
                }
                return closeCommand;
            }
        }
        #endregion

        #region Properties
        protected Thermometer thermometer;
        public Thermometer Thermometer
        {
            get
            {
                return thermometer;
            }
            private set
            {
                thermometer = value;
                //OnPropertyChanged("Thermometer");
            }

        }
        #endregion

        public PropertyChangedEventHandler handler;

        public ThermometerViewModel(Thermometer t)
        {
            thermometer = t;
            handler = (s, e) =>
            {
                update();
            };

            Thermometer.PropertyChanged += handler;
        }

        #region Methods
        protected abstract void update();

        //bruges af CloseCommand
        public void Remove()
        {

            Thermometer.PropertyChanged -= handler;  
            Thermometer.Remove();
            
        }
        #endregion
    }
}
