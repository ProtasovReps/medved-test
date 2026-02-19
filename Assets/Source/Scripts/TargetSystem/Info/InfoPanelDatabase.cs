using System;
using System.Collections.Generic;
using Interface;
using UI;

namespace TargetSystem.Info
{
    public class InfoPanelDatabase
    {
        private readonly Dictionary<IInformationalTarget, TargetInfoPanel> _settedTargets = new ();

        public void Set(IInformationalTarget target, TargetInfoPanel newTargetInfo)
        {
            if (TryGet(target, out _))
            {
                throw new InvalidOperationException();
            }

            _settedTargets.Add(target, newTargetInfo);
        }

        public void Remove(IInformationalTarget target)
        {
            if (TryGet(target, out _) == false)
            {
                throw new InvalidOperationException();
            }

            _settedTargets.Remove(target);
        }

        public bool TryGet(IInformationalTarget target, out TargetInfoPanel panel)
        {
            return _settedTargets.TryGetValue(target, out panel);
        }
    }
}