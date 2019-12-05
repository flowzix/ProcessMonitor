using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.Model
{
    public static class ProcessFetcher
    {
        public static List<Process> Fetch()
        {
            return new List<Process>(Process.GetProcesses());
        }
        public static Process FetchByPid(string pid)
        {
            Process fetchedProcess = null;
            try
            {
                fetchedProcess = Process.GetProcessById(Int32.Parse(pid));
                return fetchedProcess;
            }
            catch
            {
                return null;
            }
        }
    }
}
