using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using System;

namespace PrismSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactivePropertySlim<string> Title { get; } = new ReactivePropertySlim<string>("Prism Application");

        public MainWindowViewModel(IRegionManager regionManager) : base(regionManager)
        {
            regionManager.RegisterViewWithRegion<Views.SampleUserControl>("ContentRegion");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            System.Diagnostics.Debug.WriteLine($"{GetType().Name}");
        }
    }
}
