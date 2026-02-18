using UnityEngine;

namespace Interface
{
    public interface IInformationalTarget
    {
        public string Name { get; }
        public Vector3 Position { get; }
    }
}