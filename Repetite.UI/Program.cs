using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using Repetite.UI.ViewModels;
using Repetite.UI.Views;

namespace Repetite.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = BuildAvaloniaApp();

            app.Start<MainWindow>(() => new MainWindowViewModel());
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .LogToDebug();
    }
}
