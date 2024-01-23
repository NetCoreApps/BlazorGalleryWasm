using ServiceStack;
using MyApp.ServiceModel;
using System;

namespace MyApp.ServiceInterface;

public class MyServices : Service
{
    public static string AssertName(string Name) => Name.IsNullOrEmpty() 
        ? throw new ArgumentNullException(nameof(Name))
        : Name;

    public object Any(Hello request) =>
        new HelloResponse { Result = $"Hello, {AssertName(request.Name)}!" };

    public object Any(HelloSecure request) => 
        new HelloResponse { Result = $"Hello, {AssertName(request.Name)}!" };

    public object Any(GitHubCode request)
    {
        var path = AssertName(request.Path);
        var url = GalleryCodeUrl(path);
        return url.GetStreamFromUrl();
    }
    
    public static string GalleryCodeBaseUrl { get; set; } = "https://github.com/NetCoreApps/BlazorGalleryWasm/tree/main/Gallery.Wasm.Client/Pages";
    public static string GalleryCodeUrl(string path) => $"{GalleryCodeBaseUrl}/{path}";
}
