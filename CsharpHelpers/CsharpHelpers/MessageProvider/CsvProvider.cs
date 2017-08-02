using System;
using System.Collections.Generic;
using System.IO;

namespace CsharpHelpers.MessageProvider
{
    public class CsvProvider : IMessageProvider
    {
        public const string SEPARATOR = ";";
        public const string EXTENSION = ".csv";
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
                File.AppendAllText(FilePath, message + Environment.NewLine);
            }
        }

        public void Add(IEnumerable<string> cells)
        {
            lock (_locker)
            {
                File.AppendAllText(FilePath, string.Join(SEPARATOR, cells) + Environment.NewLine);
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                try
                {
                    var destination = Copy();
                    if (destination != null)
                    {
                        File.WriteAllText(FilePath, string.Empty);
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        if (File.Exists(FilePath))
                        {
                            var destination = Copy();
                            if (destination != null)
                            {
                                File.Delete(FilePath);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Add("Exception: cant clear log");
                    }
                }
            }
        }

        public string GetFullText()
        {
            lock (_locker)
            {
                return File.ReadAllText(FilePath);
            }
        }


        public string Copy(string destination = null)
        {
            if (destination == null)
            {
                var now = $"{DateTime.Now:yyyy_MM_dd_HH_mm_ss}";
                destination = FilePath.Remove(FilePath.Length - EXTENSION.Length) 
                    + "_" + now + EXTENSION;
            }

            try
            {
                File.Copy(FilePath, destination);
            }
            catch (Exception e)
            {
                Add("Exception: cant copy file: " + e.Message);
                return null;
            }

            return destination;
        }

        public Dictionary<int, string[]> Read()
        {
            var lines = new Dictionary<int, string[]>();
            using (var reader = new StreamReader(FilePath))
            {
                string line;
                int i = 0;
                var separator = SEPARATOR[0];
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
