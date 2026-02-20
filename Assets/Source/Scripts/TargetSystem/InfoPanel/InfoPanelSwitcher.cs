using System.Collections.Generic;
using Interface;
using TargetSystem.Notifier;
using UI;

namespace TargetSystem.InfoPanel
{
    public class InfoPanelSwitcher : NotifierListener<Target>
    {
        private readonly InfoPanelPool _panelPool;
        private readonly Dictionary<IInformationalTarget, SwitchablePanel> _settedTargets;

        public InfoPanelSwitcher(IObjectNotifier<Target> notifier, InfoPanelPool panelPool)
            : base(notifier)
        {
            _settedTargets = new Dictionary<IInformationalTarget, SwitchablePanel>();

            _panelPool = panelPool;
        }

        protected override void OnNotified(Target notifieable)
        {
            if (_settedTargets.TryGetValue(notifieable, out SwitchablePanel panel))
            {
                Hide(notifieable, panel);
                return;
            }

            Show(notifieable);
        }

        private void Hide(Target target, SwitchablePanel panel)
        {
            panel.InfoPanel.Cancel();

            _panelPool.Release(panel);
            _settedTargets.Remove(target);
        }

        private void Show(Target target)
        {
            SwitchablePanel newPanel = _panelPool.Get();

            newPanel.InfoPanel.Set(target);
            newPanel.ExitButton.Set(target);

            _settedTargets.Add(target, newPanel);
        }
    }
}