using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;
using SiteParser.Parser;
using SiteParser.Handlers;
using SiteParser.PipelineBuilder;
using System.Threading.Tasks;

namespace SiteParserCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddTransient<IConfigurationProvider, XmlConfigurationProvider>();
                    services.AddTransient<IPipelineProvider, PipelineProvider>();
                    services.AddTransient<IHandlerFactory, HandlerFactory>();
                    services.AddTransient<ISiteParser, SiteParser.Parser.SiteParser>();
                    services.AddSingleton<IHostedService, SiteParserHostedService>();
                    services.AddSingleton<IServiceProvider>(services.BuildServiceProvider());
                })
                .UseConsoleLifetime();
            await host.RunConsoleAsync();
        }
    }
}
