using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CsharpHelpers.Logging.MessageProviders
{
    public class ConsoleProvider : IMessageProvider
    {
        public ObservableCollection<string> Messages = new ObservableCollection<string>();

        public void Add(string message)
        {
            Messages.Add(message);
            if (Messages.Count > 100)
            {
                Messages.Remove(Messages.First());
            }
        }

        public void Add(IEnumerable<string> operations)
        {
            Add(string.Join("\t", operations));
        }

        public void Clear()
        {
            Messages.Clear();
        }
    }
}
