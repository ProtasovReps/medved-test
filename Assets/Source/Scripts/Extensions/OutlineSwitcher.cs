using System.Collections.Generic;
using Interface;
using TargetSystem.Notifier;

namespace Extensions
{
    public class OutlineSwitcher : NotifierListener<Outline>
    {
        private readonly HashSet<Outline> _outlines;

        public OutlineSwitcher(IObjectNotifier<Outline> notifier)
            : base(notifier)
        {
            _outlines = new HashSet<Outline>();
        }

        protected override void OnNotified(Outline outline)
        {
            if (_outlines.Contains(outline))
            {
                outline.Disable();
                _outlines.Remove(outline);
            }
            else
            {
                outline.Enable();
                _outlines.Add(outline);
            }
        }
    }
}