using System;
using System.Collections.Generic;
using Interface;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TargetSystem.InfoPanel
{
    public class InfoPanelPool : INotifiablePool
    {
        private readonly Camera _camera;
        private readonly SwitchablePanel _prefab;
        private readonly Queue<SwitchablePanel> _freePanels = new ();
        
        public InfoPanelPool(SwitchablePanel prefab, Camera camera)
        {
            _prefab = prefab;
            _camera = camera;
        }

        public event Action<SwitchablePanel> Created;
        
        public SwitchablePanel Get()
        {
            if (_freePanels.Count == 0)
            {
                Create();
            }

            SwitchablePanel panel = _freePanels.Dequeue();
            
            panel.gameObject.SetActive(true);
            return panel;
        }
        
        public void Release(SwitchablePanel targetInfoPanel)
        {
            targetInfoPanel.gameObject.SetActive(false);
            _freePanels.Enqueue(targetInfoPanel);
        }
        
        private void Create()
        {
            SwitchablePanel panel = Object.Instantiate(_prefab);
            Canvas canvas = panel.GetComponent<Canvas>();

            canvas.worldCamera = _camera;
            
            _freePanels.Enqueue(panel);
            Created?.Invoke(panel);
        }
    }
}