using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.Model.Logic
{
    public static class ProcessManager
    {
        public static Process StartProcess(int msDuration)
        {
            Process newProcess = Process.Start("test.txt");
            
            return newProcess;
        }

        public static void KillProcess(string pid)
        {
            Process matchingProcess = ProcessFetcher.FetchByPid(pid);
            if(matchingProcess != null)
            {
                matchingProcess.Kill();
            }
        }

        public static void SetPriority(string pid, ProcessPriorityClass priorityClass)
        {
            Process matchingProcess = ProcessFetcher.FetchByPid(pid);
            if(matchingProcess != null)
            {
                matchingProcess.PriorityClass = priorityClass;
            }
            
        }
    }
}
