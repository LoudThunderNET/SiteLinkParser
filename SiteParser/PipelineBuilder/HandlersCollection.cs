using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SiteParser.PipelineBuilder
{
    [XmlRoot("handlers")]
    public class HandlersCollection
    {
        [XmlElement("handler")]
        public List<PipelineConfigItem> Handlers { get; set; }
        public HandlersCollection()
        {
            Handlers = new List<PipelineConfigItem>();
        }
    }
}
