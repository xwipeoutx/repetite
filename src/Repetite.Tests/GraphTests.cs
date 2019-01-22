using System;
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

            graph.Bindings.Should().Contain(b =>
                b.SourceNode == zeroNode && b.TargetNode == incrementNode &&
                b.SourceOutput == zeroNode.Output("Zero") && b.TargetInput == incrementNode.Input("Value"));
        }

        [Fact]
        public void ConnectingNodesMultipleTimesDoesNothing()
        {
            var graph = new Graph();

            var zeroNode = new ZeroBehaviour().ToNode();
            var incrementNode = new AddOneBehaviour().ToNode();

            graph.AddNode(zeroNode);
            graph.AddNode(incrementNode);

            graph.Connect(zeroNode, "Zero", incrementNode, "Value");
            graph.Connect(zeroNode, "Zero", incrementNode, "Value");

            graph.Bindings.Should().HaveCount(1);
        }

        [Fact]
        public void CanChangeNodeConnectionNodes()
        {
            var graph = new Graph();

            var zeroNode = new ZeroBehaviour().ToNode();
            var otherZeroNode = new ZeroBehaviour().ToNode();
            var incrementNode = new AddOneBehaviour().ToNode();

            graph.AddNode(zeroNode);
            graph.AddNode(otherZeroNode);
            graph.AddNode(incrementNode);

            graph.Connect(zeroNode, "Zero", incrementNode, "Value");
            graph.Connect(otherZeroNode, "Zero", incrementNode, "Value");

            graph.Bindings.Should().Contain(b =>
                b.SourceNode == otherZeroNode && b.TargetNode == incrementNode &&
                b.SourceOutput == otherZeroNode.Output("Zero") && b.TargetInput == incrementNode.Input("Value"));
        }

        [Fact]
        public void CanConnectNodeOutputsToMultipleInputs()
        {
            var graph = new Graph();

            var zeroNode = new ZeroBehaviour().ToNode();
            var incrementNode = new AddOneBehaviour().ToNode();
            var incrementNode2 = new AddOneBehaviour().ToNode();

            graph.AddNode(zeroNode);
            graph.AddNode(incrementNode);
            graph.AddNode(incrementNode2);

            graph.Connect(zeroNode, "Zero", incrementNode, "Value");
            graph.Connect(zeroNode, "Zero", incrementNode2, "Value");

            graph.Bindings.Should().Contain(b =>
                b.SourceNode == zeroNode && b.TargetNode == incrementNode &&
                b.SourceOutput == zeroNode.Output("Zero") && b.TargetInput == incrementNode.Input("Value"));

            graph.Bindings.Should().Contain(b =>
                b.SourceNode == zeroNode && b.TargetNode == incrementNode2 &&
                b.SourceOutput == zeroNode.Output("Zero") && b.TargetInput == incrementNode2.Input("Value"));
        }

        [Fact]
        public void ExecutionOrder_FollowsAStraightLine()
        {
            var graph = new Graph();
            var zeroNode = new ZeroBehaviour().ToNode();
            var incrementNode = new AddOneBehaviour().ToNode();
            var incrementNode2 = new AddOneBehaviour().ToNode();

            graph.AddNode(incrementNode);
            graph.AddNode(zeroNode);
            graph.AddNode(incrementNode2);

            graph.Connect(incrementNode, "Value", incrementNode2, "Value");
            graph.Connect(zeroNode, "Zero", incrementNode, "Value");

            var executionOrder = graph.ExecutionOrder();
            executionOrder.Should().BeEquivalentTo(zeroNode, incrementNode, incrementNode2);
        }

        [Fact]
        public void WhenCircularDependency_CannotConnect()
        {
            var graph = new Graph();
            var incrementNode = new AddOneBehaviour().ToNode();
            var incrementNode2 = new AddOneBehaviour().ToNode();

            graph.AddNode(incrementNode);
            graph.AddNode(incrementNode2);

            graph.Connect(incrementNode, "Value", incrementNode2, "Value");
            graph.Invoking(g => g.Connect(incrementNode2, "Value", incrementNode, "Value"))
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Contain("Circular dependency");
        }

        [Fact]
        public void WhenComplexCircularDependency_CannotConnect()
        {
            var graph = new Graph();
            var incrementNode = new AddOneBehaviour().ToNode();
            var incrementNode2 = new AddOneBehaviour().ToNode();
            var incrementNode3 = new AddOneBehaviour().ToNode();
            var incrementNode4 = new AddOneBehaviour().ToNode();

            graph.AddNode(new ZeroBehaviour().ToNode());
            graph.AddNode(incrementNode);
            graph.AddNode(new ZeroBehaviour().ToNode());
            graph.AddNode(incrementNode2);
            graph.AddNode(new ZeroBehaviour().ToNode());
            graph.AddNode(incrementNode3);
            graph.AddNode(new ZeroBehaviour().ToNode());
            graph.AddNode(incrementNode4);
            graph.AddNode(new ZeroBehaviour().ToNode());

            graph.Connect(incrementNode, "Value", incrementNode2, "Value");
            graph.Connect(incrementNode2, "Value", incrementNode3, "Value");
            graph.Connect(incrementNode3, "Value", incrementNode4, "Value");
            graph.Invoking(g => g.Connect(incrementNode4, "Value", incrementNode, "Value"))
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Contain("Circular dependency");
        }
    }
}