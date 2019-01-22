using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Repetite.Tests
{
    public class OutputTests
    {
        [Fact]
        public void GivenAnyInput_CanReceiveAllOutputs()
        {
            var input = new Input(InputType.Any, "SomeKey", "Default");

            var outputTypes = Enum.GetValues(typeof(OutputType)).Cast<OutputType>();
            foreach (var outputType in outputTypes)
            {
                input.CanReceive(outputType).Should().BeTrue();
            }
        }
    }
}