using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SiteParser.PipelineBuilder;

namespace SiteParser.Handlers
{
    public class AnchorLinkParser : LinkParserBase
    {
        protected override Regex TagReg => new Regex("<\\s*a.*href\\s*=\\s*\"(.*)\"{1}>", RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        protected override Regex LinkReg => new Regex("href\\s*=\\s*\"(\\S +)\"", RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        public AnchorLinkParser(PipelineConfigItem config) : base(config)
        {
        }

        protected override bool IsNeedToProcess(ParseContext context, Uri uri)
        {
            return !uri.IsFile && !uri.IsLoopback && !string.IsNullOrWhiteSpace(uri.Host);
        }
    }
}
