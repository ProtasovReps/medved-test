using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions;
using UnityEngine;

namespace MovementSystem
{
    public class CycledMovement : IDisposable
    {
        private readonly Transform _figure;
        private readonly Vector3[] _targets;
        private readonly float _speed;
        private readonly CancellationTokenSource _tokenSource;

        private int _currentIndex;

        public CycledMovement(Transform figure, Path path, float speed)
        {
            Vector3[] targets = path.PathPoints.ToArray();

            if (targets.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(targets));
            }

            _tokenSource = new CancellationTokenSource();

            _figure = figure;
            _targets = targets;
            _speed = speed;

            MoveCycled().Forget();
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        private async UniTaskVoid MoveCycled()
        {
            while (_tokenSource.IsCancellationRequested == false)
            {
                if (_figure.position == _targets[_currentIndex])
                {
                    _currentIndex = _targets.GetCycledIndex(_currentIndex);
                }

                _figure.position = Vector3.MoveTowards(
                    _figure.position,
                    _targets[_currentIndex],
                    _speed * Time.deltaTime);

                _figure.LookAt(_targets[_currentIndex]);
                await UniTask.Yield();
            }
        }
    }
}