using System;
using Extensions;
using Interface;
using UnityEngine;

namespace TargetSystem
{
    public class Target : MonoBehaviour, IInformationalTarget
    {
        [SerializeField] private Outliner _outliner; // перенести
        
        private Transform _transform;
        
        public Vector3 Position => _transform.position;
        public Outliner Outliner => _outliner;
        public string Name { get; private set; }
        
        public void Initialize(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            _outliner.Initialize();
            Name = name;
            _transform = transform;
        }        
    }
}