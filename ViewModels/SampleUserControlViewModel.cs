using Prism.Commands;
using Prism.Mvvm;
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
        public ReactiveCommand OpenSample4DialogCommand { get; }
        public AsyncReactiveCommand CancelSample4DialogCommand { get; }

        public SampleUserControlViewModel()
        {
            OpenSample4DialogCommand = new ReactiveCommand()
                .WithSubscribe(OpenSample4Dialog)
                .AddTo(Disposables);
            CancelSample4DialogCommand = new AsyncReactiveCommand()
                .WithSubscribe(async () => await Task.Run(CancelSample4Dialog))
                .AddTo(Disposables);
        }
        private bool _isSample4DialogOpen;
        private object _sample4Content;

        public bool IsSample4DialogOpen
        {
            get => _isSample4DialogOpen;
            set => SetProperty(ref _isSample4DialogOpen, value);
        }

        public object Sample4Content
        {
            get => _sample4Content;
            set => SetProperty(ref _sample4Content, value);
        }

        private void OpenSample4Dialog()
        {
            IsSample4DialogOpen = true;
        }

        private void CancelSample4Dialog() => IsSample4DialogOpen = false;
    }
}
