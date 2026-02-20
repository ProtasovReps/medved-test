using System;
using Extensions;
using Interface;
using UnityEngine;

namespace TargetSystem
{
    [RequireComponent(typeof(Outline))]
    public class Target : MonoBehaviour, IInformationalTarget
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