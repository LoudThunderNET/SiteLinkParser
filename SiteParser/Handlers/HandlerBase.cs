using SiteParser.PipelineBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.Handlers
{
    public abstract class HandlerBase : IHandler
    {
        public IHandler Next { get; set; }
        protected PipelineConfigItem Configuration { get; set; }
        protected HandlerBase(PipelineConfigItem config)
        {
            Configuration = config;
        }

        public abstract Task HandleAsync(ParseContext context);

        protected List<string> GetParamValues(string paramValue)
        {
            return Configuration.HandlerParams
                .Where(p => p.Name.Equals(paramValue, StringComparison.InvariantCultureIgnoreCase))
                .Select(p => p.Value)
                .ToList();
        }
    }
}
