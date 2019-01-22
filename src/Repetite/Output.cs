namespace Repetite
{
    public class Output
    {
        public OutputType Type { get; }
        public string Name { get; }

        public Output(OutputType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}