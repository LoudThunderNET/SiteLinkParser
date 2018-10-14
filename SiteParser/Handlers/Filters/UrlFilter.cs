using SiteParser.Handlers;
using SiteParser.PipelineBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiteParser.Handlers.Filters
{
    public class UrlFilter : HandlerBase
    {
        private const string AllowPatternParamName = "AllowPattern";
        private const string DisallowPatternParamName = "DisallowPattern";
        private readonly List<Regex> allowPatterns;
        private readonly List<Regex> disallowPatterns;
        public UrlFilter(PipelineConfigItem config) : base(config)
        {

            allowPatterns = GetParamValues(AllowPatternParamName)
                .Select(InitializeRegex)
                .Where(p=>p != null)
                .ToList();

            disallowPatterns = GetParamValues(DisallowPatternParamName)
                .Select(InitializeRegex)
                .Where(p => p != null)
                .ToList();
        }

        public override async Task HandleAsync(ParseContext context)
        {
            var linksToDelete = new List<Uri>();
            foreach (bool key in context.Links.Keys)
            {
                foreach (Uri link in context.Links[key])
                {
                    bool isAllowed = allowPatterns.Count == 0
                                        ? true
                                        : allowPatterns.Any(r => r.IsMatch(link.AbsoluteUri));
                    bool isDisallowed = disallowPatterns.Count == 0
                                        ? false
                                        : disallowPatterns.Any(r => r.IsMatch(link.AbsoluteUri));
                    if (isDisallowed)
                    {
                        linksToDelete.Add(link);
                    }
                }
                foreach (var linkToDelete in linksToDelete)
                {
                    context.Links[key].Remove(linkToDelete);
                }
            }
            if (Next != null)
            {
                await Next.HandleAsync(context);
            }
        }

        private Regex InitializeRegex(string pattern)
        {
            if (!string.IsNullOrEmpty(pattern))
            {
                try
                {
                    return new Regex(pattern);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
