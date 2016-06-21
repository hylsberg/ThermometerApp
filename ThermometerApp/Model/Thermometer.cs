using System;

namespace ThermometerApp
{
    public enum ThermometerMode
    {

        NormalMode,
        WarningMode,
        AlarmMode
    }
    /// <summary>
    /// Base Class til Thermometer. Abscract class.
    /// Indeholder det er ikke bør variere.
    /// </summary>
    public abstract class Thermometer: CommonBase, IComparable
    { 
        #region Properties
        private bool delete;
        public bool Delete
        {
            get { return delete; }
            private set
            {
                delete = value;
                OnPropertyChanged("Delete");
            }
        }

        private double temperature;
        public double Temperature
        {
            get { return temperature; }
            protected set
            {
                temperature = value;
                updateMode();
                OnPropertyChanged("Temperature");
            }
        }

        private double warningTemperature;
        public double WarningTemperature
        {
            get { return warningTemperature; }
            set
            {
                warningTemperature = value;
                //OnPropertyChanged("WarningTemperature");
            }
        }

        private double alarmTemperature;
        public double AlarmTemperature
        {
            get { return alarmTemperature; }
            set
            {
                alarmTemperature = value;
                //OnPropertyChanged("AlarmTemperature");
            }
        }


        private int serialNr;
        public int SerialNr
        {
            get { return serialNr; }
            set { serialNr = value; }
        }
        //Enum Thermometermode (hænger sammen med farve i view - det ved model ikke)
        private ThermometerMode mode;
        public ThermometerMode Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                OnPropertyChanged("Mode");
            }
      
        }

        public  double MaxTemperature { get; protected set; }
        public  double MinTemperature { get; protected set; }
        public MeasureType ThermometerMeasureType { get; set; }


        #endregion

        public Thermometer (int serialnr, double warning, double alarm, double mintemperature, double maxtemperature)
        {
         
            MinTemperature = mintemperature;
            MaxTemperature = maxtemperature;
            SerialNr = serialnr;
            WarningTemperature = warning;
            AlarmTemperature = alarm;
            Delete = false;
        }

        #region Methods
        // logik der opdatere Mode bruges af Temperature property set 
        private void updateMode()
        {
            if (Temperature > AlarmTemperature)
            {
                Mode = ThermometerMode.AlarmMode;
            }
            else if (Temperature > WarningTemperature)
            {
                Mode = ThermometerMode.WarningMode;
            }
            else
            {
                Mode = ThermometerMode.NormalMode;
            }
        }
      
        //til at fjerne det fra Thermometerlisten i ThermometerRepository
        public  void Remove()
        {
           
            Delete = true;
            
        }
       
        //Implementering af IComparable (til sortering - bruges i gridview)
        public int CompareTo(object obj)
        {
            if (obj is Thermometer)
            {
                return this.SerialNr - ((Thermometer)obj as Thermometer).SerialNr;
            } else
            {
                throw new ArgumentException("Wrong Comparing! Not a Thermometer Type");
            }
        }

        #endregion

        #region Abstract methods
        public abstract void Stop();

        public abstract void Start();
        
        #endregion
    }
}
