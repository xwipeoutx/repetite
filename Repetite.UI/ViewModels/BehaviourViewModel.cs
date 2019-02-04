using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace Repetite.UI.ViewModels
{

    class BehaviourViewModel : ViewModelBase
    {
        public BehaviourViewModel(IBehaviour behaviour)
        {
            Behaviour = behaviour;
        }

        public IBehaviour Behaviour { get; }
    }
}
