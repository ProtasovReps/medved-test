using System;
using Interface;
using UI.Panel;

namespace TargetSystem.Notifier
{
    public class NotifierExitButtonAdder : IDisposable
    {
        private readonly INotifiablePool _creator;
        private readonly SelectionNotifier _selection;

        public NotifierExitButtonAdder(INotifiablePool creator, SelectionNotifier selection)
        {
            _creator = creator;
            _selection = selection;

            _creator.Created += OnCreated;
        }

        public void Dispose()
        {
            _creator.Created -= OnCreated;
        }

        private void OnCreated(SwitchablePanel panel)
        {
            _selection.AddSubscribtion(panel.ExitButton);
        }
    }
}