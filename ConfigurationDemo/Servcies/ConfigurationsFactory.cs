using ConfigurationDemo.Configurations;
using ConfigurationDemo.Interfaces;
using System.Text.Json;

namespace ConfigurationDemo.Servcies;

public class ConfigurationsFactory : IConfigurationsFactory
{
    private readonly ILogger<ConfigurationsFactory> _logger;
    private readonly ConfigurationsFactorySettings _settings;
    private readonly IConfiguration _configuration;
    private readonly IEnumerable<string> _paths;

    public ConfigurationsFactory(
        ILogger<ConfigurationsFactory> logger,
        ConfigurationsFactorySettings settings,
        IConfiguration configuration)
    {
        _logger = logger;
        _settings = settings;
        _configuration = configuration;
        _paths = GetAllPaths(_settings.SectionRootKey);
    }

    public TSettings? Get<TSettings>()
    {
        foreach (var path in _paths)
        {
            try
            {
                var settings = _configuration.GetSection(path).Get(typeof(TSettings));
                if (ValidateSettings(settings)) return (TSettings)settings;
            }
            catch (Exception)
            {
            }
          
        }

        return default;
    }

    private bool ValidateSettings(object? settings)
    {
        if(settings is null) return false;

        var type = settings.GetType();
        foreach (var propertyInfo  in type.GetProperties())
        {
            if (propertyInfo.GetValue(settings) is null) return false;
        }
        return true;
    }

    //public TConfiguration? GetConfiguration<TConfiguration>()
    //{
    //    foreach (var path in _paths)
    //    {
    //        var key = _paths.FirstOrDefault(path =>
    //        {

    //        });
    //    }
    //    throw new NotImplementedException();
    //}

    private IEnumerable<string> GetAllPaths(string sectionKey)
    {
        var rootSection = _configuration.GetSection(sectionKey);
        return GetSectionPaths(rootSection);
    }

    private IEnumerable<string> GetSectionPaths(IConfigurationSection section)
    {
        var children = section.GetChildren();
        if (children.Any() is false) return new List<string> { section.Path };

        var paths = new List<string>() { section.Path };
        foreach (var child in children)
        {
            var childPaths = GetSectionPaths(child);
            paths.AddRange(childPaths);
        }
        return paths;
    }
}

//private IEnumerable<string> GetSectionPaths(IConfigurationSection section)
//{
//    var children = section.GetChildren();
//    if (children.Any() is false) return new List<string> { section.Path };

//    var paths = new List<string>();
//    foreach (var child in children)
//    {
//        var childPaths = GetSectionPaths(child);
//        paths.AddRange(childPaths);
//    }
//    return paths;
//}
//public TConfiguration? GetConfiguration<TConfiguration>(string key)
//{
//    var rootSection = _config.GetSection(key);
//    var children = rootSection.GetChildren();
//    _logger.LogInformation($"{JsonSerializer.Serialize(rootSection)}, childrens: {children.Count()}");
//    foreach (var section in children)
//    {
//        var sectionChildren = section.GetChildren();
//        _logger.LogInformation($"{JsonSerializer.Serialize(section)}, childrens: {sectionChildren.Count()}");
//    }
//    rootSection.GetChildren();
//    return _config.GetSection(key).Get<TConfiguration>();
//}
//public TConfiguration? GetConfiguration<TConfiguration>(TConfiguration configuration)
//{
//    _config.Bind(configuration);
//    return configuration;
//}

//public object? GetConfiguration(object configuration)
//{
//    _config.Bind(configuration);
//    return configuration;
//}