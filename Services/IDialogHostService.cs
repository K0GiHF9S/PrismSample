using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismSample.Services
{
    public interface IDialogHostService
    {
        void ShowDialog(string name, IDialogParameters parameters, Action<IDialogResult> callback, string identifier);
    }
}
