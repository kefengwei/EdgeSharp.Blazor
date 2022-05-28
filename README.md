# Base on  [EdgeSharp](https://github.com/webview2/EdgeSharp), inspire by [Photino.Blazor](https://github.com/tryphotino/photino.Blazor)

## Usage
```C#
var appBuilder = EdgeSharpBlazorAppBuilder.CreateDefault(args);

appBuilder.Services
	.AddLogging();

// register root component and selector
appBuilder.RootComponents.Add<App>("app");
// custom window
appBuilder.Config.WindowOptions.Borderless = true;

var app = appBuilder.Build();



AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
{
	Debug.WriteLine($"Fatal exception:{error.ExceptionObject.ToString()}");
};

app.Run(args);
```
