using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SiteParser.PipelineBuilder;

namespace SiteParser.Handlers
{
    /// <summary>
    /// Базовый класс для парсеров ссылок.
    /// </summary>
    public abstract class LinkParserBase: HandlerBase
    {
        /// <summary>
        /// Регулярное выражение для тэга.
        /// </summary>
        protected abstract Regex TagReg { get; }

        /// <summary>
        /// Регулярное выражения для получения ссылки из тега.
        /// </summary>
        protected abstract Regex LinkReg { get; }

        protected abstract bool IsNeedToProcess(ParseContext context, Uri uri);
        protected LinkParserBase(PipelineConfigItem config) : base(config)
        {
        }

        /// <inheritdoc/>
        public override async Task HandleAsync(ParseContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Content))
            {
                return;
            }
            MatchCollection mathes = TagReg.Matches(context.Content);
            for (int i = 0; i < mathes.Count; i++)
            {
                GroupCollection srcGroups = LinkReg.Match(mathes[i].Value).Groups;
                if (srcGroups.Count >= 2)
                {
                    try
                    {
                        var uri = new Uri(srcGroups[1].Value);
                        if (!string.IsNullOrWhiteSpace(uri.Host) && !uri.Scheme.Equals("javascript", StringComparison.InvariantCultureIgnoreCase))
                        {
                            context.Links[IsNeedToProcess(context, uri)].Add(uri);
                        }
                    }
                    catch (Exception) { }
                }
            }
            if (Next != null)
            {
                await Next.HandleAsync(context);
            }
        }

    }
}
