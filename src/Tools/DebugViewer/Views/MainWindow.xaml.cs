using System.Windows;
using System.Windows.Data;
using Autofac;
using DebugViewer.ViewModels;

namespace DebugViewer.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly object LockObj = new(); 
        private static readonly object LockObj2 = new();
        private static readonly object LockObj3 = new();
        
        public MainWindow()
        {
            InitializeComponent();

            var model = App.Container.Resolve<MainWindowViewModel>();
            DataContext = model;

            BindingOperations.EnableCollectionSynchronization(model.LogPanelViewModel.LogsCollection, LockObj);
            BindingOperations.EnableCollectionSynchronization(model.MetricsPanelViewModel.PerformanceMeasureCollection, LockObj2);
            BindingOperations.EnableCollectionSynchronization(model.ComponentPanelViewModel.ComponentsCollection, LockObj3);
        }
    }
}