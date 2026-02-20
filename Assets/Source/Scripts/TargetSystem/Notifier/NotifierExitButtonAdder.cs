using System;
using Interface;
using UI;

namespace TargetSystem.Notifier
{
    public class NotifierExitButtonAdder : IDisposable
    {
        private readonly IPanelCreator _creator;
        private readonly SelectionNotifier _notifier;

        public NotifierExitButtonAdder(IPanelCreator creator, SelectionNotifier notifier)
        {
            _creator = creator;
            _notifier = notifier;

            _creator.Created += OnCreated;
        }

        public void Dispose()
        {
            _creator.Created -= OnCreated;
        }

        private void OnCreated(SwitchablePanel panel)
        {
            _notifier.Subscribe(panel.ExitButton);
        }
    }
}