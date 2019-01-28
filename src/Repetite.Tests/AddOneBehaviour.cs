using System.Collections.Generic;
using System.Linq;

namespace Repetite.Tests
{
    public class AddOneBehaviour : IBehaviour
    {
        public string Id => "Tests.AddOne";
        public string Name => "AddOne";

        public Input[] Inputs => new[]
        {
            DefaultInputs.Int("Value")
        };

        public Output[] Outputs => new[]
        {
            new Output(OutputType.Integer, "Value")
        };

        public IValueBag Execute(IValueBag inputs)
        {
            if (!inputs.TryGet("Value", out int value))
                throw new MissingInputException("Value");

            return new BasicValueBag {{"Value", value + 1}};
        }

        public T Default<T>(string value)
        {
            var input = Inputs.FirstOrDefault(i => i.Name == value)
                        ?? throw new KeyNotFoundException();

            return (T) input.DefaultValue;
        }
    }
}