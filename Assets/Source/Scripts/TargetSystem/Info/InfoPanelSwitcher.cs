using System;
using System.Collections.Generic;
using Interface;
using UI;

namespace TargetSystem.Info
{
    public class InfoPanelSwitcher : ITargetManager, IDisposable
    {
        private readonly IObjectObserver<Target> _targetObserver;
        private readonly InfoPanelPool _panelPool;
        private readonly InfoPanelDatabase _database;
        private readonly List<TargetInfoPanel> _subscriptions;

        public InfoPanelSwitcher(
            IObjectObserver<Target> targetObserver,
            InfoPanelPool panelPool,
            InfoPanelDatabase database)
        {
            _subscriptions = new List<TargetInfoPanel>();

            _targetObserver = targetObserver;
            _panelPool = panelPool;
            _database = database;

            _targetObserver.Notifying += SwitchPanel;
        }

        public event Action<Target> Added;
        public event Action<Target> Removed;
        
        public void Dispose()
        {
            for (int i = 0; i < _subscriptions.Count; i++)
            {
                _subscriptions[i].ExitPressed -= SwitchPanel;
            }

            _targetObserver.Notifying -= SwitchPanel;
        }

        private void SwitchPanel(Target target)
        {
            if (_database.TryGetPanel(target, out TargetInfoPanel panel))
            {
                RemovePanel(target, panel);
                return;
            }

            CreatePanel(target);
        }

        private void RemovePanel(Target target, TargetInfoPanel panel)
        {
            panel.ExitPressed -= SwitchPanel;

            target.Outliner.Disable();;
            _subscriptions.Remove(panel);
            _panelPool.Release(panel);
            _database.Remove(target);
            
            Removed?.Invoke(target);
        }

        private void CreatePanel(Target target)
        {
            TargetInfoPanel newPanel = _panelPool.Get();
            newPanel.ExitPressed += SwitchPanel;

            target.Outliner.Enable();
            _subscriptions.Add(newPanel);
            newPanel.Set(target);
            _database.Set(target, newPanel);
            
            Added?.Invoke(target);
        }
    }
}