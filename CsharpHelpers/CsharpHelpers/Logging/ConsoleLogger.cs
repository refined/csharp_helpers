using CsharpHelpers.Logging.MessageProviders;

namespace CsharpHelpers.Logging
{
    public class ConsoleLogger : LoggerBase
    {
        public ConsoleLogger(ConsoleProvider provider) : base(provider)
        {
        }
    }
}