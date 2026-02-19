using System;
using System.Collections.Generic;
using Interface;
using UI;

namespace TargetSystem.Info
{
    public class InfoPanelSwitcher : IDisposable
    {
        private readonly IObjectObserver<IInformationalTarget> _targetObserver;
        private readonly InfoPanelPool _panelPool;
        private readonly InfoPanelDatabase _database;
        private readonly List<InfoPanel> _subscriptions;

        public InfoPanelSwitcher(
            IObjectObserver<IInformationalTarget> targetObserver,
            InfoPanelPool panelPool,
            InfoPanelDatabase database)
        {
            _subscriptions = new List<InfoPanel>();

            _targetObserver = targetObserver;
            _panelPool = panelPool;
            _database = database;

            _targetObserver.Notifying += SwitchPanel;
        }

        public void Dispose()
        {
            for (int i = 0; i < _subscriptions.Count; i++)
            {
                _subscriptions[i].ExitPressed -= SwitchPanel;
            }

            _targetObserver.Notifying -= SwitchPanel;
        }

        private void SwitchPanel(IInformationalTarget target)
        {
            if (_database.TryGet(target, out InfoPanel panel))
            {
                RemovePanel(target, panel);
                return;
            }

            CreatePanel(target);
        }

        private void RemovePanel(IInformationalTarget target, InfoPanel panel)
        {
            panel.ExitPressed -= SwitchPanel;

            _subscriptions.Remove(panel);
            _panelPool.Release(panel);
            _database.Remove(target);
        }

        private void CreatePanel(IInformationalTarget target)
        {
            InfoPanel newPanel = _panelPool.Get();
            newPanel.ExitPressed += SwitchPanel;

            _subscriptions.Add(newPanel);
            newPanel.Set(target);
            _database.Set(target, newPanel);
        }
    }
}