using EdgeSharp.Core;
using EdgeSharp.Core.Defaults;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

namespace EdgeSharp.Blazor
{
    public class BlazorEdgeSharpConfig : Configuration
    {
        public BlazorEdgeSharpConfig()
        {
            StartUrl = EdgeSharpWebViewManager.AppBaseUri;
        }
    }

    public class EdgeSharpBlazorAppBuilder
    {
        private AppBuilder appBuilder;

        internal EdgeSharpBlazorAppBuilder()
        {
            RootComponents = new RootComponentList();
            Services = new ServiceCollection();
            Config = new BlazorEdgeSharpConfig();

            appBuilder = AppBuilder.Create()
                .UseWindow<EdgeSharpWindow>()
                .UseApp<BlazorEdgeSharpApp>()
                .UseConfig<BlazorEdgeSharpConfig>(Config)
                .UseServices(Services);
        }

        public static EdgeSharpBlazorAppBuilder CreateDefault(string[] args = default)
        {
            // We don't use the args for anything right now, but we want to accept them
            // here so that it shows up this way in the project templates.
            // var jsRuntime = DefaultWebAssemblyJSRuntime.Instance;
            var builder = new EdgeSharpBlazorAppBuilder();
            builder.Services
                .AddTransient<EdgeSharpHttpHandler>()
                .AddScoped(sp =>
                {
                    var handler = sp.GetService<EdgeSharpHttpHandler>();
                    return new HttpClient(handler) { BaseAddress = new Uri(EdgeSharpWebViewManager.AppBaseUri) };
                })
                .AddSingleton<EdgeSharpBlazorApp>()
                .AddBlazorWebView();

            //builder.Services.AddHttpClient("", client =>
            //{
            //    client.BaseAddress = new Uri(EdgeSharpWebViewManager.AppBaseUri);
            //}).AddHttpMessageHandler<EdgeSharpHttpHandler>();

            // Right now we don't have conventions or behaviors that are specific to this method
            // however, making this the default for the template allows us to add things like that
            // in the future, while giving `new BlazorDesktopHostBuilder` as an opt-out of opinionated
            // settings.
            return builder;
        }

        public RootComponentList RootComponents { get; }

        public IServiceCollection Services { get; }

        public BlazorEdgeSharpConfig Config { get; set; }

        public AppBuilder Build(Action<IServiceProvider> serviceProviderOptions = null)
        {
            appBuilder.Build();
            var sp = ServiceLocator.Current.Provider;

            var app = sp.GetService<EdgeSharpBlazorApp>();

            serviceProviderOptions?.Invoke(sp);

            app.Initialize(sp, RootComponents);
            return appBuilder;
        }
    }

    public class RootComponentList : IEnumerable<(Type, string)>
    {
        private List<(Type componentType, string domElementSelector)> components = new List<(Type componentType, string domElementSelector)>();

        public void Add<TComponent>(string selector) where TComponent : IComponent
        {
            components.Add((typeof(TComponent), selector));
        }

        public IEnumerator<(Type, string)> GetEnumerator()
        {
            return components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return components.GetEnumerator();
        }
    }
}