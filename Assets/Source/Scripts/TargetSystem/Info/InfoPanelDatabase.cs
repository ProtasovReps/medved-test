using System;
using System.Collections.Generic;
using UI;

namespace TargetSystem.Info
{
    public class InfoPanelDatabase
    {
        private readonly Dictionary<Target, TargetInfoPanel> _settedTargets = new ();

        public void Set(Target target, TargetInfoPanel newInfo)
        {
            if (TryGetInfo(target, out _))
            {
                throw new InvalidOperationException();
            }

            _settedTargets.Add(target, newInfo);
        }

        public void Remove(Target target)
        {
            if (TryGetInfo(target, out _) == false)
            {
                throw new InvalidOperationException();
            }

            _settedTargets.Remove(target);
        }

        public bool TryGetInfo(Target target, out TargetInfoPanel panel)
        {
            return _settedTargets.TryGetValue(target, out panel);
        }
    }
}