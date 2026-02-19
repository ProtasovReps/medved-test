using System;
using TargetSystem;

namespace Interface
{
    public interface ITargetManager
    {
        public event Action<Target> Added;
        public event Action<Target> Removed;
    }
}