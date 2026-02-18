using System;
using InputSystem;
using TargetSystem;
using UnityEngine;

namespace Extensions
{
    public class TargetRaycaster : IDisposable
    {
        private const float MaxRaycastDistance = 100f;
            
        private readonly RaycastHit[] _results;
        private readonly Camera _camera;
        private readonly ClickReader _clickReader;
        private readonly int _mask;

        public TargetRaycaster(ClickReader clickReader, LayerMask mask)
        {
            _results = new RaycastHit[1];
            _camera = Camera.main;
            
            _clickReader = clickReader;
            _mask = mask;
            
            _clickReader.Clicked += OnClicked;
        }

        public event Action<Target> TargetClicked;
        
        public void Dispose() //
        {
            _clickReader.Clicked -= OnClicked;
        }
        
        private void OnClicked(Vector2 mousePosition)
        {
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            int hits = Physics.RaycastNonAlloc(ray, _results, MaxRaycastDistance, _mask);

            if (hits == 0)
            {
                return;
            }

            if (_results[0].transform.TryGetComponent(out Target target) == false)
            {
                return;
            }

            TargetClicked?.Invoke(target);
        }
    }
}