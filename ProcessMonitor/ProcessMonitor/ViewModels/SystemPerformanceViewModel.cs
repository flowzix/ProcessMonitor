using Prism.Mvvm;
using ProcessMonitor.Model.Logic;
using ProcessMonitor.Model.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessMonitor.ViewModels
{
    public class SystemPerformanceViewModel : BindableBase
    {
        private ResourcesUsage _usage;
        public ResourcesUsage Usage
        {
            get { return _usage; }
            set { SetProperty(ref _usage, value); }
        }

        private ResourceMonitor resourceMonitor;
       
        public SystemPerformanceViewModel()
        {
            resourceMonitor = new ResourceMonitor();
            StartSystemStatsThread();
        }
        private void StartSystemStatsThread()
        {
            Thread statsCheckThread = new Thread(StartUpdatingSystemStats);
            AppThreadManager.AddThread(statsCheckThread);
            statsCheckThread.Start();
        }

        private void StartUpdatingSystemStats()
        {
            while (true)
            {
                Usage = resourceMonitor.GetResourcesUsage();
                Thread.Sleep(500);
            }

        }
    }
}
