using System;
using System.Collections.Generic;
using System.IO;

namespace CsharpHelpers.MessageProvider
{
    public class CsvProvider : IMessageProvider
    {
        public const string Separator = ";";
        public string FileName { get; set; }
        public string FilePath { get; set; }

        private readonly object _locker = new object();

        public CsvProvider(string fileName)
        {
            FilePath = fileName;
        }

        public void Add(string message)
        {
            lock (_locker)
            {
                File.AppendAllText(FilePath, message);
                File.AppendAllText(FilePath, Environment.NewLine);
            }
        }

        public void Add(IEnumerable<string> cells)
        {
            lock (_locker)
            {
                File.AppendAllText(FilePath, String.Join(Separator, cells));
                File.AppendAllText(FilePath, Environment.NewLine);
            }
        }

        public void Clear()
        {
            try
            {
                File.WriteAllText(FilePath, string.Empty);
            }
            catch (Exception)
            {
                try
                {
                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public Dictionary<int, string[]> Read()
        {
            var lines = new Dictionary<int, string[]>();
            using (var reader = new StreamReader(FilePath))
            {
                string line;
                int i = 0;
                var separator = Separator[0];
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(separator);
                    lines.Add(i++, parts);
                }
            }
            return lines;
        }
    }
}
