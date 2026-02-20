using System;
using Interface;

namespace Extensions
{
    public abstract class ObjectObserver<T> : IDisposable
    {
        private readonly IObjectNotifier<T> _notifier;

        protected ObjectObserver(IObjectNotifier<T> notifier)
        {
            _notifier = notifier;
            notifier.Notified += OnNotified;
        }
        
        public virtual void Dispose()
        {
            _notifier.Notified -= OnNotified;
        }
        
        protected abstract void OnNotified(T notifiable);
    }
}