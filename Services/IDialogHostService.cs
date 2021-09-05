using Prism.Services.Dialogs;
using System;
using System.Threading.Tasks;

namespace PrismSample.Services
{
    public interface IDialogHostService
    {
        Task ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback, string identifier);
    }
}
