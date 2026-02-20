using System.Collections.Generic;
using Interface;
using TargetSystem.Notifier;
using UI;

namespace TargetSystem.InfoPanel
{
    public class InfoPanelSwitcher : SelectionListener<Target>
    {
        private readonly InfoPanelPool _panelPool;
        private readonly Dictionary<Target, SwitchablePanel> _settedTargets;

        public InfoPanelSwitcher(ISelectionNotifier<Target> notifier, InfoPanelPool panelPool)
            : base(notifier)
        {
            _settedTargets = new Dictionary<Target, SwitchablePanel>();

            _panelPool = panelPool;
        }

        protected override void OnSelected(Target notifiable)
        {
            SwitchablePanel newPanel = _panelPool.Get();

            newPanel.Panel.Set(notifiable);
            newPanel.ExitButton.Set(notifiable);
            
            _settedTargets.Add(notifiable, newPanel);
        }

        protected override void OnUnselected(Target target)
        {
            SwitchablePanel panel = _settedTargets[target];

            panel.Panel.Cancel();
            _panelPool.Release(panel);
            _settedTargets.Remove(target);
        }
    }
}