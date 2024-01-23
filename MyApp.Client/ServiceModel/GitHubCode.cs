using ServiceStack;

namespace MyApp.ServiceModel;

public class GitHubCode : IGet, IReturn<string>
{
    public string? Path { get; set; }
}