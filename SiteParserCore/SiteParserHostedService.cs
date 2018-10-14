using Microsoft.Extensions.Hosting;
using SiteParser.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiteParserCore
{
    public class SiteParserHostedService : IHostedService
    {
        readonly ISiteParser _siteParser;
        readonly IApplicationLifetime _applicationLifetime;
        public SiteParserHostedService( ISiteParser siteParser, IApplicationLifetime applicationLifetime)
        {
            _siteParser = siteParser;
            _applicationLifetime = applicationLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.Write("Начальный урл:");
            string startUrl = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(startUrl))
            {
                _applicationLifetime.StopApplication();
            }

            List<Uri> links = await _siteParser.ParseAsync(new Uri(startUrl));
            foreach (Uri link in links)
            {
                Console.WriteLine(link.AbsoluteUri);
            }
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.Read();

            _applicationLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
