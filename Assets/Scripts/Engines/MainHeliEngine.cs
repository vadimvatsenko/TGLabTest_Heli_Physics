using UnityEngine;

namespace Engines
{
    public class MainHeliEngine : MonoBehaviour
    {
        // максимальне значення кінських сил
        [SerializeField] private float maxHp = 140f;
        // максимальний обертальний момент
        [SerializeField] private float maxRpm = 2700f;
        [SerializeField] private float powerDelay = 2f;
        
        // поточна кінська сила
        public float CurrentHp { get; private set; }
        // поточна обертальний момент
        public float CurrentRpm { get; private set; }
        
        // передаємо значення головного двигуна
        private void UpdateEngine(float throttleInput)
        {
            
        }
    }
}