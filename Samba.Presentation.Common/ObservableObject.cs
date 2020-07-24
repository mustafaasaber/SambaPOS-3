using Prism.Mvvm;
using System;

namespace Samba.Presentation.Common
{
    public abstract class ObservableObject : BindableBase
    {
        ~ObservableObject()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //
        }
    }
}
