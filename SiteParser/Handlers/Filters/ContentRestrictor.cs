using System;
using System.Linq;
using System.Threading.Tasks;
using SiteParser.PipelineBuilder;

namespace SiteParser.Handlers.Filters
{
    /// <summary>
    /// Реализация обработчика для фильтрации контента по определенным признакам.
    /// </summary>
    public class ContentRestrictor : HandlerBase
    {
        private const string MinSizeParamName = "minSize";
        private const string MaxSizeParamName = "maxSize";
        private const string DomainParamName = "domain";
        /// <summary>
        /// Инициализирует экземпляр <see cref="ContentRestrictor"/>.
        /// </summary>
        /// <param name="config"></param>
        public ContentRestrictor(PipelineConfigItem config) : base(config)
        {
        }

        /// <inheritdoc/>
        public override async Task HandleAsync(ParseContext context)
        {
            string minSizeValue = GetParamValues(MinSizeParamName).FirstOrDefault();
            string maxSizeValue = GetParamValues(MaxSizeParamName).FirstOrDefault();
            string domain = GetParamValues(DomainParamName).FirstOrDefault();

            if (!string.IsNullOrEmpty(domain) && context.CurrentUrl.AbsoluteUri.IndexOf(domain, StringComparison.InvariantCultureIgnoreCase) != -1)
            { 
                int minSize = 0, maxSize = 0;
                if (int.TryParse(minSizeValue, out minSize) && context.Content.Length < minSize)
                {
                    return;
                }
                if (int.TryParse(maxSizeValue, out maxSize) && context.Content.Length >= maxSize)
                {
                    return;
                }
            }
            if (Next != null)
            {
                await Next.HandleAsync(context);
            }
        }
    }
}
