using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismSample.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;

namespace PrismSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactivePropertySlim<string> Title { get; } = new("Prism Application");

        public ReactiveCommand ConfirmClose { get; }

        public ReactivePropertySlim<bool> CanClose { get; }

        public MainWindowViewModel(IRegionManager regionManager, IDialogHostService dialogHostService) : base(regionManager)
        {
            regionManager.RegisterViewWithRegion<Views.SampleUserControl>("ContentRegion");

            CanClose = new ReactivePropertySlim<bool>()
                .AddTo(Disposables);

            ConfirmClose = new ReactiveCommand()
                .WithSubscribe(() => dialogHostService.ShowDialog(nameof(Views.MaterialDialog), null, result =>
                {
                    CanClose.Value = result.Result == ButtonResult.OK;
                }, "RootDialog"))
                .AddTo(Disposables);
        }
    }
}
