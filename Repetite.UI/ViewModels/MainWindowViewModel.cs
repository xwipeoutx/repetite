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
            BehaviourList = new BehaviourListViewModel();
            DoThing = ReactiveCommand.Create(() => Console.WriteLine("HI"));
        }

        private void RunDoThing(string name)
        {
            Console.WriteLine(name);
        }

        internal BehaviourListViewModel BehaviourList { get; }
        public ReactiveCommand<Unit, Unit> DoThing { get; }
    }
}
