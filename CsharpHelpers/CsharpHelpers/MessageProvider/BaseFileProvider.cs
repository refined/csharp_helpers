using System;
using System.Collections.Generic;
using System.IO;

namespace CsharpHelpers.MessageProvider
{
    public abstract class BaseFileProvider : IMessageProvider
    {
        public abstract string Separator { get; }
        public abstract string Extension { get; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        private readonly object _locker = new object();

        protected BaseFileProvider(string fileName)
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
                File.AppendAllText(FilePath, string.Join(Separator, cells) + Environment.NewLine);
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
                var now = $"{DateTime.Now:yyyy_MM_dd__HH_mm_ss}";
                destination = FilePath.Remove(FilePath.Length - Extension.Length) 
                              + "_" + now + Extension;
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