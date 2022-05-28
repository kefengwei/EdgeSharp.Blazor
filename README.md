# Build native, cross-platform desktop apps with Blazor

Photino is a lightweight open-source framework for building native,  
cross-platform desktop applications with Web UI technology.

Photino.Blazor builds on <span>Photino.</span>NET to add Blazor capabilities.

Photino uses the OSs built-in WebKit-based browser control for Windows, macOS and Linux.
Photino is the lightest cross-platform framework. Compared to Electron, a Photino app is up to 110 times smaller! And it uses far less system memory too!

## Usage
```C#
var appBuilder = PhotinoBlazorAppBuilder.CreateDefault(args);

appBuilder.Services
	.AddLogging();

// register root component and selector
appBuilder.RootComponents.Add<App>("app");

var app = appBuilder.Build();

// customize window
app.MainWindow
    .SetIcon(favicon.ico)
	.SetTitle("Photino Blazor Sample");

AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
{
	app.MainWindow.OpenAlertWindow("Fatal exception", error.ExceptionObject.ToString());
};

app.Run();
```


## Photino.Blazor

This project represents Blazor functionality on top of <span>Photino.</span>NET (the .NET 5 wrapper for the Photino.Native project), which makes it available for all operating systems (Windows, macOS, Linux).
This library is used for the Blazor sample projects: 
https://github.com/tryphotino/photino.Samples

If you made changes to the Photino.Native or <span>Photino.</span>NET projects, or added new features to them, you will likely need this repo to expose the new functionality to the Photino.Blazor wrapper.
In all other cases, you can just grab the nuget package for your projects:
https://www.nuget.org/packages/Photino.Blazor

## How to build this repo

If you want to build this library itself, you will need:
 * Windows 10, Mac 10.15+, or Linux (Tested with Ubuntu 18.04+)
 * Make sure the Photino.Native Nuget package is added and up to date.
 * If you're on Windows Subsystem for Linux (WSL), then as well as the above, you will need a local X server: 
  https://tryphotino.kavadocs.com/Running-Photino-in-WSL
