using System.Linq;
using FluentAssertions;
using Xunit;

namespace Repetite.Tests
{
    public class GraphTests
    {
        [Fact]
        public void WhenCreated_NoNodes()
        {
            var graph = new Graph();
            graph.Nodes.Count.Should().Be(0);
        }

        [Fact]
        public void WhenNodeAdded_ItExists()
        {
            var graph = new Graph();
            var node = new AddOneBehaviour().ToNode();

            graph.AddNode(node);
            graph.Nodes.Single().Should().Be(node);
        }

        [Fact]
        public void WhenTwoNodesAdded_TheyExist()
        {
            var graph = new Graph();
            var node1 = new AddOneBehaviour().ToNode();
            var node2 = new AddOneBehaviour().ToNode();

            graph.AddNode(node1);
            graph.AddNode(node2);
            graph.Nodes.Should().HaveCount(2);
        }

        [Fact]
        public void CanConnectNodes()
        {
            var graph = new Graph();

            var zeroNode = new ZeroBehaviour().ToNode();
            var incrementNode = new AddOneBehaviour().ToNode();

            graph.AddNode(zeroNode);
            graph.AddNode(incrementNode);

            graph.Connect(zeroNode, "Zero", incrementNode, "Value");
        }

        [Fact]
        public void WhenExecuted_SomethingHappens()
        {
            var graph = new Graph();
            var incrementNode = new AddOneBehaviour().ToNode();
            graph.AddNode(incrementNode);
        }
    }
}