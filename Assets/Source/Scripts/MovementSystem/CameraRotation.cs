using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using UnityEngine;

namespace MovementSystem
{
    public class CameraRotation : IDisposable
    {
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly IRotationInput[] _inputs;
        
        private CancellationTokenSource _source;
        
        public CameraRotation(IRotationInput[] rotationPanels, float speed, Camera camera)
        {
            if (speed <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speed));
            }
            
            _inputs = rotationPanels;
            _speed = speed;
            _transform = camera.transform;
            
            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i].Rotating += OnRotating;
                _inputs[i].Stopped += OnStopped;
            }
        }
        
        public void Dispose()
        {
            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i].Rotating -= OnRotating;
                _inputs[i].Stopped -= OnStopped;
            }
            
            Cancel();
            
            _source?.Dispose();
        }

        private void OnRotating(int side)
        {
            _source = new CancellationTokenSource();
            
            Rotate(side).Forget();
        }

        private void OnStopped()
        {
            Cancel();
        }
        
        private async UniTaskVoid Rotate(int side)
        {
            while (_source.IsCancellationRequested == false)
            {
                float direction = side * Time.deltaTime * _speed;
                
                _transform.Rotate(_transform.forward * direction, Space.World);
                await UniTask.Yield(_source.Token, true);
            }
        }

        private void Cancel()
        {
            _source?.Cancel();
        }
    }
}