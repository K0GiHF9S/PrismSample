using Prism.Ioc;
using Prism.Regions;
using PrismSample.Services;
using PrismSample.Views;
using System.Windows;

namespace PrismSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IDialogHostService, DialogHostService>();

            containerRegistry.RegisterDialog<ExecutingDialog>();
            containerRegistry.RegisterDialog<MaterialDialog>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var region_manager = Container.Resolve<IRegionManager>();
            region_manager.RegisterViewWithRegion<SamplePanel>("SampleRegion");
        }

    }
}
