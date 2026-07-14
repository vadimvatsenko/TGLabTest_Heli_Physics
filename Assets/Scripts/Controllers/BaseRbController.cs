using UnityEngine;

// базовий клас Контролер твердого тіла
namespace Controllers
{
    public class BaseRbController : MonoBehaviour
    {
        protected Rigidbody Rb;
        void Start() => Rb = GetComponent<Rigidbody>();
        
        private void FixedUpdate()
        {
            HandlePhysics();
        }

        protected void HandlePhysics() { }
    }
}
