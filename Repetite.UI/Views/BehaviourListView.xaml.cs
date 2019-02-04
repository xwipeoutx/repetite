﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Repetite.UI.ViewModels;

namespace Repetite.UI.Views
{
    class BehaviourListView : UserControl<BehaviourListViewModel>
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
