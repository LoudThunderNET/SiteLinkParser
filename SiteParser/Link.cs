using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser
{
    internal class LinkInfo
    {
        public Uri Link { get; set; }
        public bool Processed { get; set; }
        public bool NeedToProcess { get; set; }
    }
}
