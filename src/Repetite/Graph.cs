using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repetite
{
    public class Graph
    {
        private List<Node> _nodes = new List<Node>();
        public IReadOnlyList<Node> Nodes => _nodes;

        private List<Binding> _bindings = new List<Binding>();
        public IReadOnlyList<Binding> Bindings => _bindings;

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

            var numBindingsRemoved = _bindings.RemoveAll(e => e.TargetNode == targetNode && e.TargetInput.Name == targetKey);
            
            var edge = new Binding(sourceNode, output, targetNode, input);
            _bindings.Add(edge);

            this.ExecutionOrder().ToList(); // just checkin....
        }
        
        public void Disconnect(Node targetNode, string targetKey)
        {
            var numBindingsRemoved = _bindings.RemoveAll(e => e.TargetNode == targetNode && e.TargetInput.Name == targetKey);
        }
        
        public IEnumerable<Node> ExecutionOrder()
        {
            var nodesInOrder = new LinkedList<Node>();
            var numChecked = 0;
            var nodeCount = Nodes.Count;
            var notReady = new Queue<Node>(Nodes);

            while (notReady.Any())
            {
                numChecked++;
                var node = notReady.Dequeue();

                var isReady = !Bindings.Where(e => e.TargetNode == node).Any(e => notReady.Contains(e.SourceNode));

                if (isReady)
                {
                    numChecked = 0;
                    nodeCount--;
                    nodesInOrder.AddLast(node);
                }
                else
                {
                    notReady.Enqueue(node);
                    if (numChecked > nodeCount)
                    {
                        throw new InvalidOperationException("Circular dependency detected!");
                    }
                }
            }

            return nodesInOrder;
        }
    }
}