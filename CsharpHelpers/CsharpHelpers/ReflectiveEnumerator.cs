using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpHelpers
{
    public static class ReflectiveEnumerator
    {
        public static T ActivateType<T>(Type type, params object[] constructorArgs) where T : class
        {
            return (T)Activator.CreateInstance(type, constructorArgs);
        }

        public static IEnumerable<Type> GetEnumerableOfType<T>() where T : class
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes());
            return types
                .Where(p => typeof(T).IsAssignableFrom(p) && !p.IsAbstract);
        }
    }
}
