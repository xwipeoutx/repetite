using System;
using FluentAssertions;
using Xunit;

namespace Repetite.Tests
{
    public class BasicInputSourceTests
    {
        [Fact]
        public void GivenNoNodeValue_KeyNotFoundIsThrown()
        {
            var inputs = new BasicValueBag();
            inputs.TryGet("Not there", out int value).Should().BeFalse();
            value.Should().Be(default(int));
        }

        [Fact]
        public void GivenNodeValue_ItIsFound()
        {
            var inputs = new BasicValueBag();
            var givenValue = new Random().Next(1, 1000000);
            inputs.Add("Value", givenValue);
            inputs.TryGet("Value", out int value).Should().BeTrue();
            value.Should().Be(givenValue);
        }
    }
}