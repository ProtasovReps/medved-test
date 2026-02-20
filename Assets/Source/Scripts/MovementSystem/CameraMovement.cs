using System;
using Extensions;
using Interface;
using UnityEngine;

namespace MovementSystem
{
    public class CameraMovement : ObjectObserver<Vector2>
    {
        private readonly Transform _camera;
        private readonly float _speed;

        public CameraMovement(IObjectNotifier<Vector2> input, float speed, Camera camera)
            : base(input)
        {
            if (speed <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(speed));
            }

            _camera = camera.transform;
            _speed = speed;
        }

        protected override void OnNotified(Vector2 direction)
        {
            _camera.transform.Translate(direction * _speed * Time.deltaTime, Space.Self);
        }
    }
}