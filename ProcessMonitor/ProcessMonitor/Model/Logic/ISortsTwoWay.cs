using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.Model.Logic
{
    public interface ISortsTwoWay
    {
        void SortAscending();
        void SortDescending();
        void SortDefault();
    }
}
