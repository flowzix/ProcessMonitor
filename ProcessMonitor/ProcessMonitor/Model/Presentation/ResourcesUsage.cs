using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.Model.Presentation
{
    public class ResourcesUsage
    {
        public string CpuUsage { get; set; }
        public string MemoryUsage { get; set; }
        public string CacheHits { get; set; }
    }
}
