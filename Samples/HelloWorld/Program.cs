using EdgeSharp.Blazor;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;

namespace HelloWorld
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var appBuilder = EdgeSharpBlazorAppBuilder.CreateDefault(args);
            appBuilder.Services
                .AddMudServices()
                .AddLogging();

            // register root component
            appBuilder.RootComponents.Add<App>("app");

            appBuilder.Config.WindowOptions.Borderless = true;

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