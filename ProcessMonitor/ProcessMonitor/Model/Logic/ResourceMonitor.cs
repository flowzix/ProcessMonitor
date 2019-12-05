using ProcessMonitor.Model.Presentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.Model.Logic
{
    public class ResourceMonitor
    {
        PerformanceCounter cpuCounter;
        PerformanceCounter ramPercentCounter;
        PerformanceCounter cacheHitsPercent;
        public ResourceMonitor()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramPercentCounter = new PerformanceCounter("Memory", "Available MBytes");
            cacheHitsPercent = new PerformanceCounter("Cache", "Copy Read Hits %");
        }

        public ResourcesUsage GetResourcesUsage()
        {
            return new ResourcesUsage
            {
                CpuUsage = cpuCounter.NextValue().ToString(),
                MemoryUsage = ramPercentCounter.NextValue().ToString(),
                CacheHits = cacheHitsPercent.NextValue().ToString()
            };
        }
    }
}
