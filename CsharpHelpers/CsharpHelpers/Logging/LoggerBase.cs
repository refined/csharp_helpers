using CsharpHelpers.Logging.MessageProviders;

namespace CsharpHelpers.Logging
{
    public abstract class LoggerBase : ILogger
    {
        public IMessageProvider Provider { get; }

        protected LoggerBase(IMessageProvider provider)
        {
            Provider = provider;
        }

        public void AddMessage(string message)
        {
            Provider.Add(message);
        }

        public void AddMessage(string[] message)
        {
            Provider.Add(message);
        }

        public void AddException(string message)
        {
            AddMessage("WARNING");
            AddMessage(message);
        }
    }
}