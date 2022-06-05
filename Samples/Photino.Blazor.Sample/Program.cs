using Microsoft.Extensions.DependencyInjection;
using System;

namespace EdgeSharp.Blazor.Sample
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var appBuilder = EdgeSharpBlazorAppBuilder.CreateDefault(args);

            appBuilder.Services
                .AddLogging();

            // register root component and selector
            appBuilder.RootComponents.Add<App>("app");

            var app = appBuilder.Build();

            AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
            {
                //app.MainWindow.OpenAlertWindow("Fatal exception", error.ExceptionObject.ToString());
            };

            app.Run(args);
        }
    }
}