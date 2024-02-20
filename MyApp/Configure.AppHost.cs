using Funq;
using MyApp.ServiceInterface;
using ServiceStack.NativeTypes.TypeScript;

[assembly: HostingStartup(typeof(MyApp.AppHost))]

namespace MyApp;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) =>
        {
            // Configure ASP.NET Core IOC Dependencies
        });

    public AppHost() : base("MyApp", typeof(MyServices).Assembly) { }

    // Configure your AppHost with the necessary configuration and dependencies your App needs
    public override void Configure(Container container)
    {
        TypeScriptGenerator.InsertTsNoCheck = true;

        SetConfig(new HostConfig {
            IgnorePathInfoPrefixes = { "/appsettings", "/_framework" },
        });
    }
}
