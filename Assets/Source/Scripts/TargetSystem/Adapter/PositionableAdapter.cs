using System;
using Extensions;
using Interface;
using TargetSystem.Notifier;

namespace TargetSystem.Adapter
{
    public class PositionableAdapter : NotifierListener<Target>, IObjectNotifier<IPositionable>
    {
        public PositionableAdapter(IObjectNotifier<Target> targetNotifier)
            : base(targetNotifier)
        {
        }

        public event Action<IPositionable> Notified;

        protected override void OnNotified(Target target)
        {
            Notified?.Invoke(target);
        }
    }
}