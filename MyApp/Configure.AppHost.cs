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

        string[] allowedOrigins = [
            "https://localhost:5001",
            "https://localhost:5002",
            "https://docs.servicestack.net",
            "https://servicestack.net",
            "https://*.servicestack.net",
        ];

        GlobalResponseFilters.Add((req,res,dto) => {
            var origin = req.Headers.Get(HttpHeaders.Origin);
            if (origin != null && allowedOrigins.Any(o => origin.StartsWith(o)))
            {
                res.AddHeader("X-Frame-Options", $"ALLOW-FROM {origin}");
                res.AddHeader("Content-Security-Policy", $"frame-ancestors {origin}");
            }
        });
    }
}
