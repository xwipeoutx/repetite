using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Repetite.UI.ViewModels;

namespace Repetite.UI.Views
{
    public class MainWindow : Window
    {
        private TextBlock _dropTarget;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            AddHandler(DragDrop.DragOverEvent, DoDragOver);
            AddHandler(DragDrop.DropEvent, DoDrop);
        }

        private void DoDrop(object sender, DragEventArgs e)
        {
            ((MainWindowViewModel)DataContext).AddBehaviourToGraph(e.Data.GetText());
        }

        private void DoDragOver(object sender, DragEventArgs e)
        {
            _dropTarget.Text = "..." + e.Data.GetText();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _dropTarget = this.Find<TextBlock>("DropTarget");
        }
    }
}
