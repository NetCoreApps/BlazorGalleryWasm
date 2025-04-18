﻿[assembly: HostingStartup(typeof(MyApp.ConfigureCors))]

namespace MyApp;

public class ConfigureCors : IHostingStartup
{
    public static string[] Origins = [
        "http://localhost:5000", "https://localhost:5001", "https://localhost:5002", "http://localhost:8080",
        "https://localhost:5173", "http://localhost:5173",
        "https://docs.servicestack.net","https://servicestack.net","https://account.servicestack.net",
        "https://razor-ssg.web-templates.io", "https://razor-press.web-templates.io",
        "https://press-vue.servicestack.net", "https://press-react.servicestack.net",
        "https://vue-spa.web-templates.io", "https://react-spa.web-templates.io",
    ];
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services =>
        {
            services.AddCors(options => {
                options.AddDefaultPolicy(policy => {
                    policy.WithOrigins(Origins)
                    .AllowCredentials()
                    .WithHeaders(["Content-Type", "Allow", "Authorization"])
                    .SetPreflightMaxAge(TimeSpan.FromHours(1));
                });
            });
            services.AddTransient<IStartupFilter, StartupFilter>();
        });

    public class StartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) => app =>
        {
            app.UseCors();
            next(app);
        };
    }        
}
