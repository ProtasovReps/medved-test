using System.Collections.Generic;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TargetSystem.Info
{
    public class InfoPanelPool
    {
        private readonly Camera _camera;
        private readonly TargetInfoPanel _prefab;
        private readonly Queue<TargetInfoPanel> _freePanels = new ();
        
        public InfoPanelPool(TargetInfoPanel prefab)
        {
            _prefab = prefab;
            _camera = Camera.main;
        }
       
        public TargetInfoPanel Get()
        {
            if (_freePanels.Count == 0)
            {
                Create();
            }

            TargetInfoPanel panel = _freePanels.Dequeue();
            
            panel.gameObject.SetActive(true);
            return panel;
        }
        
        public void Release(TargetInfoPanel targetInfoPanel)
        {
            targetInfoPanel.Cancel();
            targetInfoPanel.gameObject.SetActive(false);
            _freePanels.Enqueue(targetInfoPanel);
        }
        
        private void Create()
        {
            TargetInfoPanel panel = Object.Instantiate(_prefab);
            Canvas canvas = panel.GetComponent<Canvas>();

            canvas.worldCamera = _camera;
            
            _freePanels.Enqueue(panel);
        }
    }
}