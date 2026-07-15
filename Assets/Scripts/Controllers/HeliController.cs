using System.Collections.Generic;
using System.Linq;
using Engines;
using InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Controllers
{
    [RequireComponent(typeof(Rigidbody), typeof(BaseHeliInput))]
    public class HeliController : BaseRbController
    {
        private BaseHeliInput _baseHeliInput;
        private RotorController _rotorController;
        [SerializeField] private List<MainHeliEngine> engines;

        private void Start()
        {
            _baseHeliInput = GetComponent<BaseHeliInput>();
            _rotorController = GetComponentInChildren<RotorController>();
        }
        
        protected override void HandlePhysics()
        {
            HandleEngines();
            HandleRotors();
            HandleCharacteristics();
        }

        // тут доробити, на випадок якщо декілька двигунів
        private void HandleRotors()
        {
            _rotorController.UpdateRotor(_baseHeliInput, engines[0].CurrentRpm);
        }

        protected virtual void HandleEngines()
        {
            engines.ForEach(e => e.UpdateEngine(_baseHeliInput.ThrottleInput));
        }

        protected virtual void HandleCharacteristics()
        {
            
        }
    }
}