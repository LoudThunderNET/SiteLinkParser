using System;
using System.Text.RegularExpressions;
using SiteParser.PipelineBuilder;

namespace SiteParser.Handlers
{
    /// <summary>
    /// Реализация <see cref="IHandler"/> для парсера ссылок с тегов img.
    /// </summary>
    public class ImgLinkParser : LinkParserBase
    {
        protected override Regex TagReg => new Regex("<img.*src\\s*=\\s*\"(.*)\"\\s*/>", RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        protected override Regex LinkReg => new Regex("src\\s*=\\s*\"(.*)\"", RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        /// <summary>
        /// Инициализирует экземпляр <see cref="ImgLinkParser"/>.
        /// </summary>
        /// <param name="configProvider"></param>
        public ImgLinkParser(PipelineConfigItem configProvider) : base(configProvider)
        {
        }

        protected override bool IsNeedToProcess(ParseContext context, Uri uri)
        {
            return false;
        }
    }
}
