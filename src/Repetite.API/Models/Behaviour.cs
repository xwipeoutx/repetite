using System;
using Microsoft.CodeAnalysis;

namespace Repetite.API.Models
{
    public class Node
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public string BehaviourId { get; set; }
    }
    
    public class Behaviour
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public Output[] Outputs { get; set; }
        public Input[] Inputs { get; set; }
    }

    public class Output
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
    
    public class Input
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}