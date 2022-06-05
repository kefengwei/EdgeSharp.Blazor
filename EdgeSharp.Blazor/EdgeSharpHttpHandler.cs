using EdgeSharp.Browser;
using EdgeSharp.Core;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace EdgeSharp.Blazor
{
    public class EdgeSharpHttpHandler : DelegatingHandler
    {
        private EdgeSharpWindow _window;

        //use this constructor if a handler is registered in DI to inject dependencies
        public EdgeSharpHttpHandler(IBrowserWindow window)
        {
            _window =  window as EdgeSharpWindow;
            InnerHandler = new HttpClientHandler();
        }

        ////Use this constructor if a handler is created manually.
        ////Otherwise, use DelegatingHandler.InnerHandler public property to set the next handler.
        //public EdgeSharpHttpHandler(EdgeSharpWindow window, HttpMessageHandler innerHandler)
        //{
        //    _window = window;

        //    //the last (inner) handler in the pipeline should be a "real" handler.
        //    //To make a HTTP request, create a HttpClientHandler instance.
        //    InnerHandler = innerHandler ?? new HttpClientHandler();
        //}

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Stream content = _window.HandleWebRequest(null, null, request.RequestUri.AbsoluteUri, out string contentType);
            if (content != null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(content);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                return response;
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}