using System;
using Interface;
using TargetSystem;
using UnityEngine;

namespace Extensions
{
    public class TargetRaycaster : IObjectObserver<Target>, IDisposable 
    {
        private const float MaxRaycastDistance = 100f;

        private readonly RaycastHit[] _results;
        private readonly Camera _camera;
        private readonly IObjectObserver<Vector2> _clickObserver;
        private readonly int _mask;

        public TargetRaycaster(IObjectObserver<Vector2> clickObserver, LayerMask mask)
        {
            _results = new RaycastHit[1];
            _camera = Camera.main;

            _clickObserver = clickObserver;
            _mask = mask;

            _clickObserver.Notifying += OnClicked;
        }

        public event Action<Target> Notifying;

        public void Dispose()
        {
            _clickObserver.Notifying -= OnClicked;
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

            Notifying?.Invoke(target);
        }
    }
}