using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProcessMonitor.Model.Events;
using ProcessMonitor.Model.Logic;
using System.Threading;

namespace ProcessMonitor.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand MinimizeCommand { get; set; }
        private IEventAggregator _eventAggregator;
        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            CloseCommand = new DelegateCommand(CloseWindow);
            MinimizeCommand = new DelegateCommand(MinimizeWindow);
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ThreadMadeEvent>().Subscribe(AddToManagedThreads);
        }
        private void CloseWindow()
        {
            AppThreadManager.TerminateEvery();
            System.Windows.Application.Current.Shutdown();
        }

        private void MinimizeWindow()
        {

        }
        private void AddToManagedThreads(Thread t)
        {
            AppThreadManager.AddThread(t);
        }
    }
}
