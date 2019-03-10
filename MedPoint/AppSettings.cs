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
        public EmailSettings EmailSettings { get; set; }
    }

    public class ResourcePaths
    {
        public string Base { get; set; }
        public string Images { get; set; }
    }

    public class Urls
    {
        public string Images { get; set; }
        public string ConfirmSuccess { get; set; }
        public string ConfirmFailure { get; set; }
    }

    public class EmailSettings
    {
        public string SMTPServer { get; set; }
        public string SMTPUserName { get; set; }
        public string SMPTPassword { get; set; }
        public string DisplayName { get; set; }
        public int SMPTPort { get; set; }
    }
}
