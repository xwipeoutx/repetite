using ReactiveUI;
using System.Collections.ObjectModel;

namespace Repetite.UI.ViewModels
{
     class BehaviourListViewModel : ViewModelBase
    {
        public BehaviourListViewModel()
        {
            Behaviours = new Behaviours();
        }

        public Behaviours Behaviours { get; }
    }

    class Behaviours : ObservableCollection<IBehaviour>
    {
        public Behaviours()
        {
            foreach (var behaviour in new BehaviourStore().All)
            {
                Add(behaviour);
            }
        }
    }
}
