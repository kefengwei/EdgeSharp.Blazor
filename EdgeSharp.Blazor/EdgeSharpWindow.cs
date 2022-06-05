using EdgeSharp.Browser;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Web.WebView2.Core;
using System;
using System.IO;

namespace EdgeSharp.Blazor
{
    public class EdgeSharpWindow : BrowserWindow
    {
        private readonly IServiceProvider services;

        private EdgeSharpWebViewManager WindowManager { get; set; }

        public BlazorWindowRootComponents RootComponents { get; private set; }
        private RootComponentList rootComponents;

        private EdgeSharpBlazorApp _app { get; set; }

        public EdgeSharpWindow(IServiceProvider services) : base()
        {
            this.services = services;
            _app = services.GetService<EdgeSharpBlazorApp>();
            rootComponents = _app.RootComponents;
        }

        protected override void OnInitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            base.OnInitializationCompleted(sender, e);
            CoreWebView2.WebResourceRequested += CoreWebView2_WebResourceRequested;
            CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.external = { sendMessage: function(message) { window.chrome.webview.postMessage(message); }, receiveMessage: function(callback) { window.chrome.webview.addEventListener(\'message\', function(e) { callback(e.data); }); } };");
            // We assume the host page is always in the root of the content directory, because it's
            // unclear there's any other use case. We can add more options later if so.
            string hostPage = "index.html";
            var contentRootDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");
            var fileProvider = new PhysicalFileProvider(contentRootDir);

            var dispatcher = new EdgeSharpDispatcher();
            var jsComponents = new JSComponentConfigurationStore();
            WindowManager = new EdgeSharpWebViewManager(this, services, dispatcher, new Uri(EdgeSharpWebViewManager.AppBaseUri), fileProvider, jsComponents, hostPage);
            RootComponents = new BlazorWindowRootComponents(WindowManager, jsComponents);
            foreach (var component in rootComponents)
            {
                RootComponents.Add(component.Item1, component.Item2);
            }
        }

        private void CoreWebView2_WebResourceRequested(object sender, CoreWebView2WebResourceRequestedEventArgs e)
        {
            var uri = new Uri(e.Request.Uri);
            if (uri.Host == "0.0.0.0")
            {
                var contentType = $"Content-Type:{e.ResourceContext.ToString()}";
                var stream = HandleWebRequest(sender, uri.Scheme, e.Request.Uri, out contentType);
                e.Response = CoreWebView2.Environment.CreateWebResourceResponse(stream, 200, "OK", contentType);
            }
        }

        public Stream HandleWebRequest(object sender, string scheme, string url, out string contentType)
                => WindowManager.HandleWebRequest(sender, scheme, url, out contentType!)!;
    }
}