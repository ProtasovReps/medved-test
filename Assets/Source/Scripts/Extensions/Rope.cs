using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using TargetSystem;
using UnityEngine;

namespace Extensions
{
    public class Rope : IDisposable
    {
        private const int MinTargetCount = 2;
        
        private readonly List<Target> _targets;
        private readonly LineRenderer _lineRenderer;
        private readonly ITargetManager _targetManager;

        private CancellationTokenSource _cancellationTokenSource;
        
        public Rope(LineRenderer lineRenderer, ITargetManager targetManager)
        {
            _targets = new List<Target>();
            _lineRenderer = lineRenderer;
            _targetManager = targetManager;

            _targetManager.Added += OnAdded;
            _targetManager.Removed += OnRemoved;
        }

        public void Dispose()
        {
            _targetManager.Added -= OnAdded;
            _targetManager.Removed -= OnRemoved;
            
            Cancel();
            
            _cancellationTokenSource?.Dispose();
        }

        private void OnAdded(Target target)
        {
            _targets.Add(target);
            ManageRope();
        }

        private void OnRemoved(Target target)
        {
            _targets.Remove(target);
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