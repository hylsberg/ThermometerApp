using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ThermometerApp
{
    /// <summary>
    /// Implementering af Interfacet INotifyPropertyChanged.
    /// </summary>
    public abstract class CommonBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Hvis Property ændrer sig så invokes event
        //[CallerMemberName] gør så man ikke behøver sende properties som tekst-strenge,
        //men istedet kan skrive dem som properties og bruge VS-intelisense :-)
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
