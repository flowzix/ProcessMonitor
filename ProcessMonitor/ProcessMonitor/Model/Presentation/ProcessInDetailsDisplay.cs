using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.Model.Presentation
{
    public class ProcessInDetailsDisplay
    {
        public ProcessInDetailsDisplay()
        {
            Name = "-";
            Pid = "-";
            Memory = "-";
            Priority = "-";
            ThreadsCount = "-";
            ModulesCount = "-";
            Responding = "-";
        }
        public string Name { get; set; }
        public string Pid { get; set; }
        public string Memory { get; set; }
        public string Priority { get; set; }
        public string ThreadsCount { get; set; }
        public string ModulesCount { get; set; }
        public string Responding { get; set; }
        public List<string> ThreadNames { get; set; }
        public List<string> ModuleNames { get; set; }
    }
}
