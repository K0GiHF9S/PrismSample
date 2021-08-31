using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;

namespace PrismSample.ViewModels
{
    public class ViewModelBase : BindableBase, IDisposable, IDestructible
    {
        protected CompositeDisposable Disposables = new CompositeDisposable();

        private bool disposedValue;
        private readonly IRegionManager regionManager = null;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Disposables.Dispose();
                    if (regionManager != null)
                    {
                        foreach (var region in regionManager.Regions)
                        {
                            region.RemoveAll();
                        }
                    }
                }
                disposedValue = true;
            }
        }

        public ViewModelBase() { }

        public ViewModelBase(IRegionManager regionManager) => this.regionManager = regionManager;

        ~ViewModelBase() => Dispose(disposing: false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void Destroy() => Dispose();
    }
}
