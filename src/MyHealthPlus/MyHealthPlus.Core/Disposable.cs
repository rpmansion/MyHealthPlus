using System;

namespace MyHealthPlus.Core
{
    public class Disposable : IDisposable
    {
        protected bool Disposed;

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                DisposeCore();
            }

            Disposed = true;
        }

        public void ThrowIfDisposed()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void DisposeCore()
        {
        }
    }
}