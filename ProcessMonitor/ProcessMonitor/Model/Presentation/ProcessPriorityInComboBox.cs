using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.Model.Presentation
{
    public class ProcessPriorityInComboBox
    {
        public ProcessPriorityClass Priority { get; set; }
        public string PriorityName { get; set; }
    }

    public static class ProcessPriorityInComboBoxGenerator
    {
        public static List<ProcessPriorityInComboBox> Generate()
        {
            List<ProcessPriorityInComboBox> prioritaits = new List<ProcessPriorityInComboBox>();
            prioritaits.Add(new ProcessPriorityInComboBox { Priority = ProcessPriorityClass.Idle, PriorityName = "Idle" });
            prioritaits.Add(new ProcessPriorityInComboBox { Priority = ProcessPriorityClass.BelowNormal, PriorityName = "Below normal" });
            prioritaits.Add(new ProcessPriorityInComboBox { Priority = ProcessPriorityClass.Normal, PriorityName = "Normal" });
            prioritaits.Add(new ProcessPriorityInComboBox { Priority = ProcessPriorityClass.AboveNormal, PriorityName = "Above normal" });
            prioritaits.Add(new ProcessPriorityInComboBox { Priority = ProcessPriorityClass.RealTime, PriorityName = "Real time" });
            return prioritaits;
        }
    }
}
