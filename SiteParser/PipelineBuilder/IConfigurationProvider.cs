using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser.PipelineBuilder
{
    public interface IConfigurationProvider
    {
        HandlersCollection GetConfig();
        PipelineConfigItem GetHandlerConfig(Type handlerType);
    }
}
