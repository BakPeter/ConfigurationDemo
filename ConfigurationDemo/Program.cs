using ConfigurationDemo;
using ConfigurationDemo.Configurations;
using ConfigurationDemo.Interfaces;
using ConfigurationDemo.Servcies;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, configurations) =>
    {
        configurations.AddJsonFile("worker-settings.json");
    })
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton(new ConfigurationsFactorySettings { SectionRootKey = "Sections" });
        services.AddSingleton<IConfigurationsFactory, ConfigurationsFactory>();
        
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();