using System;
using Interface;
using UnityEngine;

namespace MovementSystem
{
    public class CameraMovement : IDisposable
    {
        private readonly Transform _camera;
        private readonly IObjectObserver<Vector2> _inputObserver;
        private readonly float _speed;
        
        public CameraMovement(IObjectObserver<Vector2> inputObserver, float speed)
        {
            if (speed <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(speed));
            }
            
            _camera = Camera.main.transform;
            _inputObserver = inputObserver;
            _speed = speed;

            _inputObserver.Notifying += Translate;
        }

        public void Dispose()
        {
            _inputObserver.Notifying -= Translate;
        }

        private void Translate(Vector2 direction)
        {
            Vector3 projectedDirection = new(direction.x, 0, direction.y);
            _camera.transform.position += projectedDirection * _speed * Time.deltaTime;
        }
    }
}