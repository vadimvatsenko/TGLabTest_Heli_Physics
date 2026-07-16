using UnityEngine;

namespace Characteristics
{
    public class BaseCharacterictics : MonoBehaviour
    {
        public void UpdateCharacterictics()
        {
            HandleLift();
            HandleCyclic();
            HandlePedals();
        }

        protected virtual void HandleLift()
        {
            Debug.Log("Lift");
        }
        
        protected virtual void HandleCyclic()
        {
            Debug.Log("Cyclic");
        }
        protected virtual void HandlePedals()
        {
            Debug.Log("Pedals");
        }
    }
}