using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using TargetSystem.Notifier;
using UnityEngine;

namespace Extensions
{
    public class Rope : SelectionListener<IPositionable>
    {
        private const int MinTargetCount = 2;

        private readonly List<IPositionable> _targets;
        private readonly LineRenderer _lineRenderer;

        private CancellationTokenSource _cancellationTokenSource;

        public Rope(LineRenderer lineRenderer, ISelectionNotifier<IPositionable> notifier)
            : base(notifier)
        {
            _targets = new List<IPositionable>();
            _lineRenderer = lineRenderer;
        }

        public override void Dispose()
        {
            base.Dispose();

            Cancel();
            _cancellationTokenSource?.Dispose();
        }

        protected override void OnSelected(IPositionable positionable)
        {
            _targets.Add(positionable);

            ManageRope();
        }

        protected override void OnUnselected(IPositionable positionable)
        {
            _targets.Remove(positionable);
            
            ManageRope();
        }

        private void ManageRope()
        {
            Cancel();

            if (_targets.Count < MinTargetCount)
            {
                _lineRenderer.positionCount = 0;
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();

            Connect().Forget();
        }

        private async UniTaskVoid Connect()
        {
            while (_cancellationTokenSource.IsCancellationRequested == false)
            {
                _lineRenderer.positionCount = _targets.Count;

                for (int i = 0; i < _targets.Count; i++)
                {
                    _lineRenderer.SetPosition(i, _targets[i].Position);
                }

                await UniTask.Yield(_cancellationTokenSource.Token, true);
            }
        }

        private void Cancel()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}