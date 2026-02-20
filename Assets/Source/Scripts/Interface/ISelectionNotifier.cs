using System;

namespace Interface
{
    public interface ISelectionNotifier<T>
    {
        public event Action<T> Selected;
        public event Action<T> Unselected;
    }
}