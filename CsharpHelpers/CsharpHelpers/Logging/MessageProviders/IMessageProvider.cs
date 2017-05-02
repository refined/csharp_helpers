using System.Collections.Generic;

namespace CsharpHelpers.Logging.MessageProviders
{
    public interface IMessageProvider
    {
        void Add(string message);
        void Add(IEnumerable<string> operation);
        void Clear();
    }
}