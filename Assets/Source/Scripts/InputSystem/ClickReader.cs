using System;
using Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class ClickReader : IDisposable, IObjectObserver<Vector2>
    {
        private readonly InputActions _inputActions;
        
        public ClickReader(InputActions inputActions)
        {
            _inputActions = inputActions;
            
            _inputActions.Camera.Click.performed += OnClicked;
        }

        public event Action<Vector2> Notifying;
        
        public void Dispose()
        {
            _inputActions.Camera.Click.performed -= OnClicked;
        }
        
        private void OnClicked(InputAction.CallbackContext context)
        {
            Vector2 mousePosition = _inputActions.Camera.MousePosition.ReadValue<Vector2>();
            Notifying?.Invoke(mousePosition);
        }
    }
}