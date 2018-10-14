using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteParser.Handlers;

namespace SiteParser.PipelineBuilder
{
    public class PipelineProvider : IPipelineProvider
    {
        private readonly IHandlerFactory _handlerFactory;
        readonly IConfigurationProvider _configurationProvider;

        public PipelineProvider(IConfigurationProvider configurationProvider, 
            IHandlerFactory handlerFactory)
        {
            _configurationProvider = configurationProvider;
            _handlerFactory = handlerFactory;
        }

        public IHandler GetPipeline()
        {
            var pipeLineHandlres = new List<IHandler>();
            HandlersCollection handlersCollection = _configurationProvider.GetConfig();

            foreach (PipelineConfigItem handler in handlersCollection.Handlers)
            {
                pipeLineHandlres.Add(_handlerFactory.GetHandler(handler));
            }

            if (pipeLineHandlres.Count > 0)
            {
                IHandler pipeline = null;
                pipeLineHandlres.ForEach(h =>
                {
                    if (pipeline != null)
                    {
                        pipeline.Next = h;
                    }
                    pipeline = h;
                });

                return pipeLineHandlres[0];
            }
            return null;
        }
    }
}
