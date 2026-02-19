using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using TargetSystem.Info;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private TargetInfo _info;
        [SerializeField] private float _verticalOffeset;
        [SerializeField] private Transform _transform;
        [SerializeField] private Button _exitButton;
        
        private IInformationalTarget _target;
        private CancellationTokenSource _cancellationTokenSource;

        public event Action<IInformationalTarget> ExitPressed;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(InvokePressed);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(InvokePressed);
        }

        private void OnDestroy()
        {
            Cancel();
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
                _transform.position = new Vector3(_target.Position.x, 0, _target.Position.z + _verticalOffeset);
                
                _info.Update(_target.Name, _target.Position);
                await UniTask.Yield(_cancellationTokenSource.Token, true);
            }
        }

        private void InvokePressed()
        {
            ExitPressed?.Invoke(_target);
        }
    }
}