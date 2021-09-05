using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PrismSample.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Reactive.Bindings.Notifiers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PrismSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReactivePropertySlim<string> Title { get; } = new("Prism Application");

        public AsyncReactiveCommand<Action> ConfirmClose { get; }

        public ReadOnlyReactivePropertySlim<bool> IsBusy { get; }
        public AsyncReactiveCommand BusyCommand { get; }
        public ReactiveCommand StopCommand { get; }

        private CancellationTokenSource cancellationTokenSource;

        public ReactiveCollection<string> SampleList { get; }
        [Required(ErrorMessage = "選択必須")]
        public ReactiveProperty<string> SelectedSample { get; }

        public MainWindowViewModel(IRegionManager regionManager, IDialogHostService dialogHostService, IDialogService s) : base(regionManager)
        {
            regionManager.RegisterViewWithRegion<Views.SampleUserControl>("ContentRegion");

            ConfirmClose = new AsyncReactiveCommand<Action>()
                .WithSubscribe(async callback =>
                {
                    await dialogHostService.ShowDialog(nameof(Views.MaterialDialog), null,
                        result =>
                        {
                            if (result.Result == ButtonResult.OK)
                            {
                                callback();
                            }
                        }, "RootDialog").ConfigureAwait(false);
                }
                )
                .AddTo(Disposables);

            var can_execute = new ReactivePropertySlim<bool>(true);

            IsBusy = can_execute
                .Inverse()
                .ToReadOnlyReactivePropertySlim()
                .AddTo(Disposables);


            BusyCommand = can_execute
                .ToAsyncReactiveCommand()
                .WithSubscribe(async () =>
                {
                    try
                    {
                        using (cancellationTokenSource = new CancellationTokenSource())
                        {
                            await Task.Delay(10000, cancellationTokenSource.Token);
                        }
                    }
                    catch (TaskCanceledException)
                    {
                    }
                    finally
                    {
                        cancellationTokenSource = null;
                    }
                })
                .AddTo(Disposables);

            StopCommand = IsBusy
                .ToReactiveCommand()
                .WithSubscribe(() => cancellationTokenSource?.Cancel())
                .AddTo(Disposables);

            SampleList = new ReactiveCollection<string>()
                .AddTo(Disposables);

            SampleList.Add("A");
            SampleList.Add("B");
            SampleList.Add("C");

            SelectedSample = new ReactiveProperty<string>()
                .SetValidateAttribute(() => SelectedSample)
                .AddTo(Disposables);
        }
    }
}
