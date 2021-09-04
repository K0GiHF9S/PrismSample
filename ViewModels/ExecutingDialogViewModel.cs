using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismSample.ViewModels
{
    public class ExecutingDialogViewModel : ViewModelBase, IDialogAware
    {
        public ReactiveCommand YesButton { get; }

        public ExecutingDialogViewModel()
        {
            YesButton = new ReactiveCommand()
                .WithSubscribe(() => OnRequestClose())
                .AddTo(Disposables);
            RequestClose += ExecutingDialogViewModel_RequestClose;
        }

        private void ExecutingDialogViewModel_RequestClose(IDialogResult obj)
        {
        }

        public string Title => "sample";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() => Dispose();

        private void OnRequestClose()
        {
            RequestClose.Invoke(null);
        }

        public void OnDialogOpened(IDialogParameters parameters) { }
    }
}
