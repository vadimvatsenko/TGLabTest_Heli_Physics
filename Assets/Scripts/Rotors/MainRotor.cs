using InputSystem;
using UnityEngine;

namespace Rotors
{
    public class MainRotor : MonoBehaviour, IRotor
    {
        [Header("Main Rotor Properties")] 
        [SerializeField] private Transform lRotor;
        [SerializeField] private Transform rRotor;
        // максимальний кут леза
        [SerializeField] private float maxPitch = 35f;
        public void UpdateRotor(float dps, BaseHeliInput input)
        {
            // оберт 
            transform.Rotate(Vector3.up, dps);
            if (lRotor && rRotor)
            {
                lRotor.localRotation = Quaternion.Euler(0f, 0f, input.CollectiveInput * maxPitch);
                rRotor.localRotation = Quaternion.Euler(0f, 0f, -input.CollectiveInput * maxPitch);
            }
        }
    }
}