using System;
using UI;

namespace Interface
{
    public interface IPanelCreator
    {
        public event Action<SwitchablePanel> Created;
    }
}