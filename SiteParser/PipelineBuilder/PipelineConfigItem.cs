using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SiteParser.PipelineBuilder
{
    [XmlRoot(ElementName ="handler")]
    public class PipelineConfigItem
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("parallel")]
        public string GroupName { get; set; }

        [XmlArray(ElementName ="params")]
        [XmlArrayItem(ElementName = "param")]
        public List<HandlerParam> HandlerParams { get; set; }

        public PipelineConfigItem()
        {
            HandlerParams = new List<HandlerParam>();
        }
    }
}
