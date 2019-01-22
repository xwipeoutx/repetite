namespace Repetite
{
    public class Edge
    {
        public Node SourceNode { get; }
        public Output SourceOutput { get; }
        public Node TargetNode { get; }
        public Input TargetInput { get; }

        public Edge(Node sourceNode, Output sourceOutput, Node targetNode, Input targetInput)
        {
            SourceNode = sourceNode;
            SourceOutput = sourceOutput;
            TargetNode = targetNode;
            TargetInput = targetInput;
        }
    }
}