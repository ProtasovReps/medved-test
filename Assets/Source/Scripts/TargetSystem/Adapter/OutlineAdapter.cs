using System;
using System.Collections.Generic;
using Extensions;
using Interface;
using TargetSystem.Notifier;

namespace TargetSystem.Adapter
{
    public class OutlineAdapter : NotifierListener<Target>, IObjectNotifier<Outline>
    {
        private readonly Dictionary<Target, Outline> _targetOutlines;
        
        public OutlineAdapter(IObjectNotifier<Target> targetNotifier)
            : base(targetNotifier)
        {
            _targetOutlines = new Dictionary<Target, Outline>();
        }
        
        public event Action<Outline> Notified;

        protected override void OnNotified(Target target)
        {
            Outline outline;
            
            if (_targetOutlines.ContainsKey(target) == false)
            {
                outline = target.GetComponent<Outline>();
                _targetOutlines.Add(target, outline);
            }
            else
            {
                outline = _targetOutlines[target];
            }
            
            Notified?.Invoke(outline);            
        }
    }
}