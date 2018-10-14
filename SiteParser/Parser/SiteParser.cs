using SiteParser.Handlers;
using SiteParser.PipelineBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Parser
{
    /// <summary>
    /// Реализация интерфейса <see cref="ISiteParser"/>
    /// </summary>
    public class SiteParser : ISiteParser
    {
        readonly IPipelineProvider _provider;

        /// <summary>
        /// Инициализирует экземпляр <see cref="SiteParser"/>.
        /// </summary>
        /// <param name="provider">Провайдер конвейра обработки сайта.</param>
        public SiteParser(IPipelineProvider provider)
        {
            _provider = provider;
        }

        /// <inheritdoc/>
        public IHandler PipelineHandler { get ; set; }

        /// <inheritdoc/>
        public async Task<List<Uri>> ParseAsync(Uri startUri)
        {
            startUri = startUri ?? throw new ArgumentNullException("Не задан стартовый Uri");
            if (PipelineHandler == null && _provider == null)
            {
                throw new ApplicationException("Не задан конвейер разбора сайтов");
            }

            if (PipelineHandler == null)
            {
                PipelineHandler = _provider.GetPipeline();
            }
            // значение ключа разделяет списки ссылок на две группы: 
            //       true - надо обрабатывать, false - не надо обрабатывать.
            var currentProcessingUri = new Dictionary<bool, List<Uri>>
            {
                { true,  new List<Uri> { startUri } },
                { false, new List<Uri>() }
            };

            ParseContext context = new ParseContext();
            do
            {
                foreach (Uri currentUris in currentProcessingUri[true])
                {
                    context.CurrentUrl = currentUris;
                    try
                    {
                        await PipelineHandler.HandleAsync(context);
                    }
                    catch (Exception e)
                    {
                        context.Exceptions.Add(e);
                        break;
                    }
                }
                // перекладываем обработанные ссылки в ключ обработанных
                currentProcessingUri[false].AddRange(currentProcessingUri[true]);

                // ставим на обработку ссылки, выделенные парсерами для обработки и 
                // отфильтровывать те что уже были ранее обработанны.
                var newUri = context
                    .Links[true]
                    .Where(u => currentProcessingUri[false].All(p => p.AbsoluteUri != u.AbsoluteUri)); 
                var toProcess = newUri.Where(u=> u.Authority == startUri.Authority);

                var toProcessed = newUri.Except(toProcess);

                currentProcessingUri[true] = toProcess.Distinct().ToList();

                currentProcessingUri[false].AddRange(context.Links[false].Distinct());
                currentProcessingUri[false].AddRange(toProcessed.Distinct());
                context.Reset();
            }
            while (currentProcessingUri[true].Count > 0);

            return currentProcessingUri[false].Distinct().ToList();
        }
    }
}
