using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;

namespace PrismSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactivePropertySlim<string> Title { get; } = new("Prism Application");

        public ReactiveCommand ConfirmClose { get; }

        public MainWindowViewModel(IRegionManager regionManager) : base(regionManager)
        {
            regionManager.RegisterViewWithRegion<Views.SampleUserControl>("ContentRegion");

            ConfirmClose = new ReactiveCommand()
                .WithSubscribe(()=> System.Windows.MessageBox.Show("test"))
                .AddTo(Disposables);
        }
    }
}
