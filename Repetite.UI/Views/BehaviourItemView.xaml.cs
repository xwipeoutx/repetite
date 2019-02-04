using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Repetite.UI.ViewModels;

namespace Repetite.UI.Views
{

    class BehaviourItemView : UserControl<BehaviourViewModel>
    {
        private Border _DragBoundary;

        public BehaviourItemView()
        {
            this.InitializeComponent();

            _DragBoundary.PointerPressed += DragMe;
        }

        private async void DragMe(object sender, PointerPressedEventArgs e)
        {
            DataObject dragData = new DataObject();
            dragData.Set(DataFormats.Text, DataContext.Behaviour.Id);

            var result = await DragDrop.DoDragDrop(dragData, DragDropEffects.Copy);
            switch (result)
            {
                case DragDropEffects.Copy:
                    break;
                case DragDropEffects.Link:
                    break;
                case DragDropEffects.None:
                    break;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _DragBoundary = this.Find<Border>("DragBoundary");
        }
    }
}
