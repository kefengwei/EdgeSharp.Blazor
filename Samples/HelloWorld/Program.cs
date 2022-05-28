using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using EdgeSharp.Blazor;
using MudBlazor.Services;
namespace HelloWorld
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var appBuilder = EdgeSharpBlazorAppBuilder.CreateDefault(args);
            appBuilder.Services
                .AddMudServices()
                .AddLogging();

            // register root component
            appBuilder.RootComponents.Add<App>("app");


            appBuilder.Config.WindowOptions.Borderless = true;
            appBuilder.Config.WindowOptions.HighDpiMode = EdgeSharp.Core.Configuration.HighDpiMode.PER_MONITOR_AWARE2;

            var app = appBuilder.Build();

            // customize window
            // app.MainWindow
            //     .SetIconFile("favicon.ico")
            //     .SetTitle("Photino Hello World");

            AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
            {
                // app.MainWindow.OpenAlertWindow("Fatal exception", error.ExceptionObject.ToString());
            };

            app.Run(args);
        }

    }
}
