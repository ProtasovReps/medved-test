using System.Collections.Generic;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TargetSystem.Info
{
    public class InfoPanelPool
    {
        private readonly Camera _camera;
        private readonly InfoPanel _prefab;
        private readonly Queue<InfoPanel> _freePanels = new ();
        
        public InfoPanelPool(InfoPanel prefab)
        {
            _prefab = prefab;
            _camera = Camera.main;
        }
       
        public InfoPanel Get()
        {
            if (_freePanels.Count == 0)
            {
                Create();
            }

            InfoPanel panel = _freePanels.Dequeue();
            
            panel.gameObject.SetActive(true);
            return panel;
        }
        
        public void Release(InfoPanel infoPanel)
        {
            infoPanel.Cancel();
            infoPanel.gameObject.SetActive(false);
            _freePanels.Enqueue(infoPanel);
        }
        
        private void Create()
        {
            InfoPanel panel = Object.Instantiate(_prefab);
            Canvas canvas = panel.GetComponent<Canvas>();

            canvas.worldCamera = _camera;
            
            _freePanels.Enqueue(panel);
        }
    }
}