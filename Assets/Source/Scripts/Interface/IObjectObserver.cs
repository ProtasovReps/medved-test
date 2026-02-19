using System;

namespace Interface
{
    public interface IObjectObserver<T>
    {
        public event Action<T> Notifying;
    }
}