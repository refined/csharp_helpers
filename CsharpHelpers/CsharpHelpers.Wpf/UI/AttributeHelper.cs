using System.ComponentModel;
using System.Windows.Controls;

namespace CsharpHelpers.Wpf.UI
{
    public static class AttributeHelper
    {
        public static string GetNameAttributeValue<T>(string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName);
            return ((DisplayNameAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), true)[0]).DisplayName;
        }

        public static void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var desc = e.PropertyDescriptor as PropertyDescriptor;
            var att = desc.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
            if (!string.IsNullOrEmpty(att?.DisplayName))
            {
                e.Column.Header = att.DisplayName;
            }
        }
    }
}
