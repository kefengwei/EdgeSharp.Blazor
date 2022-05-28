using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeSharp;
using EdgeSharp.Browser;
using Microsoft.Web.WebView2.Core;

namespace EdgeSharp.Blazor
{

    public class EdgeSharpBlazorApp
    {
        /// <summary>
        /// Gets configuration for the service provider.
        /// </summary>
        public IServiceProvider Services { get; private set; }

        private BlazorWindowRootComponents _rootComponents { get; set; }

        /// <summary>
        /// Gets configuration for the root components in the window.
        /// </summary>
        public RootComponentList RootComponents { get; private set; }

        internal void Initialize(IServiceProvider services, RootComponentList rootComponents)
        {

            Services = services;
            RootComponents = rootComponents;
        }
    }
}
