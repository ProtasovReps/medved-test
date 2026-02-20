using System;
using Interface;
using TargetSystem.Notifier;

namespace TargetSystem.Adapter
{
    public class InformationalAdapter : NotifierListener<Target>, IObjectNotifier<IInformationalTarget>
    {
        public InformationalAdapter(IObjectNotifier<Target> targetNotifier)
            : base(targetNotifier)
        {
        }

        public event Action<IInformationalTarget> Notified;
        
        protected override void OnNotified(Target target)
        {
            Notified?.Invoke(target);
        }
    }
}