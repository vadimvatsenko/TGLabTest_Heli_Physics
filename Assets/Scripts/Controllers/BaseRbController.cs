using UnityEngine;

// базовий клас Контролер твердого тіла
namespace Controllers
{
    public class BaseRbController : MonoBehaviour
    {
        // вага 
        [SerializeField] private float weight = 500f;
        // точка гравітації, приблизно центр.
        [SerializeField] private Transform centerGravity;
        protected Rigidbody Rb;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            Rb.mass = weight;
        }
        
        private void FixedUpdate() => HandlePhysics();
        protected virtual void HandlePhysics() { }
    }
}
