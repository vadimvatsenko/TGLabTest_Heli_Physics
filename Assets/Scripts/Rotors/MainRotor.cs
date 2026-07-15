using UnityEngine;

namespace Rotors
{
    public class MainRotor : MonoBehaviour, IRotor
    {
        public void UpdateRotor(float dps)
        {
            transform.Rotate(Vector3.up, dps);
        }
    }
}