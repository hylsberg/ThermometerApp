using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ThermometerApp
{
    class ColorConverter : IValueConverter
    {
        /// <summary>
        /// Konverterer fra ThermometerMode til WPF Brushes farve.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ThermometerMode mode = (ThermometerMode)Enum.Parse(typeof(ThermometerMode), value.ToString());
            switch (mode)
            {
                case ThermometerMode.NormalMode:
                    return Brushes.Green;
                case ThermometerMode.WarningMode:
                    return Brushes.Yellow;
                case ThermometerMode.AlarmMode:
                    return Brushes.Red;
                default:
                    return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
