namespace CsharpHelpers.MessageProvider
{
    public class TxtProvider : BaseFileProvider
    {
        public override string Separator => "\n";
        public override string Extension => ".txt";

        public TxtProvider(string fileName) : base(fileName)
        {
        }
    }
}