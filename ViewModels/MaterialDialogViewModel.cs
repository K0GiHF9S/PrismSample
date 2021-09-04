using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismSample.ViewModels
{
    class MaterialDialogViewModel : ViewModelBase, IDialogAware
    {
        public static readonly string OK_LABEL_KEY = "OK";
        public static readonly string CANCEL_LABEL_KEY = "Cancel";

        public string Title => throw new NotImplementedException();

        public event Action<IDialogResult> RequestClose;

        public MaterialDialogViewModel()
        {
            OkLabel = new ReactivePropertySlim<string>(OK_LABEL_KEY)
                .AddTo(Disposables);
            CancelLabel = new ReactivePropertySlim<string>(CANCEL_LABEL_KEY)
                .AddTo(Disposables);

            OkCommand = new ReactiveCommand()
                .WithSubscribe(() => OnRequestClose(ButtonResult.OK))
                .AddTo(Disposables);
            CancelCommand = new ReactiveCommand()
                .WithSubscribe(() => OnRequestClose(ButtonResult.Cancel))
                .AddTo(Disposables);
        }

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() => Dispose();

        private void OnRequestClose(ButtonResult buttonResult)
        {
            RequestClose?.Invoke(new DialogResult(buttonResult));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.TryGetValue(OK_LABEL_KEY, out string ok_label))
            {
                OkLabel.Value = ok_label;
            }
            if (parameters.TryGetValue(CANCEL_LABEL_KEY, out string cancel_label))
            {
                CancelLabel.Value = cancel_label;
            }
        }

        public ReactivePropertySlim<string> OkLabel { get; }
        public ReactiveCommand OkCommand { get; }
        public ReactivePropertySlim<string> CancelLabel { get; }
        public ReactiveCommand CancelCommand { get; }
    }
}
