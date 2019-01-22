namespace Repetite
{
    public class Input
    {
        public InputType Type { get; }
        public string Name { get; }
        public object DefaultValue { get; }

        public Input(InputType type, string name, object defaultValue)
        {
            Type = type;
            Name = name;
            DefaultValue = defaultValue;
        }

        public bool CanReceive(OutputType outputType)
        {
            switch (Type)
            {
                case InputType.Any:
                    return true;
                case InputType.Integer:
                    return outputType == OutputType.Integer;
                default:
                    return false;
            }
        }

        public bool CanReceive(object value)
        {
            switch (Type)
            {
                case InputType.Any:
                    return true;
                case InputType.Integer:
                    return value is int;
                default:
                    return false;
            }
        }
    }
}