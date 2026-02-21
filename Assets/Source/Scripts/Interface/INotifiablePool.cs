using System;
using UI.Panel;

namespace Interface
{
    public interface INotifiablePool
    {
        public event Action<SwitchablePanel> Created;
    }
}