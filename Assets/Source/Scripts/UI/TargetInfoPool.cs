using System.Collections.Generic;
using UnityEngine;

namespace InputSystem.UI
{
    public class TargetInfoPool//Dispose
    {
        private readonly Camera _camera;
        private readonly TargetInfoPanel _prefab;
        private readonly Queue<TargetInfoPanel> _freePanels = new ();
        private readonly List<TargetInfoPanel> _panels = new ();
        
        public TargetInfoPool(TargetInfoPanel prefab)
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

            return _freePanels.Dequeue();
        }
        
        private void Create()
        {
            TargetInfoPanel panel = Object.Instantiate(_prefab);
            Canvas canvas = panel.GetComponent<Canvas>();

            canvas.worldCamera = _camera;
            
            _panels.Add(panel);
            _freePanels.Enqueue(panel);
        }

        private void Release(TargetInfoPanel targetInfoPanel)
        {
            targetInfoPanel.gameObject.SetActive(false);
            _freePanels.Enqueue(targetInfoPanel);
        }
    }
}