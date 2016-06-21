using System.Collections.ObjectModel;

namespace ThermometerApp
{
    /// <summary>
    /// ViewModel for GridView.
    /// Indlæser Thermometer.Repositort.Thermometers (termometerlisten) som ObservableCollection
    /// </summary>
    public class GridViewModel: ObservableCollection<Thermometer>
    {
        private ThermometerRepository repository;

        public GridViewModel():base()
        {
            repository = ThermometerRepository.GetInstance();
            repository.PropertyChanged += (s, e) => update();
            update();
        }

        #region Methods (kun en)
        //updaterer og sorterer. Bruges af eventhandler i construktor
        private void update()
        {
            Clear();
            repository.Thermometers.Sort();
            foreach (Thermometer t in repository.Thermometers)
            {
                Add(t);
            }
        }
        #endregion
    }
}
