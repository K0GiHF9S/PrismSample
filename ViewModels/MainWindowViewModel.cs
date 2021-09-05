using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismSample.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.ComponentModel;

namespace PrismSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactivePropertySlim<string> Title { get; } = new("Prism Application");

        public AsyncReactiveCommand<Action<bool>> ConfirmClose { get; }

        public MainWindowViewModel(IRegionManager regionManager, IDialogHostService dialogHostService, IDialogService s) : base(regionManager)
        {
            regionManager.RegisterViewWithRegion<Views.SampleUserControl>("ContentRegion");

            ConfirmClose = new AsyncReactiveCommand<Action<bool>>()
                .WithSubscribe(async callback =>
                {
                    await dialogHostService.ShowDialog(nameof(Views.MaterialDialog), null,
                        result =>
                        {
                            callback(result.Result == ButtonResult.OK);
                        }, "RootDialog").ConfigureAwait(false);
                }
                )
                .AddTo(Disposables);
        }
    }
}
