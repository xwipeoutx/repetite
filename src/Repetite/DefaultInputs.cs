namespace Repetite
{
    public static class DefaultInputs
    {
        public static Input Int(string name) => new Input(InputType.Integer, name, 0);
        public static Input String(string name) => new Input(InputType.String, name, 0);
        public static Input Any(string name) => new Input(InputType.Any, name, null);
    }
}