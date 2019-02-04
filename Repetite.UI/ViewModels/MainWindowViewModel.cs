using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Avalonia.Input;

namespace Repetite.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _behaviourStore = new BehaviourStore();

            Graph = new GraphViewModel(new Graph());
            BehaviourList = new BehaviourListViewModel();
            DoThing = ReactiveCommand.Create(() => Console.WriteLine("HI"));
        }

        private void RunDoThing(string name)
        {
            Console.WriteLine(name);
        }

        private BehaviourStore _behaviourStore;

        internal GraphViewModel Graph { get; }
        internal BehaviourListViewModel BehaviourList { get; }
        public ReactiveCommand<Unit, Unit> DoThing { get; }

        internal void AddBehaviourToGraph(string behaviourId)
        {
            Graph.AddBehaviour(_behaviourStore.Get(behaviourId));
        }
    }
}
