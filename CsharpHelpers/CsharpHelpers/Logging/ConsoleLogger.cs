using CsharpHelpers.MessageProvider;

namespace CsharpHelpers.Logging
{
    public class ConsoleLogger : LoggerBase
    {
        public ConsoleLogger(ConsoleProvider provider) : base(provider)
        {
        }
    }
}