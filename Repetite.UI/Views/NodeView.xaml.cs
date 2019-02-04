using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Repetite.UI.ViewModels;

namespace Repetite.UI.Views
{
    class NodeView : UserControl<NodeViewModel>
    {
        public NodeView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
