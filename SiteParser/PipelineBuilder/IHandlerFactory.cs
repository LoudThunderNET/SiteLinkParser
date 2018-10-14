using SiteParser.Handlers;

namespace SiteParser.PipelineBuilder
{
    public interface IHandlerFactory
    {
        IHandler GetHandler(PipelineConfigItem handler);
    }
}