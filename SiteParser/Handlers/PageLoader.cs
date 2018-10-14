using SiteParser.PipelineBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Handlers
{
    /// <summary>
    /// Реализация <see cref="IHandler"/> загрузчика HTML страницы.
    /// </summary>
    public class PageLoader : HandlerBase
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="PageLoader"/>.
        /// </summary>
        /// <param name="config"></param>
        public PageLoader(PipelineConfigItem config) : base(config)
        {
        }

        /// <inheritdoc/>
        public override async Task HandleAsync(ParseContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));
            context.CurrentUrl = context.CurrentUrl ?? throw new ArgumentNullException("Не задан адрес контента.");
            using (var client = new HttpClient())
            {
                context.Content = await client.GetStringAsync(context.CurrentUrl);
            }

            if (!string.IsNullOrWhiteSpace(context.Content))
            {
                if (Next != null)
                {
                    await Next.HandleAsync(context);
                }
            }
        }
    }
}
