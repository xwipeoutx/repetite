using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Repetite.UI.Views
{
    class BehaviourListView : UserControl
    {
        public BehaviourListView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
