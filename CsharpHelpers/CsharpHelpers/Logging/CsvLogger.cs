using CsharpHelpers.MessageProvider;

namespace CsharpHelpers.Logging
{
    public class CsvLogger : LoggerBase
    {
        public CsvLogger(LogCsvProvider provider) : base(provider)
        {
        }
    }
}