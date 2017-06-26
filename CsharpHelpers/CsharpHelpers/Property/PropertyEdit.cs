using System.Reflection;

namespace CsharpHelpers.Property
{
    public class PropertyEdit : NotifyPropertyChanged
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly object _obj;

        public string Name => _propertyInfo.Name;

        public int Value
        {
            get { return (int)_propertyInfo.GetValue(_obj); }
            set { _propertyInfo.SetValue(_obj, value); OnPropertyChanged();}
        }

        public PropertyEdit(PropertyInfo propertyInfo, object obj)
        {
            _propertyInfo = propertyInfo;
            _obj = obj;
        }
    }
}
