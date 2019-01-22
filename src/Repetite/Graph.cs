using System;
using System.Collections.Generic;

namespace Repetite
{
    public class Graph
    {
        private List<Node> _nodes = new List<Node>();
        public IReadOnlyList<Node> Nodes => _nodes;

        private List<Edge> _edges = new List<Edge>();
        public IReadOnlyList<Edge> Edges => _edges;

        public void AddNode(Node node)
        {
            _nodes.Add(node);
        }

        public void Connect(Node sourceNode, string sourceKey, Node targetNode, string targetKey)
        {
            var output = sourceNode.Output(sourceKey);
            var input = targetNode.Input(targetKey);

            if (input == null || output == null)
            {
                throw new KeyNotFoundException();
            }

            if (!input.CanReceive(output.Type))
            {
                throw new ArgumentException();
            }

            var edge = new Edge(sourceNode, output, targetNode, input);
            _edges.Add(edge);
        }
    }
}