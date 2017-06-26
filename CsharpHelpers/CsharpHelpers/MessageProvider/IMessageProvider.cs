using System.Collections.Generic;

namespace CsharpHelpers.MessageProvider
{
    public interface IMessageProvider
    {
        void Add(string message);
        void Add(IEnumerable<string> messages);
        void Clear();
    }
}
