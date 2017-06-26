using CsharpHelpers.MessageProvider;

namespace CsharpHelpers.Logging
{
    public interface ILogger
    {
        void AddMessage(string message);
        void AddMessage(string[] messages);
        void AddException(string message);

        IMessageProvider Provider { get; }
    }
}
