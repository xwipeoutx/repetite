namespace Repetite.Tests
{
    public class AddIntBehaviour : IBehaviour
    {
        public string Id => "Math.AddInt";
        public string Name => "AddInt";

        public Output[] Outputs => new[]
        {
            new Output(OutputType.Integer, "Result")
        };

        public Input[] Inputs => new[]
        {
            DefaultInputs.Int("First"),
            DefaultInputs.Int("Second")
        };

        public IValueBag Execute(IValueBag valueBag)
        {
            if (!valueBag.TryGet("First", out int first))
                throw new MissingInputException("First");

            if (!valueBag.TryGet("Second", out int second))
                throw new MissingInputException("Second");

            return new BasicValueBag {{"Result", first + second}};
        }
    }
}