using System;

namespace Interface
{
    public interface IObjectNotifier<out T>
    {
        public event Action<T> Notified;
    }
}