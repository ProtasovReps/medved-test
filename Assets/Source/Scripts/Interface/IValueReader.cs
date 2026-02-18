using System;

namespace Interface
{
    public interface IValueReader<T>
    {
        public event Action<T> ValueChanged;
    }
}