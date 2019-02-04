using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace Repetite.UI.ViewModels
{
     class BehaviourListViewModel : ViewModelBase
    {
        public BehaviourListViewModel()
        {
            Behaviours = new ObservableCollection<BehaviourViewModel>();
            foreach (var behaviour in new BehaviourStore().All)
            {
                Behaviours.Add(new BehaviourViewModel(behaviour));
            }
        }

        public ObservableCollection<BehaviourViewModel> Behaviours { get; }
    }
}
