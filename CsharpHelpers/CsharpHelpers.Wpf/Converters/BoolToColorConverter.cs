using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CsharpHelpers.Wpf.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new SolidColorBrush(Colors.LightGreen);
            }

            return System.Convert.ToBoolean(value)
                ? new SolidColorBrush(Colors.LightGreen)
                : new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
