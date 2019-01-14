using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPoint
{
    public class AppSettings
    {
        public ResourcePaths ResourcePaths { get; set; }
        public Urls Urls { get; set; }
    }

    public class ResourcePaths
    {
        public string Base { get; set; }
        public string Images { get; set; }
    }

    public class Urls
    {
        public string Images { get; set; }
    }
}
