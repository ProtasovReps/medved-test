using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public class TargetInfoPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _position;

        private IInformationalTarget _target;
        private CancellationTokenSource _cancellationTokenSource;

        private void OnDestroy()
        {
            Release();
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

        public void Release()
        {
            _target = null;
            _cancellationTokenSource?.Cancel();
        }

        private async UniTaskVoid UpdateInfo()
        {
            while (_cancellationTokenSource?.IsCancellationRequested == false)
            {
                _name.text = _target.Name;
                _position.text = _target.Position.ToString();
                await UniTask.Yield(_cancellationTokenSource.Token, true);
            }
        }
    }
}