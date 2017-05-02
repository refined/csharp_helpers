using CsharpHelpers.Logging.MessageProviders;

namespace CsharpHelpers.Logging
{
    public class CsvLogger : LoggerBase
    {
        public CsvLogger(LogCsvProvider provider) : base(provider)
        {
        }
    }
}