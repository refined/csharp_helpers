namespace CsharpHelpers.MessageProvider
{
    public class CsvProvider : BaseFileProvider
    {
        public override string Separator => "; ";
        public override string Extension => ".csv";

        public CsvProvider(string fileName) : base(fileName)
        {
        }
    }
}
