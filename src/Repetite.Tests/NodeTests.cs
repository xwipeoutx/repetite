using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Repetite.Tests
{
    public class NodeTests
    {
        [Fact]
        public void WhenGettingValue_WhenInvalidKey_Throws()
        {
            var addOneBehaviour = new AddOneBehaviour();
            var node = new Node(addOneBehaviour);

            var defaultValue = addOneBehaviour.Default<int>("Value");

            node.Invoking(n => n.TryGetValue("NotAKey", out int notFoundValue))
                .Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void WhenGettingValue_ReturnsTrueAndDefault()
        {
            var addOneBehaviour = new AddOneBehaviour();
            var node = new Node(addOneBehaviour);

            var defaultValue = addOneBehaviour.Default<int>("Value");
            var isValid = node.TryGetValue("Value", out int value);

            isValid.Should().BeTrue();
            value.Should().Be(defaultValue);
        }

        [Fact]
        public void WhenValueSet_WhenGettingValue_ReturnsTrueAndCorrectValue()
        {
            var addOneBehaviour = new AddOneBehaviour();
            var node = new Node(addOneBehaviour);

            node.SetValue("Value", 3);
            var isValid = node.TryGetValue("Value", out int value);

            isValid.Should().BeTrue();
            value.Should().Be(3);
        }
        
        [Fact]
        public void WhenExecuting_DefaultValuesAreUsed()
        {
            var addOneBehaviour = new AddOneBehaviour();
            var node = new Node(addOneBehaviour);

            var result = node.Execute(new BasicValueBag());
            result.TryGet("Value", out int value).Should().BeTrue();
            value.Should().Be(1); // 0 + 1
        }
        
        [Fact]
        public void WhenExecuting_SetValuesTakePrecedence()
        {
            var addOneBehaviour = new AddOneBehaviour();
            var node = new Node(addOneBehaviour);
            node.SetValue("Value", 3);

            var result = node.Execute(new BasicValueBag());
            result.TryGet("Value", out int value).Should().BeTrue();
            value.Should().Be(4); // 3 + 1
        }
        
        [Fact]
        public void WhenExecuting_ExternalValuesTakePrecedence()
        {
            var addOneBehaviour = new AddOneBehaviour();
            var node = new Node(addOneBehaviour);
            node.SetValue("Value", 3);

            var result = node.Execute(new BasicValueBag()
            {
                { "Value", 5 }
            });
            
            result.TryGet("Value", out int value).Should().BeTrue();
            value.Should().Be(6); // 5 + 1
        }
    }
}