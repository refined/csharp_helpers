using CsharpHelpers.Logging.MessageProviders;

namespace CsharpHelpers.Logging
{
    public interface ILogger
    {
        void AddMessage(string message);
        void AddMessage(string[] message);
        void AddException(string message);

        IMessageProvider Provider { get; }
    }
}
