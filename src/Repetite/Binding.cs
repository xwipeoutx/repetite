using System;
using System.Collections.Generic;

namespace Repetite
{
    public class Binding
    {
        public Node SourceNode { get; }
        public Output SourceOutput { get; }
        public Node TargetNode { get; }
        public Input TargetInput { get; }
        
        public Binding(Node sourceNode, Output sourceOutput, Node targetNode, Input targetInput)
        {
            SourceNode = sourceNode;
            SourceOutput = sourceOutput;
            TargetNode = targetNode;
            TargetInput = targetInput;
        }
    }
}