using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class BaseHeliInput : MonoBehaviour
    {
        // Доделать Sensitivity, занадто швидко йде перемикання між одниницею та нулем
        [SerializeField] private float delayInput = 4; // зараз дуже швидко йде перемикання 
        private InputSystem_Actions _inputSystemActions;
        
        // throttleInput - ручка газу / Керування потужністю двигуна
        private float _throttleInputFromInput;
        public float ThrottleInput { get; private set; } = 0f;
        
        private bool _isHoldingThrottleInput = false;
        
        // collective - загальний кут установки лопатей несного гвинта, керування висотою
        public float CollectiveInput { get; private set; } = 0f;

        // W/S Нахиляють ніс вертольота вниз або вгору (рух або гальма). - Pitch
        // A/D Нахил в ліво або право - Roll
        // collective - наклони вперед та назад
        public Vector2 CyclicInput { get; private set; } = Vector2.zero;

        // pedalInput - поворот ліво чи право, хвіст // Yaw в ТЗ
        public float PedalInput { get; private set; } = 0f;
        
        private void OnEnable()
        {
            _inputSystemActions = new InputSystem_Actions();
            _inputSystemActions.Enable();
            _inputSystemActions.Heli.CyclicInput.performed += OnCyclicPerformed;
            _inputSystemActions.Heli.CyclicInput.canceled += OnCyclicCanceled;
            _inputSystemActions.Heli.ThrottleInput.performed += OnThrottleInputPerformed;
            _inputSystemActions.Heli.ThrottleInput.canceled += OnThrottleInputCanceled;
            _inputSystemActions.Heli.CollectiveInput.performed += OnCollectiveInputPerformed;
            _inputSystemActions.Heli.CollectiveInput.canceled += OnCollectiveInputCanceled;
            _inputSystemActions.Heli.PedalInput.performed += OnPedalInputPerformed;
            _inputSystemActions.Heli.PedalInput.canceled += OnPedalInputCanceled;
        }

        private void Update() => StickyThrottleInput();
        
        // метод залипання швидкості центрального двигуна, тут плавне нарощування швидкості із залипанням
        private void StickyThrottleInput()
        {
            if (_isHoldingThrottleInput)
            {
                ThrottleInput += Time.deltaTime * delayInput * _throttleInputFromInput;
            }
            
            ThrottleInput = Mathf.Clamp01(ThrottleInput);
            //Debug.Log(ThrottleInput);
        }
        
        private void OnCyclicPerformed(InputAction.CallbackContext context) 
            => CyclicInput = context.ReadValue<Vector2>();
        private void OnCyclicCanceled(InputAction.CallbackContext context) 
            => CyclicInput = Vector2.zero;
        private void OnThrottleInputPerformed(InputAction.CallbackContext context)
        {
            _isHoldingThrottleInput = true;
            _throttleInputFromInput = context.ReadValue<float>();
            
        }
        private void OnThrottleInputCanceled(InputAction.CallbackContext context) => _isHoldingThrottleInput = false;
        private void OnCollectiveInputPerformed(InputAction.CallbackContext context) 
            => CollectiveInput = context.ReadValue<float>();
        private void OnCollectiveInputCanceled(InputAction.CallbackContext context) 
            => CollectiveInput = 0f;
        private void OnPedalInputPerformed(InputAction.CallbackContext context) 
            => PedalInput = context.ReadValue<float>();
        private void OnPedalInputCanceled(InputAction.CallbackContext context) 
            => PedalInput = 0f;

        private void OnDisable()
        {
            _inputSystemActions.Heli.CyclicInput.performed -= OnCyclicPerformed;
            _inputSystemActions.Heli.CyclicInput.canceled -= OnCyclicCanceled;
            _inputSystemActions.Heli.ThrottleInput.performed -= OnThrottleInputPerformed;
            _inputSystemActions.Heli.ThrottleInput.canceled -= OnThrottleInputCanceled;
            _inputSystemActions.Heli.CollectiveInput.performed -= OnCollectiveInputPerformed;
            _inputSystemActions.Heli.CollectiveInput.canceled -= OnCollectiveInputCanceled;
            _inputSystemActions.Heli.PedalInput.performed -= OnPedalInputPerformed;
            _inputSystemActions.Heli.PedalInput.canceled -= OnPedalInputCanceled;
            _inputSystemActions.Disable();
        }
    }
}
