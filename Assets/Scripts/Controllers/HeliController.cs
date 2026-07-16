using System.Collections.Generic;
using Characteristics;
using Engines;
using InputSystem;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Rigidbody), typeof(BaseHeliInput))]
    public class HeliController : BaseRbController
    {
        [SerializeField] private List<MainHeliEngine> engines;
        private BaseHeliInput _baseHeliInput;
        private RotorController _rotorController;
        private BaseCharacterictics _baseCharacterictics;

        private void Start()
        {
            _baseHeliInput = GetComponent<BaseHeliInput>();
            _baseCharacterictics = GetComponent<BaseCharacterictics>();
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
            foreach (var engine in engines)
            {
                _rotorController.UpdateRotor(_baseHeliInput, engine.CurrentRpm);
            }
        }

        protected virtual void HandleEngines()
        {
            engines.ForEach(e => e.UpdateEngine(_baseHeliInput.ThrottleInput));
        }

        protected virtual void HandleCharacteristics()
        {
            _baseCharacterictics.UpdateCharacterictics();
        }
    }
}