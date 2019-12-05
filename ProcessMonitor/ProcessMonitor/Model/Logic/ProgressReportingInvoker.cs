using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessMonitor.Model.Logic
{
    public class ProgressReportingInvoker
    {
        private Action ActionToInvoke { get; set; }
        private Func<string, bool> ShouldContinue { get; set; }
        private IUpdatesProgress UpdatesProgress { get; set; }
        private int msElapsed;
        public ProgressReportingInvoker(Action actionToInvoke, IUpdatesProgress updatesProgress, Func<string, bool> shouldContinue)
        {
            ActionToInvoke = actionToInvoke;
            msElapsed = 0;
            UpdatesProgress = updatesProgress;
            ShouldContinue = shouldContinue;

        }
        public void Start(int delayMs, string param)
        {
            int step = delayMs / 50;
            while (msElapsed < delayMs && ShouldContinue(param)) 
            {
                Thread.Sleep(step);
                msElapsed += step;
                UpdatesProgress.UpdateProgress(msElapsed * 100 / delayMs);
            }
            ActionToInvoke.Invoke();
        }

    }
}
