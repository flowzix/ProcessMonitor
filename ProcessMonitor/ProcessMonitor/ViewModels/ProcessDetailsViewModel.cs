using Prism.Events;
using Prism.Mvvm;
using ProcessMonitor.Model;
using ProcessMonitor.Model.Events;
using ProcessMonitor.Model.Presentation;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessMonitor.ViewModels
{
    public class ProcessDetailsViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private ProcessInDetailsDisplay _displayedProcess = new ProcessInDetailsDisplay();
        public ProcessInDetailsDisplay DisplayedProcess
        {
            get { return _displayedProcess; }
            set { SetProperty(ref _displayedProcess, value); }
        }

        public ProcessDetailsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ProcessPidSelectedEvent>().Subscribe(LoadDetailsOfProcess);
        }

        private void LoadDetailsOfProcess(string pid)
        {
            Process selectedProcess = ProcessFetcher.FetchByPid(pid);
            if(selectedProcess == null)
            {
                return;
            }
            DisplayedProcess = MapProcessToDisplay(selectedProcess);
        }

        private ProcessInDetailsDisplay MapProcessToDisplay(Process process)
        {
            List<string> threadNames = new List<string>();
            List<string> moduleNames = new List<string>();
            //IEnumerator moduleEnumerator = process.Modules.GetEnumerator();
            IEnumerator threadEnumerator = process.Threads.GetEnumerator();
            //while (moduleEnumerator.MoveNext())
            //    moduleNames.Add(((ProcessModule)moduleEnumerator.Current).ModuleName);
            while (threadEnumerator.MoveNext())
                threadNames.Add(((ProcessThread)threadEnumerator.Current).Id.ToString());

            return new ProcessInDetailsDisplay
            {
                Name = process.ProcessName,
                Memory = process.NonpagedSystemMemorySize64.ToString(),
                ThreadsCount = process.Threads.Count.ToString(),
                ModulesCount = "",//process.Modules.Count.ToString(),
                Pid = process.Id.ToString(),
                Priority = process.PriorityClass.ToString(),
                Responding = process.Responding.ToString(),
                ThreadNames = threadNames,
                ModuleNames = moduleNames
            };
        }

    }
}
