using InputSystem;

namespace Controllers
{
    public class HeliController : BaseRbController
    {
        private BaseHeliInput _baseHeliInput;

        private void Start()
        {
            _baseHeliInput = GetComponent<BaseHeliInput>();
        }
        
        protected override void HandlePhysics()
        {
            HandleEngines();
            HandleCharacteristics();
        }

        protected virtual void HandleCharacteristics()
        {
            
        }

        protected virtual void HandleEngines()
        {
            
        }
    }
}