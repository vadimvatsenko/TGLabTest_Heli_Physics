using System;
using InputSystem;
using UnityEngine;

namespace Rotors
{
    public class MainRotor : MonoBehaviour, IRotor
    {
        [Header("Main Rotor Properties")] 
        [SerializeField] protected Transform lFirstRotor;
        [SerializeField] protected Transform rFirstRotor;
        // максимальний кут леза
        [SerializeField] protected float maxPitch = 35f;

        public Action<float> OnRotate;
        public void UpdateRotor(float dps, BaseHeliInput input)
        {
            float pitch = input.CollectiveInput * maxPitch;
            OnRotate?.Invoke(pitch);
            // оберт 
            transform.Rotate(Vector3.up, dps, Space.Self);
            
            if (lFirstRotor && rFirstRotor)
            {
                lFirstRotor.localRotation = Quaternion.Euler(pitch, 0f, 0f );
                rFirstRotor.localRotation = Quaternion.Euler(-pitch, 0f, 0f);
            }
        }
    }
}