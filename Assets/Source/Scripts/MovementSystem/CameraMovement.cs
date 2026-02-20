using System;
using Interface;
using UnityEngine;

namespace MovementSystem
{
    public class CameraMovement : IDisposable
    {
        private readonly Transform _camera;
        private readonly IObjectNotifier<Vector2> _input;
        private readonly float _speed;
        
        public CameraMovement(IObjectNotifier<Vector2> input, float speed)
        {
            if (speed <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(speed));
            }
            
            _camera = Camera.main.transform;
            _input = input;
            _speed = speed;

            _input.Notified += Translate;
        }

        public void Dispose()
        {
            _input.Notified -= Translate;
        }

        private void Translate(Vector2 direction)
        {
            Vector3 projectedDirection = new(direction.x, 0, direction.y);
            _camera.transform.position += projectedDirection * _speed * Time.deltaTime;
        }
    }
}