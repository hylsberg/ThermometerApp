using System;
using System.Windows.Input;

namespace ThermometerApp
{
    /// <summary>
    /// ViewModel til MainWindowView. Hovedskærmen til oprettelse af termometre.
    /// Her er UI-logikken placeret.
    /// </summary>
    public class MainWindowViewModel : CommonBase
    {

        private IWindowFactory windowFactory;

        #region Command Properties
        private RelayCommand overviewCommand;
        public RelayCommand OverviewCommand
        {
            get
            {
                if (overviewCommand == null)
                {
                    overviewCommand = new RelayCommand(o => { WindowFactory.createGridView(); },
                                                       o => { return Model.Repository.Thermometers.Count != 0; });
                }
                return overviewCommand;
            }

          
        }

        private RelayCommand createCommand;
        public ICommand CreateCommand
        {
            get
            {
                if (createCommand == null)
                {
                    createCommand = new RelayCommand(o => CreateThermometer(), o => canCreateThermometer());
                }
                return createCommand;
            }
        }

        private RelayCommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (clearCommand == null)
                {
                    clearCommand = new RelayCommand(o => Clear());
                }
                return clearCommand;
            }
        }

        #endregion

        #region Properties
        private MainModel model;
        public MainModel Model
        {
            get { return model; }
            private set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        private bool isAnalogChecked;
        public bool IsAnalogChecked
        {
            get { return isAnalogChecked; }
            set
            {
                isAnalogChecked = value;
                Model.Type = (value) ? ThermometerType.Analog : ThermometerType.Digital ;
                OnPropertyChanged("IsAnalogChecked");
            }
        }

        private bool isDigitalChecked;
        public bool IsDigitalChecked
        {
            get { return isDigitalChecked; }
            set
            {
                isDigitalChecked = value;
                Model.Type = (value) ? ThermometerType.Digital : ThermometerType.Analog;
                OnPropertyChanged("IsDigitalChecked");
            }
        }

        private string messageToDisplay;
        public string MessageToDisplay
        {
            get { return messageToDisplay; }
            private set
            {
                messageToDisplay = value;
                OnPropertyChanged("messageToDisplay");
            }
        }

  
        public IWindowFactory WindowFactory
        {
            get
            {
                return windowFactory;
            }

            set
            {
                windowFactory = value;
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            Model = new MainModel();
            IsDigitalChecked = true;
        }

        #region Methods
        private bool canCreateThermometer()
        {
            bool canCreate = Model.canCreateThermometer();
            MessageToDisplay = Model.Error;
            return canCreate;
        }

        //laver Termometer (ved at kalde MainModel) kalder derefter CreateThermometerViewModel
        //får til sidst windowfactory til at lave et vindue
        private void CreateThermometer()
        {
            MessageToDisplay = String.Empty;
            if(Model.canCreateThermometer()){
                Thermometer thermometer = Model.CreateThermometer();
                ThermometerViewModel thermometerViewModel = CreateThermometerViewModel(thermometer);
       
             
                WindowFactory.createNewWindow(thermometerViewModel);
                Clear();
            }

        }


        //Laver viewModel baseret på termometer og visningstypen (som er en property der bindes til view).
        private ThermometerViewModel CreateThermometerViewModel(Thermometer thermometer)
        {
            ThermometerViewModel thermometerViewModel;
            if (Model.Type == ThermometerType.Analog)
            {
                thermometerViewModel = new AnalogThermometerViewModel(thermometer);
            }
            else if (Model.Type == ThermometerType.Digital)
            {
                thermometerViewModel = new DigitalThermometerViewModel(thermometer);
            } else
            {
                thermometerViewModel = new DigitalThermometerViewModel(thermometer);
            }
            

            return thermometerViewModel;
        }
    
        //sletter de indtastede værdier i MainWindowViewModel og MainModel
        private void Clear()
        {
            Model.Clear();
            IsAnalogChecked = false;
            IsDigitalChecked = true;
        }
        #endregion
    }
}
