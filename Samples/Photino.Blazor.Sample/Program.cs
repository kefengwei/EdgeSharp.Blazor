using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using EdgeSharp.Blazor;
using EdgeSharp;

namespace EdgeSharp.Blazor.Sample
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
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
