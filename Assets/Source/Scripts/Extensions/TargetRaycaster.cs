using System;
using Interface;
using TargetSystem;
using UnityEngine;

namespace Extensions
{
    public class TargetRaycaster : IObjectNotifier<Target>, IDisposable 
    {
        private const float MaxRaycastDistance = 100f;

        private readonly RaycastHit[] _results;
        private readonly Camera _camera;
        private readonly IObjectNotifier<Vector2> _click;
        private readonly int _mask;

        public TargetRaycaster(IObjectNotifier<Vector2> click, LayerMask mask)
        {
            _results = new RaycastHit[1];
            _camera = Camera.main;

            _click = click;
            _mask = mask;

            _click.Notified += OnClicked;
        }

        public event Action<Target> Notified;

        public void Dispose()
        {
            _click.Notified -= OnClicked;
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

            Notified?.Invoke(target);
        }
    }
}