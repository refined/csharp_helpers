using CsharpHelpers.MessageProvider;

namespace CsharpHelpers.Logging
{
    public class TxtLogger : LoggerBase
    {
        public TxtLogger(LogTxtProvider provider) : base(provider)
        {
        }
    }
}