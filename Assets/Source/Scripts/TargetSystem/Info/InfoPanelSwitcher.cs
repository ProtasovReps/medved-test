using System;
using Interface;
using UI;

namespace TargetSystem.Info
{
    public class InfoPanelSwitcher : IDisposable
    {
        private readonly IObjectObserver<Target> _targetObserver;
        private readonly InfoPanelPool _panelPool;
        private readonly InfoPanelDatabase _database;
        
        public InfoPanelSwitcher(
            IObjectObserver<Target> targetObserver,
            InfoPanelPool panelPool,
            InfoPanelDatabase database)
        {
            _targetObserver = targetObserver;
            _panelPool = panelPool;
            _database = database;

            _targetObserver.Notifying += SwitchPanel;
        }

        public void Dispose()
        {
            _targetObserver.Notifying -= SwitchPanel;
        }
        
        private void SwitchPanel(Target target)
        {
            if (_database.TryGetInfo(target, out TargetInfoPanel panel))
            {
                _panelPool.Release(panel);
                _database.Remove(target);
                return;
            }

            TargetInfoPanel newPanel = _panelPool.Get();
            
            newPanel.Set(target);
            _database.Set(target, newPanel);
        }
    }
}