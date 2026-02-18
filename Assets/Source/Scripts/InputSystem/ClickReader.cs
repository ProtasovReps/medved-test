using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class ClickReader : IDisposable
    {
        private readonly InputActions _inputActions;
        
        public ClickReader(InputActions inputActions)
        {
            _inputActions = inputActions;
            
            _inputActions.Camera.Click.performed += OnClicked;
        }

        public event Action<Vector2> Clicked;
        
        public void Dispose() //
        {
            _inputActions.Camera.Click.performed -= OnClicked;
        }
        
        private void OnClicked(InputAction.CallbackContext context)
        {
            Vector2 mousePosition = _inputActions.Camera.MousePosition.ReadValue<Vector2>();
            Clicked?.Invoke(mousePosition);
        }
    }
}