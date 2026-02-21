using System;
using Interface;

namespace TargetSystem.Notifier
{
    public abstract class SelectionListener<T> : IDisposable
    {
        private readonly ISelectionNotifier<T> _notifier;

        protected SelectionListener(ISelectionNotifier<T> notifier)
        {
            _notifier = notifier;
            notifier.Selected += OnSelected;
            notifier.Unselected += OnUnselected;
        }

        public virtual void Dispose()
        {
            _notifier.Selected -= OnSelected;
            _notifier.Unselected -= OnUnselected;
        }

        protected abstract void OnSelected(T notifiable);

        protected abstract void OnUnselected(T target);
    }
}