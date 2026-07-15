using UnityEngine;

namespace Rotors
{
    public class TailRotor : MonoBehaviour, IRotor
    {
        [SerializeField] private float tailRotorSpeed = 1.5f;
        public void UpdateRotor(float dps)
        {
            transform.Rotate(Vector3.right, dps * tailRotorSpeed, Space.Self);
        }
    }
}