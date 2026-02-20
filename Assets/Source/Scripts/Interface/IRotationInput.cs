using System;

namespace Interface
{
    public interface IRotationInput
    {
        public event Action<int> Rotating;
        public event Action Stopped;
    }
}