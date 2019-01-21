using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using Xunit;

namespace Repetite.Tests
{
    public class TerminatorNodeTests
    {
        [Fact]
        public void NoOutputs()
        {
            var nullNode = new TerminatorNode();
            nullNode.Outputs.Length.Should().Be(0);
        }

        [Fact]
        public void SingleInput()
        {
            var nullNode = new TerminatorNode();
            nullNode.Inputs.Length.Should().Be(1);
        }

        [Fact]
        public void HasAnyInput()
        {
            var nullNode = new TerminatorNode();
            var input = nullNode.Inputs.Single();
            input.Type.Should().Be(InputType.Any);
        }
    }

    public class AddIntNodeTests
    {
        [Fact]
        public void HasTwoIntegerInputs()
        {
            var nullNode = new AddIntNode();
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
            var addIntNode = new AddIntNode();
            var output = addIntNode.Outputs.Single();
            output.Type.Should().Be(OutputType.Integer);
            output.Name.Should().Be("Result");
        }

        [Fact]
        public void WhenExecutedWithValues_SumIsOutput()
        {
            var addIntNode = new AddIntNode();

            var num1 = new Random().Next(1, 100);
            var num2 = new Random().Next(1, 100);
            var values = new NodeValueBag() {{"First", num1}, {"Second", num2}};
            var results = addIntNode.Execute(values);
            results.GetInt("Result").Should().Be(num1 + num2);
        }
    }

    public class OutputTests
    {
        [Fact]
        public void GivenAnyInput_CanReceiveAllOutputs()
        {
            var input = new Input(InputType.Any, "Some String");

            var outputTypes = Enum.GetValues(typeof(OutputType)).Cast<OutputType>();
            foreach (var outputType in outputTypes)
            {
                input.CanReceive(outputType).Should().BeTrue();
            }
        }
    }

    public class GeneratorNodeTests
    {
        [Fact]
        public void WhenGeneratorNode_OutputFromConstructor()
        {
            var generatorNode = new GeneratorNode(OutputType.Integer);

            var output = generatorNode.Outputs.Single();
            output.Name.Should().Be("Value");
            output.Type.Should().Be(OutputType.Integer);
        }

        [Fact]
        public void GivenGeneratorNode_WhenExecutedWithValue_ValueIsOutput()
        {
            var generatorNode = new GeneratorNode(OutputType.Integer);

            var givenValue = new Random().Next(1, 1000000);
            var values = new NodeValueBag() {{"Value", givenValue}};
            var results = generatorNode.Execute(values);
            results.GetInt("Value").Should().Be(givenValue);
        }
    }

    public class NodeValueBagTests
    {
        [Fact]
        public void GivenNoNodeValue_KeyNotFoundIsThrown()
        {
            var nodeValues = new NodeValueBag();
            nodeValues.Invoking(bag => bag.GetInt("Not there"))
                .Should().Throw<KeyNotFoundException>()
                .And.Message.Should().Contain("Not there");
        }

        [Fact]
        public void GivenNodeValue_ItIsFound()
        {
            var nodeValues = new NodeValueBag();
            var givenValue = new Random().Next(1, 1000000);
            nodeValues.Add("Value", givenValue);
            nodeValues.GetInt("Value").Should().Be(givenValue);
        }
    }

    public class NodeValueBag : IEnumerable
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public void Add(string value, int givenValue)
        {
            _values[value] = givenValue;
        }

        public int GetInt(string value)
        {
            return (int) _values[value];
        }

        public IEnumerator GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }

    public interface INode
    {
        Input[] Inputs { get; }
        Output[] Outputs { get; }
    }

    public class GeneratorNode : INode
    {
        public OutputType OutputType { get; }

        public GeneratorNode(OutputType outputType)
        {
            OutputType = outputType;
        }

        public Input[] Inputs => Array.Empty<Input>();

        public Output[] Outputs => new[]
        {
            new Output(OutputType.Integer, "Value")
        };

        public NodeValueBag Execute(NodeValueBag inputs)
        {
            var value = inputs.GetInt("Value");
            return new NodeValueBag {{"Value", value}};
        }
    }

    public class AddIntNode : INode
    {
        public Output[] Outputs => new[]
        {
            new Output(OutputType.Integer, "Result")
        };

        public Input[] Inputs => new[]
        {
            new Input(InputType.Integer, "First"),
            new Input(InputType.Integer, "Second")
        };

        public NodeValueBag Execute(NodeValueBag values)
        {
            var num1 = values.GetInt("First");
            var num2 = values.GetInt("Second");
            return new NodeValueBag {{"Result", num1 + num2}};
        }
    }

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
            var node = new GeneratorNode(OutputType.Integer);

            graph.AddNode(node);
            graph.Nodes.Single().Should().Be(node);
        }
    }

    public class NodeInstanceTests
    {
        [Fact]
        public void NodeIsSet()
        {
            var boundNode = new AddIntNode();
            var nodeInstance = new NodeInstance(boundNode);

            nodeInstance.Node.Should().Be(boundNode);
        }
        
        [Fact]
        public void CanSetInput()
        {
            var nodeInstance = new NodeInstance(new AddIntNode());

            nodeInstance.SetInput("First", 3);
            // TODO: Consider having the graph itself have node value bags with input properties,
            // and manage the edges and things
        }
    }

    public class NodeInstance
    {
        public INode Node { get; }

        public NodeInstance(INode node)
        {
            Node = node;
        }
        
        public void SetInput<T>(string key, T value)
        {
            var input = Node.Inputs.Single(i => i.Name == key);
            if (!input.CanReceive(value))
            {
                throw new ArgumentException($"Cannot assign a {typeof(T)} to a {input.Type} field");
            }
        }
    }

    public class Graph
    {
        private List<INode> _nodes = new List<INode>();
        public IReadOnlyList<INode> Nodes => _nodes;

        public void AddNode(INode node)
        {
            _nodes.Add(node);
        }
    }

    public class TerminatorNode : INode
    {
        public Output[] Outputs => new Output[] { };
        public Input[] Inputs => new[] {new Input(InputType.Any, "Value")};
    };

    public enum OutputType
    {
        Integer
    }

    public class Output
    {
        public OutputType Type { get; }
        public string Name { get; }

        public Output(OutputType type, string name)
        {
            Type = type;
            Name = name;
        }
    }

    public enum InputType
    {
        Any,
        Integer
    }

    public class Input
    {
        public InputType Type { get; }
        public string Name { get; }

        public Input(InputType type, string name)
        {
            Type = type;
            Name = name;
        }

        public bool CanReceive(OutputType outputType)
        {
            switch (Type)
            {
                case InputType.Any:
                    return true;
                case InputType.Integer:
                    return outputType == OutputType.Integer;
                default:
                    return false;
            }
        }

        public bool CanReceive(object value)
        {
            switch (Type)
            {
                case InputType.Any:
                    return true;
                case InputType.Integer:
                    return value is int;
                default:
                    return false;
            }
        }
    }
};