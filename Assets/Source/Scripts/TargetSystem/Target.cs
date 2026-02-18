using System;
using UnityEngine;

namespace TargetSystem
{
    public class Target : MonoBehaviour
    {
        private Transform _transform;
        
        public Vector3 Position => _transform.position;
        public string Name { get; private set; }
        
        public void Initialize(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            _transform = transform;
        }        
    }
}