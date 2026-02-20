using System;
using System.Collections.Generic;
using Interface;

namespace TargetSystem.Notifier
{
    public class SelectionNotifier : NotifierListener<Target>, IObjectNotifier<Target>
    {
        private readonly IPanelCreator _creator;
        private readonly List<IObjectNotifier<Target>> _subscriptions;

        public SelectionNotifier(IObjectNotifier<Target> objectNotifier)
            : base(objectNotifier)
        {
            _subscriptions = new List<IObjectNotifier<Target>>();
        }

        public event Action<Target> Notified;

        public override void Dispose()
        {
            base.Dispose();

            for (int i = 0; i < _subscriptions.Count; i++)
            {
                _subscriptions[i].Notified -= OnNotified;
            }
        }

        public void Subscribe(IObjectNotifier<Target> notifier)
        {
            if (_subscriptions.Contains(notifier))
            {
                throw new InvalidOperationException(nameof(notifier));
            }

            notifier.Notified += OnNotified;

            _subscriptions.Add(notifier);
        }

        protected override void OnNotified(Target target)
        {
            Notified?.Invoke(target);
        }
    }
}