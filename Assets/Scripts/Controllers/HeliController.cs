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
        [SerializeField] private List<MainHeliEngine> engines;

        private void Start()
        {
            _baseHeliInput = GetComponent<BaseHeliInput>();
        }
        
        protected override void HandlePhysics()
        {
            HandleEngines();
            HandleCharacteristics();
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