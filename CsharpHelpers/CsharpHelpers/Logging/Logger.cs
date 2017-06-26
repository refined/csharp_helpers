using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using CsharpHelpers.MessageProvider;

namespace CsharpHelpers.Logging
{
    public static class Logger
    {
        public static IList<ILogger> Loggers = new List<ILogger>();
        private static readonly IList<IMessageProvider> DataProviders = new List<IMessageProvider>();
        static Logger()
        {
            var logProvider = new LogCsvProvider();
            DataProviders.Add(logProvider);
            Loggers.Add(new CsvLogger(logProvider));

            var consoleProvider = new ConsoleProvider();
            DataProviders.Add(consoleProvider);
            Loggers.Add(new ConsoleLogger(consoleProvider));
        }

        public static void Add(string message, string[] data = null)
        {
            var output = new List<string> { DateTime.Now.ToString(CultureInfo.InvariantCulture), message };
            if (data != null) output.AddRange(data);

            foreach (var logger in Loggers)
            {
                logger.AddMessage(output.ToArray());
            }
        }

        public static void Add(string message, object data)
        {
            if (data is string)
            {
                Add(message, new[] { (string)data });
            }
            else if (data != null)
            {
                var obj = data.GetType()
                    .GetProperties()
                    .Where(prop => Attribute.IsDefined((MemberInfo) prop, typeof(ShowInLogAttribute)))
                    .Select(p => p.Name + ":" + p.GetValue(data)?.ToString())
                    .ToList();
                Add(message, obj.ToArray());
            }
            else
            {
                Add(message);
            }
        }

        public static void AddException(Exception e)
        {
            Add("EXCEPTION " + e.GetType(), e.Message);
            foreach (var logger in Loggers)
            {
                if (e.InnerException != null)
                {
                    logger.AddException(e.InnerException.Message);
                }
                logger.AddException(e.StackTrace);
            }
        }

        public static T GetProvider<T>() where T : IMessageProvider
        {
            return (T)DataProviders.FirstOrDefault(p => p is T);
        }

     
    }
}