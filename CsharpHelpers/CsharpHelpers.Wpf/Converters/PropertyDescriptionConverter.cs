using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Data;

namespace CsharpHelpers.Wpf.Converters
{
    public sealed class PropertyDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var propertyString = parameter as string;
            if (string.IsNullOrEmpty(propertyString))
                return new ArgumentNullException(nameof(parameter)).ToString();
            var propertyPath = propertyString.Split('.');
            var type = value.GetType();
            PropertyInfo property = null;
            foreach (var propertyName in propertyPath)
            {
                property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
                HandleException(property, propertyString, propertyName, type);
                type = property.PropertyType;
            }

            return ((DisplayNameAttribute)property?.GetCustomAttributes(typeof(DisplayNameAttribute), true)[0])?.DisplayName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private void HandleException(PropertyInfo property, string parameter, string propertyName, Type type)
        {
            if (property == null)
                throw new ArgumentOutOfRangeException(parameter, parameter,
                    "Property \"" + propertyName + "\" not found in type \"" + type.Name + "\".");
        }
    }
}
