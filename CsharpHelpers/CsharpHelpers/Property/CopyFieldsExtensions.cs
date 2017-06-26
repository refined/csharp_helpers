using System;
using System.Linq;

namespace CsharpHelpers.Property
{
    public static class CopyFieldsAndPropertiesExtensions
    {
        public static void CopyFields<T>(this T source, T target)
        {
            var type = typeof(T);
            foreach (var sourceProperty in type.GetProperties())
            {
                var targetProperty = type.GetProperty(sourceProperty.Name);
                var attr = (DoNotCopyAttribute[])targetProperty.GetCustomAttributes(typeof(DoNotCopyAttribute), false);
                if (attr.Any()) continue;
                
                if (targetProperty.GetSetMethod() != null)
                {
                    targetProperty.SetValue(target, sourceProperty.GetValue(source, null), null);
                }
            }
            foreach (var sourceField in type.GetFields())
            {
                var targetField = type.GetField(sourceField.Name);

                var attr = (DoNotCopyAttribute[])targetField.GetCustomAttributes(typeof(DoNotCopyAttribute), false);
                if (attr.Any()) continue;
                
                targetField.SetValue(target, sourceField.GetValue(source));
            }
        }
    }

    public class DoNotCopyAttribute : Attribute
    {

    }
}
