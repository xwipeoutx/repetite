namespace Repetite
{
    public static class NodeExtensions
    {
        public static Node ToNode(this IBehaviour behaviour)
        {
            return new Node(behaviour);
        }
    }
}