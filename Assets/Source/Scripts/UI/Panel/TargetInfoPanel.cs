using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using TargetSystem.InfoPanel;
using UnityEngine;

namespace UI.Panel
{
    public class TargetInfoPanel : MonoBehaviour
    {
        [SerializeField] private TargetInfo _info;
        [SerializeField] private float _upOffset;
        
        private IInformationalTarget _target;
        private CancellationTokenSource _cancellationTokenSource;
        private Camera _camera;
        private Transform _transform;
        
        private void OnDestroy()
        {
            Cancel();
        }

        public void Initialize(Camera camera)
        {
            _camera = camera;
            _transform = transform;
        }
        
        public void Set(IInformationalTarget target)
        {
            if (_target != null)
            {
                throw new InvalidOperationException();
            }

            _target = target;
            _cancellationTokenSource = new CancellationTokenSource();

            UpdateInfo().Forget();
        }

        public void Cancel()
        { 
            _target = null;
            _cancellationTokenSource?.Cancel();
        }

        private async UniTaskVoid UpdateInfo()
        {
            while (_cancellationTokenSource?.IsCancellationRequested == false)
            {
                _transform.position = new Vector3(_target.Position.x, _target.Position.y + _upOffset, _target.Position.z);
                _transform.rotation = _camera.transform.rotation;
                
                _info.Update(_target);
                await UniTask.Yield(_cancellationTokenSource.Token, true);
            }
        }
    }
}