using System;
using System.Collections.Generic;
using Interface;
using UI;

namespace TargetSystem.Info
{
    public class InfoPanelDatabase
    {
        private readonly Dictionary<IInformationalTarget, InfoPanel> _settedTargets = new ();

        public void Set(IInformationalTarget target, InfoPanel newInfo)
        {
            if (TryGet(target, out _))
            {
                throw new InvalidOperationException();
            }

            _settedTargets.Add(target, newInfo);
        }

        public void Remove(IInformationalTarget target)
        {
            if (TryGet(target, out _) == false)
            {
                throw new InvalidOperationException();
            }

            _settedTargets.Remove(target);
        }

        public bool TryGet(IInformationalTarget target, out InfoPanel panel)
        {
            return _settedTargets.TryGetValue(target, out panel);
        }
    }
}