using System;
using Interface;
using TargetSystem;
using UnityEngine;

namespace Extensions
{
    public class TargetRaycaster : ObjectObserver<Vector2>, IObjectNotifier<Target>
    {
        private const float MaxRaycastDistance = 100f;

        private readonly RaycastHit[] _results;
        private readonly Camera _camera;
        private readonly int _mask;

        public TargetRaycaster(IObjectNotifier<Vector2> clickNotifier, LayerMask mask, Camera camera)
        : base (clickNotifier)
        {
            _results = new RaycastHit[1];

            _mask = mask;
            _camera = camera;
        }

        public event Action<Target> Notified;

        protected override void OnNotified(Vector2 mousePosition)
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