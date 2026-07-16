using InputSystem;
using UnityEngine;

namespace Characteristics
{
    public class BaseCharacterictics : MonoBehaviour
    {
        [Header("Lift Properties")] 
        [SerializeField] private float maxLiftForce;
        public void UpdateCharacterictics(Rigidbody rb, BaseHeliInput input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandlePedals(rb, input);
        }

        /// <summary>
        /// F = m * g (сила гравітації * масу = сила тяжості)
        /// Physics.gravity.magnitude = 9.81
        /// mass = 500
        /// Vector3(0, 1, 0) * 9.81 * 500 = Vector3(0, 4905, 0)
        /// Vector3 liftForce = transform.up * (Physics.gravity.magnitude * rb.mass);
        /// rb.AddForce(liftForce, ForceMode.Force);
        /// ForceMode.Force - постійно використовуємо силу
        /// </summary>
        protected virtual void HandleLift(Rigidbody rb, BaseHeliInput input)
        {
            Vector3 liftForce = transform.up * (Physics.gravity.magnitude * rb.mass);
            Debug.Log(Mathf.Pow(input.CollectiveInput, 2f));
            rb.AddForce(liftForce * Mathf.Pow(input.CollectiveInput, 2f), ForceMode.Force);
            
            //Vector3 liftForce = transform.up * (Physics.gravity.magnitude * rb.mass);
            //rb.AddForce(liftForce, ForceMode.Force);
        }
        
        protected virtual void HandleCyclic(Rigidbody rb, BaseHeliInput input)
        {
            //Debug.Log("Cyclic");
        }
        protected virtual void HandlePedals(Rigidbody rb, BaseHeliInput input)
        {
            //Debug.Log("Pedals");
        }
    }
}