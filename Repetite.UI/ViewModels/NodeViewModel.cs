namespace Repetite.UI.ViewModels
{
    class NodeViewModel : ViewModelBase
    {
        private readonly Node node;

        public string Name => node.Behaviour.Name;

        public Input[] Inputs => node.Behaviour.Inputs;
        public Output[] Outputs => node.Behaviour.Outputs;

        public NodeViewModel(Node node)
        {
            this.node = node;
        }
    }
}
