using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProcessMonitor.Model;
using ProcessMonitor.Model.Events;
using ProcessMonitor.Model.Logic;
using ProcessMonitor.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessMonitor.ViewModels
{
    public class ProcessListViewModel : BindableBase, ISortsTwoWay, IUpdatesProgress
    {
        private ObservableCollection<ProcessInListDisplay> _processList = new ObservableCollection<ProcessInListDisplay>();
        public ObservableCollection<ProcessInListDisplay> ProcessList
        {
            get { return _processList; }
            set { SetProperty(ref _processList, value); }
        }
        private ObservableCollection<ProcessInListDisplay> _filteredProcessList = new ObservableCollection<ProcessInListDisplay>();
        public ObservableCollection<ProcessInListDisplay> FilteredProcessList
        {
            get { return _filteredProcessList; }
            set { SetProperty(ref _filteredProcessList, value);}
        }

        private string _filterPhrase = "";
        public string FilterPhrase
        {
            get { return _filterPhrase; }
            set 
            { 
                SetProperty(ref _filterPhrase, value);
                ApplyFilters();
            }
        }

        private ProcessInListDisplay _selectedProcess;
        public ProcessInListDisplay SelectedProcess
        {
            get { return _selectedProcess; }
            set
            {
                SetProperty(ref _selectedProcess, value);
                RaiseProcessSelectedEvent(value);
            }
        }

        private int _percentOfProcessFinished = 50;
        public int PercentOfProcessFinished
        {
            get { return _percentOfProcessFinished; }
            set{ SetProperty(ref _percentOfProcessFinished, value); }
        }

        public DelegateCommand RefreshProcessListCommand { get; set; }
        public DelegateCommand CreateProcessCommand { get; set; }
        public DelegateCommand TerminateProcessCommand { get; set; }
        public DelegateCommand SortCommand { get; set; }
        public DelegateCommand SetProcessPriorityCommand { get; set; }

        private IEventAggregator _eventAggregator;
        private TwoDirPresenterSorter sorter;
        private bool isProcessStarted = false;

        public ProcessListViewModel(IEventAggregator eventAggregator)
        {
            RefreshProcessListCommand = new DelegateCommand(RefreshProcessList);
            CreateProcessCommand = new DelegateCommand(CreateProcess);
            TerminateProcessCommand = new DelegateCommand(TerminateProcess);
            SetProcessPriorityCommand = new DelegateCommand(DisplayProcessControlWindow);

            _eventAggregator = eventAggregator;

            sorter = new TwoDirPresenterSorter(this);
            SortCommand = new DelegateCommand(sorter.SortNext);
        }

        private void DisplayProcessControlWindow()
        {
            new ProcessControlWindow().Show();
        }

        private void RefreshProcessList()
        {

            if (SelectedProcess != null)
            {
                SelectedProcess = MapProcessForDisplay(ProcessFetcher.FetchByPid(SelectedProcess.Pid));
            }

            List<ProcessInListDisplay> ProcessesForDisplay = ProcessFetcher.Fetch()
                .Select(process => MapProcessForDisplay(process))
                .ToList();

            UpdateProcessListWithNewItems(ProcessesForDisplay);
        }

        private void ApplyFilters()
        {
            FilteredProcessList.Clear();
            List<ProcessInListDisplay> MatchingProcesses =
                ProcessList.Where(process => 
                process.DisplayName.ToLower().Contains(FilterPhrase.ToLower()) || 
                process.Pid.Contains(FilterPhrase))
                .ToList();
            FilteredProcessList.AddRange(MatchingProcesses);
        }

        private void CreateProcess()
        {
            if (isProcessStarted)
                return;
            Process newProcess = ProcessManager.StartProcess(10000);
            if (newProcess != null)
                isProcessStarted = true;
            ProgressReportingInvoker pri = new ProgressReportingInvoker(
                () => { ProcessManager.KillProcess(newProcess.Id.ToString()); isProcessStarted = false; },
                this,
                new Func<string, bool>(IsProcessAlive));


            Thread newProcessThread = new Thread(() => pri.Start(10000, newProcess.Id.ToString()));
            AppThreadManager.AddThread(newProcessThread);
            newProcessThread.Start();
        }

        private bool IsProcessAlive(string pid)
        {
            return ProcessFetcher.FetchByPid(pid) != null;
        }
        private void TerminateProcess()
        {
            if(SelectedProcess != null)
            {
                ProcessManager.KillProcess(SelectedProcess.Pid);
                ProcessList.Remove(SelectedProcess);
                ApplyFilters();
            }
        }
        private ProcessInListDisplay MapProcessForDisplay(Process process)
        {
            return new ProcessInListDisplay { Name = process.ProcessName, Pid = process.Id.ToString() };
        }
        private void RaiseProcessSelectedEvent(ProcessInListDisplay value)
        {
            if (value != null)
            {
                _eventAggregator.GetEvent<ProcessPidSelectedEvent>().Publish(value.Pid);
            }
        }

        private void UpdateProcessListWithNewItems(List<ProcessInListDisplay> newItems)
        {
            ProcessList.Clear();
            ProcessList.AddRange(newItems);
            ApplyFilters();
        }
        public void SortAscending()
        {
            List<ProcessInListDisplay> sorted = ProcessList.OrderBy(obj => obj.Name).ToList();
            UpdateProcessListWithNewItems(sorted);
        }

        public void SortDescending()
        {
            List<ProcessInListDisplay> sorted = ProcessList.OrderByDescending(obj => obj.Name).ToList();
            UpdateProcessListWithNewItems(sorted);
        }

        public void SortDefault()
        {
            List<ProcessInListDisplay> sorted = ProcessList.OrderByDescending(a => Guid.NewGuid()).ToList();
            UpdateProcessListWithNewItems(sorted);
        }

        public void UpdateProgress(int progress)
        {
            PercentOfProcessFinished = progress;
        }
    }
}
