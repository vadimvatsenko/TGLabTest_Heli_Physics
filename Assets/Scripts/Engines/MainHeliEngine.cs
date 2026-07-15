using UnityEngine;

namespace Engines
{
    public class MainHeliEngine : MonoBehaviour
    {
        // максимальне значення кінських сил
        [SerializeField] private float maxHp = 140f;
        // максимальний обертальний момент
        [SerializeField] private float maxRpm = 2700f;
        [SerializeField] private float powerDelay = 0.2f;
        
        [SerializeField] private AnimationCurve powerCurve 
            = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        
        // поточна кінська сила
        public float CurrentHp { get; private set; }
        // поточна обертальний момент
        public float CurrentRpm { get; private set; }
        
        // передаємо значення головного двигуна
        public void UpdateEngine(float throttleInput)
        {
            float t = Time.deltaTime * powerDelay;
            // розрахунок кінської сили
            //float wantedHp = throttleInput * maxHp;
            float wantedHp = powerCurve.Evaluate(throttleInput) * maxHp;
            CurrentHp = Mathf.Lerp(CurrentHp, wantedHp, t);
            
            // розрахунок обертального моменту
            //float wantedRpm = throttleInput * maxRpm;
            float wantedRpm =  powerCurve.Evaluate(throttleInput) * maxRpm;
            CurrentRpm = Mathf.Lerp(CurrentRpm, wantedRpm, t);
        }
    }
}