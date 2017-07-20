﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CsharpHelpers.MessageProvider
{
    public class ConsoleProvider : IMessageProvider
    {
        public ObservableCollection<string> Messages = new ObservableCollection<string>();

        public void Add(string message)
        {
            Messages.Add(message);
            if (Messages.Count > 200)
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

        public string GetFullText()
        {
            return string.Join("\n", Messages);
        }
    }
}