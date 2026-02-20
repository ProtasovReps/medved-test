using System;
using System.Collections.Generic;
using Extensions;
using Interface;

namespace TargetSystem.Notifier
{
    public class SelectionNotifier : ObjectObserver<Target>, ISelectionNotifier<Target>
    {
        private readonly INotifiablePool _creator;
        private readonly HashSet<Target> _selectedTargets;
        private readonly List<IObjectNotifier<Target>> _subscriptions;

        public SelectionNotifier(IObjectNotifier<Target> objectNotifier)
            : base(objectNotifier)
        {
            _selectedTargets = new HashSet<Target>();
            _subscriptions = new List<IObjectNotifier<Target>>();
        }

        public event Action<Target> Selected;
        public event Action<Target> Unselected;

        public override void Dispose()
        {
            base.Dispose();

            for (int i = 0; i < _subscriptions.Count; i++)
            {
                _subscriptions[i].Notified -= OnNotified;
            }
        }

        public void AddSubscribtion(IObjectNotifier<Target> notifier)
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
            if (_selectedTargets.Contains(target))
            {
                _selectedTargets.Remove(target);
                Unselected?.Invoke(target);
            }
            else
            {
                _selectedTargets.Add(target);
                Selected?.Invoke(target);
            }
        }
    }
}