using System;
using System.ComponentModel;

namespace ThermometerApp
{
    // til displaytype
    public enum ThermometerType
    {
        //Default Digital
        Digital,
        Analog

    }
    // til termometer (skala ect. )
    public enum MeasureType
    {
        //Default Digital
        Normal,
        Funky,
        LargeScale,
        SmallScale

    }
    /// <summary>
    /// Modellen bag oprettelse af termometre. Her valideres også
    /// </summary>
    public class MainModel : CommonBase, IDataErrorInfo
    {
        #region Properties
        public ThermometerRepository Repository
        {
            get;
            private set;
        }
        // Serial, Warningtemperature, AlarmTemperature, Type og  ModelThermometerType 
        // er kommer fra brugerens indput i MainWindowView
        private string serialNr;
        public string SerialNr
        {
            get { return serialNr; }
            set
            {
                serialNr = value;
                OnPropertyChanged("SerialNr");
            }
        }

        private string warningTermerature;
        public string WarningTemperature
        {
            get { return warningTermerature; }
            set
            {
                warningTermerature = value;
                OnPropertyChanged("WarningTemperature");
            }
        }

        private string alarmTemperature;
        public string AlarmTemperature
        {
            get { return alarmTemperature; }
            set
            {
                alarmTemperature = value;
                OnPropertyChanged("AlarmTemperature");
            }
        }

        private ThermometerType type;
        public ThermometerType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("ThermometerType");
            }
        }

        private MeasureType modelThermometerType;
        public MeasureType ModelThermometerType
        {
            get { return modelThermometerType; }
            set
            {
                modelThermometerType = value;
                OnPropertyChanged("WarningTemperature");
                OnPropertyChanged("AlarmTemperature");
                OnPropertyChanged("ModelThermometerType");
            }
        }

        #endregion

        #region Validation properties
        private static readonly string[] validatedProperties =
        {
            "SerialNr",
            "WarningTemperature",
            "AlarmTemperature"
        };

        public string Error
        {
            get;
            private set;
        }

        public string this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }
        #endregion

        public MainModel()
        {
           Repository = ThermometerRepository.GetInstance();
        
        }

        #region Methods 
        public Thermometer CreateThermometer()
        {
            return Repository.CreateThermometer(Convert.ToInt32(SerialNr), Convert.ToDouble(WarningTemperature), Convert.ToDouble(AlarmTemperature) , ModelThermometerType);
        }

        public void Clear()
        {
            SerialNr = null;
            WarningTemperature = null;
            AlarmTemperature = null;
            Type = ThermometerType.Digital;
            ModelThermometerType = MeasureType.Normal;
        }

        //bruges af canExecute på CreateCommand
        public bool canCreateThermometer()
        {
            bool isValid = true;
            Error = null;
            foreach (string property in validatedProperties)
            {
                if (GetValidationError(property) != null)
                {
                    this.Error += GetValidationError(property);
                    isValid = false;
                }
            }
            return (WarningTemperature != null && AlarmTemperature != null && SerialNr != null && isValid);
        }
        #endregion

        #region Validation methods
        //Vælger validation metode baseret på property
        public string GetValidationError(string propertyName)
        {
            string error = null;

            switch (propertyName)
            {
                case "WarningTemperature":
                    error = validateWarningTemperature();
                    break;
                case "AlarmTemperature":
                    error = validateAlarmTemperature();
                    break;
                case "SerialNr":
                    error = validateSerialNr();
                    break;
            }
            return error;
        }

        //Nedenfor er de tre validation metode med den konkrete logik der hører til
        private string validateWarningTemperature()
        {
            double MinTemperature = ModelConfig.MinTemperatures[ModelThermometerType];
            double MaxTemperature = ModelConfig.MaxTemperatures[ModelThermometerType];
            string error = null;
            double warning;
            double alarm;
            if (!double.TryParse(WarningTemperature, out warning) && WarningTemperature != null && WarningTemperature != String.Empty)
            {
                return "Warning temperature must be a number.\n";
            }
            else if (WarningTemperature != null && WarningTemperature != String.Empty)
            {
                if (warning < MinTemperature)
                {
                    error = "Warning temperature must be higher than " + MinTemperature + ".\n";
                }
                if (double.TryParse(AlarmTemperature, out alarm) && warning >= alarm)
                {
                    error += "Warning temperature must be lower than Alarm temperature.\n";
                }
                if (warning > MaxTemperature)
                {
                    error += "Warning temperature must be lower than " + MaxTemperature + ".\n";
                }
            }
            return error;
        }


        private string validateAlarmTemperature()
        {
            double MinTemperature = ModelConfig.MinTemperatures[ModelThermometerType];
            double MaxTemperature = ModelConfig.MaxTemperatures[ModelThermometerType];
            string error = null;
            double alarm;
            if(!double.TryParse(AlarmTemperature,out alarm) && AlarmTemperature != null && AlarmTemperature != String.Empty)
            {
                return "Alarm temperature must be a number.\n";
            }
            else if (AlarmTemperature != null && AlarmTemperature != String.Empty)
            {
                if (alarm < MinTemperature)
                {
                    error = "Alarm temperature must be higher than " + MinTemperature + ".\n";
                }
                if (alarm > MaxTemperature)
                {
                    error += "Alarm temperature must be lower than " + MaxTemperature + ".\n";
                }
            }
            return error;
        }

        private string validateSerialNr()
        {
            int serial;
            if (!int.TryParse(SerialNr, out serial) && SerialNr != null && SerialNr != String.Empty)
            {
                return "Serial number must be number.\n";
            }
            else if (SerialNr == null || SerialNr == String.Empty)
            {
                return null;
            }
            else
            {
                if (serial < 1000000 || serial > 9999999)
                {
                    return "Serial number must be a 7 digits number.\n";
                }
                foreach (Thermometer t in ThermometerRepository.GetInstance().Thermometers)
                {
                    if (t.SerialNr == serial)
                    {
                        return "Serial Number already exsists.\n";
                    }

                }
                return null;
            }
         
        }
        #endregion

    }
}
