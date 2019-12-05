using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProcessMonitor.Model.Events;
using ProcessMonitor.Model.Logic;
using ProcessMonitor.Model.Presentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMonitor.ViewModels
{
    public class ProcessControlViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;
        private string ProcessPidSelected = null;
        public DelegateCommand SetProcessPriorityCommand { get; set; }
        public ProcessPriorityInComboBox SelectedPriority { get; set; }
        public List<ProcessPriorityInComboBox> ProcessPriorities { get; set; }
        public ProcessControlViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ProcessPidSelectedEvent>().Subscribe(ProcessSelected);
            SetProcessPriorityCommand = new DelegateCommand(SetProcessPriority);
            ProcessPriorities = ProcessPriorityInComboBoxGenerator.Generate();

        }
        private void ProcessSelected(string pid)
        {
            ProcessPidSelected = pid;
        }
        private void SetProcessPriority()
        {
            ProcessManager.SetPriority(ProcessPidSelected, SelectedPriority.Priority);
        }
    }
}
