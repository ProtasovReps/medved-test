using System;
using Interface;
using TargetSystem.Notifier;

namespace TargetSystem.Adapter
{
    public class PositionableAdapter : SelectionListener<Target>, ISelectionNotifier<IPositionable>
    {
        public PositionableAdapter(ISelectionNotifier<Target> targetNotifier)
            : base(targetNotifier)
        {
        }

        public event Action<IPositionable> Selected;
        public event Action<IPositionable> Unselected;

        protected override void OnSelected(Target target)
        {
            Selected?.Invoke(target);
        }

        protected override void OnUnselected(Target target)
        {
            Unselected?.Invoke(target);
        }
    }
}