using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using UnityEngine;

namespace InputSystem
{
    public class MoveInputReader : IVectorReader, IDisposable
    {
        private readonly InputActions _inputActions;
        private readonly CancellationTokenSource _tokenSource;

        public MoveInputReader(InputActions inputActions)
        {
            _inputActions = inputActions;
            _tokenSource = new CancellationTokenSource();
            
            Read().Forget();
        }

        public event Action<Vector2> ValueChanged;

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        private async UniTaskVoid Read()
        {
            while (_tokenSource.IsCancellationRequested == false)
            {
                ValueChanged?.Invoke(_inputActions.Camera.Move.ReadValue<Vector2>());
                await UniTask.Yield(_tokenSource.Token, true);
            }
        }
    }
}