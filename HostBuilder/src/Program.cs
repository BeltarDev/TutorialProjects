using AppLifetime.example;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(configHost =>
    {
        configHost.SetBasePath(Directory.GetCurrentDirectory());
        configHost.AddJsonFile("hostsettings.json", optional: true);
        configHost.AddEnvironmentVariables(prefix: "PREFIX_");
        configHost.AddCommandLine(args);
    })
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<ExampleHostedService>();
    })
    .Build();

await host.RunAsync();
