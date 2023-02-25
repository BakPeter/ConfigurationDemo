using ConfigurationDemo.Interfaces;
using ConfigurationDemo.Types;
using System.Text.Json;

namespace ConfigurationDemo
{
    public class Worker : BackgroundService
    {

        private readonly ILogger<Worker> _logger;
        private readonly IConfigurationsFactory _configurationsFactory;

        public Worker(ILogger<Worker> logger, IConfigurationsFactory configurationsFactory)
        {
            _logger = logger;
            _configurationsFactory = configurationsFactory;

            var settigns = _configurationsFactory.Get<SectionAA>();
            _logger.LogInformation($"{JsonSerializer.Serialize(settigns)}");

            //var sectionContent = _configurationsFactory.GetSettingsSectionContent(_rootSectionKey);
            //_logger.LogInformation(sectionContent);
            //PrintPaths("Sections");

            //var sectionAA_1 = _configurationsFactory.GetConfiguration(new SectionAA());
            //var sectionAA_2 = _configurationsFactory.GetConfiguration<SectionAA>(new SectionAA());
            //var _settings = _configurationsFactory.GetConfiguration<SectionAA>("Sections:SectionA:SectionAA");
            //var settings11 = _configurationsFactory.GetConfiguration<SectionAA>("Sections:SectionB:Param10:0");
            //var settings12 = _configurationsFactory.GetConfiguration<SectionAA>("Sections:SectionB:Param10:1");
            //var settings2 = _configurationsFactory.GetConfiguration<SectionAA>("Sections1111:SectionA:SectionAA");
        }

        //private void PrintPaths(string section)
        //{
        //    var paths = _configurationsFactory.GetAllPaths(section);
        //    foreach (var path in paths)
        //    {
        //        //_logger.LogInformation($"{JsonSerializer.Serialize(path)}");section
        //        Console.WriteLine($"{JsonSerializer.Serialize(path)}");
        //    }
        //}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var sections1 = _configurationsFactory.GetConfiguration<SectionAA>("Sections:SectionA:SectionAA");
            
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}, _settings: {}", DateTimeOffset.Now, JsonSerializer.Serialize(sections1));
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}