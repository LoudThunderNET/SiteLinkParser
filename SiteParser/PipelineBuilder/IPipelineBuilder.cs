using SiteParser.Handlers;

namespace SiteParser.PipelineBuilder
{
    public interface IPipelineProvider
    {
        IHandler GetPipeline();
    }
}