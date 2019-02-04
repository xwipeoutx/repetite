using Avalonia.Controls;

namespace Repetite.UI.Views
{
    class UserControl<T> : UserControl
    {
        protected T DataContext => (T)base.DataContext;
    }
}
