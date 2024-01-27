using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApp.Client;
using ServiceStack.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

// Use / for local or CDN resources
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

if (builder.HostEnvironment.IsDevelopment())
{
    builder.Services.AddBlazorApiClient(builder.HostEnvironment.BaseAddress);
}
else
{
    var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress;
    builder.Services.AddBlazorApiClient(apiBaseUrl);
}


builder.Services.AddLocalStorage();

await builder.Build().RunAsync();
