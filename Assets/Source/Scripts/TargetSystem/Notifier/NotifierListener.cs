using System;
using Interface;

namespace TargetSystem.Notifier
{
    public abstract class NotifierListener<T> : IDisposable
    {
        private readonly IObjectNotifier<T> _notifier;

        protected NotifierListener(IObjectNotifier<T> notifier)
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