using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class BaseHeliInput : MonoBehaviour
    {
    
        // W/S Нахиляють ніс вертольота вниз або вгору (рух або гальма). - Pitch
        private float vertical = 0f;
    
        // A/D Нахил в ліво або право - Roll
        private float horizontal = 0f;
    
        // throttleInput - ручка газу / Керування потужністю двигуна
        private float _throttleInput = 0f;
    
        // collective - загальний кут установки лопатей несного гвинта, керування висотою
        private float _collectiveInput = 0f;
    
        private float _cyclicInput = 0f;
        // pedalInput - поворот ліво чи право, хвіст
        private float _pedalInput = 0f;
    
        private InputSystem_Actions _inputSystemActions;
    
        private void OnEnable()
        {
            _inputSystemActions = new InputSystem_Actions();
            _inputSystemActions.Enable();
            _inputSystemActions.Heli.Movement.performed += OnMovementPerformed;
            _inputSystemActions.Heli.Movement.canceled += OnMovementCanceled;
        }

        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
        
            horizontal = inputVector.x;
            vertical = inputVector.y;
            Debug.Log(horizontal + " " + vertical);
            
            _throttleInput = vertical;
            _pedalInput = horizontal;
            
        }

        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            horizontal = 0f;
            vertical = 0f;
        }

        private void OnDisable()
        {
            _inputSystemActions.Heli.Movement.performed -= OnMovementPerformed;
            _inputSystemActions.Heli.Movement.canceled -= OnMovementCanceled;
            _inputSystemActions.Disable();
        }
    
    }
}
