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

        public async void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback, string identifier)
        {
            var view = containerProvider.Resolve<object>(name);
            var dialog_aware = (view as FrameworkElement)?.DataContext as IDialogAware;

            dialog_aware.RequestClose += p =>
            {
                if (dialog_aware.CanCloseDialog())
                {
                    DialogHost.Close(identifier);
                    dialog_aware.OnDialogClosed();
                    callback(p ?? new DialogResult());
                }
            };
            var task = DialogHost.Show(view, identifier);
            dialog_aware.OnDialogOpened(parameters ?? new DialogParameters());
            await task;
        }
    }
}
