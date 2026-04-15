using System;

namespace DocumentGenerator.Core;

public sealed class AppConfiguration
{
    private static readonly Lazy<AppConfiguration> _instance = 
        new Lazy<AppConfiguration>(() => new AppConfiguration());

    public static AppConfiguration Instance => _instance.Value;

    public string OutputDirectory {get; set;} = "C:\\DocumenteGenerate";
    public string DefaultFormat {get; set;} = "HTML";
    public string DefaultAuthor {get; set;} = "System Adminsitrator";

    private AppConfiguration()
    {
        Console.WriteLine("[AppConfiguration] Se incarca setarile din sistem...");
    }
}