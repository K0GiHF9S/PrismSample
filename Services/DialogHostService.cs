using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Services.Dialogs;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PrismSample.Services
{
    public class DialogHostService : IDialogHostService
    {
        private readonly IContainerProvider containerProvider;

        public DialogHostService(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
        }

        public void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback, string identifier)
        {
            if (DialogHost.IsDialogOpen(identifier))
            {
                return;
            }
            var view = containerProvider.Resolve<object>(name);
            var dialog_aware = (view as FrameworkElement)?.DataContext as IDialogAware;

            dialog_aware.RequestClose += p =>
            {
                DialogHost.Close(identifier, p);
            };

            DialogOpenedEventHandler opend_event = (_, _) =>
            {
                dialog_aware.OnDialogOpened(parameters ?? new DialogParameters());
            };

            DialogClosingEventHandler closing_event = (o, args) =>
            {
                if (dialog_aware.CanCloseDialog())
                {
                    dialog_aware.OnDialogClosed();
                    callback?.Invoke((args.Parameter as DialogResult) ?? new DialogResult());
                }
                else
                {
                    args.Cancel();
                }
            };

            _ = DialogHost.Show(view, identifier, opend_event, closing_event).ConfigureAwait(false);
        }
    }
}
