using UnityEngine;

namespace Rotors
{
    public class TailRotor : MonoBehaviour, IRotor
    {
        public void UpdateRotor()
        {
            Debug.Log("UpdateTailRotor");
        }
    }
}