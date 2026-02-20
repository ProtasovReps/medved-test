using System;
using UI;

namespace Interface
{
    public interface INotifiablePool
    {
        public event Action<SwitchablePanel> Created;
    }
}