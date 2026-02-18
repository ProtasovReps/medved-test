using System;
using InputSystem;
using Interface;
using UnityEngine;

namespace MovementSystem
{
    public class CameraMovement : IDisposable
    {
        private readonly Transform _camera;
        private readonly IValueReader<Vector2> _inputReader;
        private readonly float _speed;
        
        public CameraMovement(IValueReader<Vector2> moveInputReader, float speed)
        {
            if (speed <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(speed));
            }
            
            _camera = Camera.main.transform;
            _inputReader = moveInputReader;
            _speed = speed;

            _inputReader.ValueChanged += Translate;
        }

        public void Dispose()
        {
            _inputReader.ValueChanged -= Translate;
        }

        private void Translate(Vector2 direction)
        {
            Vector3 projectedDirection = new(direction.x, 0, direction.y);
            _camera.transform.position += projectedDirection * _speed * Time.deltaTime;
        }
    }
}