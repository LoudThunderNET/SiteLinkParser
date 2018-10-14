using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SiteParser.Handlers;

namespace SiteParser.PipelineBuilder
{
    public class HandlerFactory : IHandlerFactory
    {
        readonly IServiceProvider _serviceProvider;
        public HandlerFactory(IServiceProvider serviceProvider )
        {
            _serviceProvider = serviceProvider;
        }

        public IHandler GetHandler(PipelineConfigItem handlerCfg)
        {
            Type handlerType = Type.GetType(handlerCfg.Type);
            return (IHandler)ActivatorUtilities.CreateInstance(_serviceProvider, handlerType, handlerCfg);
        }
    }
}
