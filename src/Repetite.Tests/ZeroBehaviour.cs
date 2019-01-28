using System;

namespace Repetite.Tests
{
    public class ZeroBehaviour : IBehaviour
    {
        public string Id => "Tests.Zero";
        public string Name => "Zero";

        public Input[] Inputs => Array.Empty<Input>();
        public Output[] Outputs => new[] {new Output(OutputType.Integer, "Zero")};

        public IValueBag Execute(IValueBag inputs)
        {
            return new BasicValueBag()
            {
                {"Zero", 0}
            };
        }
    }
}