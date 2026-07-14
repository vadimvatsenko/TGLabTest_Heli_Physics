using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class BaseHeliInput : MonoBehaviour
    {
    
        // W/S Нахиляють ніс вертольота вниз або вгору (рух або гальма). - Pitch
        // A/D Нахил в ліво або право - Roll
        // throttleInput - ручка газу / Керування потужністю двигуна
        public float ThrottleInput { get; private set; } = 0f;

        // collective - загальний кут установки лопатей несного гвинта, керування висотою
        public float CollectiveInput { get; private set; } = 0f;

        public float CyclicInput { get; private set; } = 0f;

        // pedalInput - поворот ліво чи право, хвіст
        public float PedalInput { get; private set; } = 0f;

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
            
            ThrottleInput = inputVector.y;
            PedalInput = inputVector.x;
        }

        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            ThrottleInput = 0f;
            PedalInput = 0f;
        }

        private void OnDisable()
        {
            _inputSystemActions.Heli.Movement.performed -= OnMovementPerformed;
            _inputSystemActions.Heli.Movement.canceled -= OnMovementCanceled;
            _inputSystemActions.Disable();
        }
    
    }
}
