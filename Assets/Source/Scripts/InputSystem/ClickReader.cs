using System;
using Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class ClickReader : IObjectNotifier<Vector2>, IDisposable
    {
        private readonly InputActions _inputActions;

        public ClickReader(InputActions inputActions)
        {
            _inputActions = inputActions;

            _inputActions.Camera.Click.performed += OnClicked;
        }

        public event Action<Vector2> Notified;

        public void Dispose()
        {
            _inputActions.Camera.Click.performed -= OnClicked;
        }

        private void OnClicked(InputAction.CallbackContext context)
        {
            Vector2 mousePosition = _inputActions.Camera.MousePosition.ReadValue<Vector2>();
            Notified?.Invoke(mousePosition);
        }
    }
}