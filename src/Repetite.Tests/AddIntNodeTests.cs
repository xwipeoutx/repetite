using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using Xunit;

namespace Repetite.Tests
{
    public class AddIntNodeTests
    {
        [Fact]
        public void HasTwoIntegerInputs()
        {
            var nullNode = new AddIntBehaviour();
            var input1 = nullNode.Inputs.First();
            var input2 = nullNode.Inputs.Skip(1).First();

            input1.Type.Should().Be(InputType.Integer);
            input1.Name.Should().Be("First");
            input2.Type.Should().Be(InputType.Integer);
            input2.Name.Should().Be("Second");
        }

        [Fact]
        public void HasOneIntegerOutput()
        {
            var addIntNode = new AddIntBehaviour();
            var output = addIntNode.Outputs.Single();
            output.Type.Should().Be(OutputType.Integer);
            output.Name.Should().Be("Result");
        }

        [Fact]
        public void WhenExecutedWithValues_SumIsOutput()
        {
            var addIntNode = new AddIntBehaviour();

            var num1 = new Random().Next(1, 100);
            var num2 = new Random().Next(1, 100);
            var values = new BasicValueBag() {{"First", num1}, {"Second", num2}};
            var outputs = addIntNode.Execute(values);

            outputs.TryGet("Result", out int result).Should().BeTrue();
            result.Should().Be(num1 + num2);
        }
    }
};