using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Repetite.UI.Views
{
    public class GraphView : UserControl
    {
        public GraphView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
