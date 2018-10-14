using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.PipelineBuilder
{
    public class EmptyConfigurationProvider : IConfigurationProvider
    {
        public HandlersCollection GetConfig()
        {
            return new HandlersCollection();
        }

        public PipelineConfigItem GetHandlerConfig(Type handlerType)
        {
            return new PipelineConfigItem();
        }
    }
}
