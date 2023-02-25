namespace ConfigurationDemo.Interfaces;

public interface IConfigurationsFactory
{
    TSettings? Get<TSettings>();
    //string GetSettingsSectionContent(string sectionName);
    //TConfiguration? GetConfiguration<TConfiguration>();
    //object? GetConfiguration(object configuration);
    //TConfigurations? GetConfiguration<TConfigurations>(string key);
    //IEnumerable<string> GetAllPaths(string section);
}
