using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using PrismSample.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrismSample.ViewModels
{
    public class SampleUserControlViewModel : ViewModelBase
    {
        private readonly IDialogService dialogService;

        public ReactiveCommand StopCommand { get; }
        public ReactiveCommand StartCommand { get; }
        public ReactiveCommand OpenSample4DialogCommand { get; }
        public ReactiveCommand CancelSample4DialogCommand { get; }

        public ReactivePropertySlim<bool> IsSample4DialogOpen { get; }

        public SampleUserControlViewModel(IDialogService dialogService, IDialogHostService dialogHostService)
        //public SampleUserControlViewModel(IDialogService dialogService, Views.ExecutingDialog dialog)
        {
            this.dialogService = dialogService;

            var param = new DialogParameters();
            param.Add("OK", "sample");
            param.Add("Cancel", "ABC");

            StopCommand = new ReactiveCommand()
                .WithSubscribe(() => dialogHostService.ShowDialog(nameof(Views.MaterialDialog), param,
                r =>
                {
                }, "RootDialog"))
                .AddTo(Disposables);

            StartCommand = new ReactiveCommand()
                .WithSubscribe(() => dialogService.ShowDialog(nameof(Views.ExecutingDialog), param,
                r =>
                {
                    System.Diagnostics.Debug.WriteLine(r);
                }))
                .AddTo(Disposables);
            //StartCommand = new AsyncReactiveCommand()
            //    .WithSubscribe(() => DialogHost.Show(dialog, "MessageDialogHost"))
            //    .AddTo(Disposables);

            OpenSample4DialogCommand = new ReactiveCommand()
                .WithSubscribe(OpenSample4Dialog)
                .AddTo(Disposables);
            CancelSample4DialogCommand = new ReactiveCommand()
                .WithSubscribe(CancelSample4Dialog)
                .AddTo(Disposables);
            IsSample4DialogOpen = new ReactivePropertySlim<bool>()
                .AddTo(Disposables);
        }

        private void OpenSample4Dialog()
        {
            IsSample4DialogOpen.Value = true;
        }

        private void CancelSample4Dialog() => IsSample4DialogOpen.Value = false;
    }
}
