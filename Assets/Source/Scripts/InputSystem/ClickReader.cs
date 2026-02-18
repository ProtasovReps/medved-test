using System;
using Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class ClickReader : IDisposable, IVectorReader
    {
        private readonly InputActions _inputActions;
        
        public ClickReader(InputActions inputActions)
        {
            _inputActions = inputActions;
            
            _inputActions.Camera.Click.performed += OnClicked;
        }

        public event Action<Vector2> ValueChanged;
        
        public void Dispose()
        {
            _inputActions.Camera.Click.performed -= OnClicked;
        }
        
        private void OnClicked(InputAction.CallbackContext context)
        {
            Debug.Log("Clicked");
            Vector2 mousePosition = _inputActions.Camera.MousePosition.ReadValue<Vector2>();
            ValueChanged?.Invoke(mousePosition);
        }
    }
}