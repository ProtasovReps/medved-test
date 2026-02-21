using System;
using System.Collections.Generic;
using Extensions;
using Interface;
using TargetSystem.Notifier;

namespace TargetSystem.Adapter
{
    public class OutlineAdapter : SelectionListener<Target>, ISelectionNotifier<Outline>
    {
        private readonly Dictionary<Target, Outline> _targetOutlines;

        public OutlineAdapter(ISelectionNotifier<Target> targetNotifier)
            : base(targetNotifier)
        {
            _targetOutlines = new Dictionary<Target, Outline>();
        }

        public event Action<Outline> Selected;
        public event Action<Outline> Unselected;

        protected override void OnSelected(Target target)
        {
            if (_targetOutlines.TryGetValue(target, out Outline outline) == false)
            {
                outline = target.GetComponent<Outline>();
                _targetOutlines.Add(target, outline);
            }

            Selected?.Invoke(outline);
        }

        protected override void OnUnselected(Target target)
        {
            Unselected?.Invoke(_targetOutlines[target]);
        }
    }
}