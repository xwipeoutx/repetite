using Repetite.Behaviours.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Repetite.UI.ViewModels
{
    class GraphViewModel : ViewModelBase
    {
        private readonly Graph graph;

        public ObservableCollection<NodeViewModel> Nodes { get; }

        public GraphViewModel(Graph graph)
        {
            this.graph = graph;
            graph.AddNode(new Node(new ReadTextFileBehaviour()));
            Nodes = new ObservableCollection<NodeViewModel>(graph.Nodes.Select(n => new NodeViewModel(n)));
        }

        public void AddBehaviour(IBehaviour behaviour)
        {
            var node = behaviour.ToNode();
            graph.AddNode(node);
            Nodes.Add(new NodeViewModel(node));
        }
    }
}
