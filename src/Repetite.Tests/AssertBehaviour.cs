using System;

namespace Repetite.Tests
{
    public class AssertBehaviour : IBehaviour
    {
        public string Id => "Tests.Assert";
        public string Name => "Assert";

        private readonly Input[] _expectedInputs;
        private IValueBag _inputs;
        public Input[] Inputs => _expectedInputs;

        public Output[] Outputs => Array.Empty<Output>();

        public IValueBag Execute(IValueBag inputs)
        {
            _inputs = inputs;
            return new BasicValueBag();
        }

        public AssertBehaviour(Input[] expectedInputs)
        {
            _expectedInputs = expectedInputs;
        }
    }
}