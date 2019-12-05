using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.Model
{
    public class ProcessInListDisplay
    {
        public string Pid { get; set; }
        public string Name { get; set; }

        public string DisplayName { 
            get
            {
                return "[" + Pid + "]: " + Name;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(this.GetType()))
            {
                return ((ProcessInListDisplay)obj).Pid == Pid;
            }
            return false;
        }
    }
}
