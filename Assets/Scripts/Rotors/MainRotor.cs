using UnityEngine;

namespace Rotors
{
    public class MainRotor : MonoBehaviour, IRotor
    {
        public void UpdateRotor()
        {
            Debug.Log("UpdateTailRotor");
        }
    }
}