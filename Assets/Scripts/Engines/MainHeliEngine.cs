using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Engines
{
    public class MainHeliEngine : MonoBehaviour
    {
        [Header("Engine Characteristics")]
        [Tooltip("максимальне значення кінських сил")]
        [SerializeField] private float maxHp = 140f;
        
        [Tooltip("максимальний обертальний момент за хв")]
        [SerializeField] float maxRpm = 2700f;
        public float MaxRpm => maxRpm;
        
        [Tooltip("час у секундах, за який двигун розкручується з 0 до 100%")]
        [SerializeField] private float engineResponseTime = 5.0f;
        
        [Tooltip("Крива розподілу потужності двигуна від ручки газу")]
        [SerializeField] private AnimationCurve powerCurve 
            = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        
        public Action<float> OnChangeCurrentHp;
        public Action<float> OnChangeCurrentRpm;
        
        private float _lastSentHp;
        private float _lastSentRpm;
        
        // поточна кінська сила
        public float CurrentHp { get; private set; }
        // поточна обертальний момент
        public float CurrentRpm { get; private set; }
        
        /// <summary>
        /// // old version
        /// float t = Time.fixedDeltaTime * powerDelay;
        /// розрахунок кінської сили
        /// float wantedHp = throttleInput * maxHp;
        /// float wantedHp = powerCurve.Evaluate(throttleInput) * maxHp;
        /// CurrentHp = Mathf.Lerp(CurrentHp, wantedHp, t);
        /// OnChangeCurrentHp?.Invoke(CurrentHp);

        /// розрахунок обертального моменту
        ///float wantedRpm = throttleInput * MaxRpm;
        ///float wantedRpm =  powerCurve.Evaluate(throttleInput) * MaxRpm;
        ///CurrentRpm = Mathf.Lerp(CurrentRpm, wantedRpm, t);
        ///OnChangeCurrentRpm?.Invoke(CurrentRpm);*/
        /// </summary>
        public void UpdateEngine(float throttleInput)
        {
            float targetPowerFactor = powerCurve.Evaluate(throttleInput);
            
            float targetHp = targetPowerFactor * maxHp;
            float targetRpm = targetPowerFactor * MaxRpm;
            
            float hpChangeRate = maxHp / engineResponseTime;
            float rpmChangeRate = MaxRpm / engineResponseTime;

            CurrentHp = Mathf.MoveTowards(CurrentHp, targetHp, hpChangeRate * Time.deltaTime);
            CurrentRpm = Mathf.MoveTowards(CurrentRpm, targetRpm, rpmChangeRate * Time.deltaTime);
            
            if (!Mathf.Approximately(CurrentHp, _lastSentHp))
            {
                _lastSentHp = CurrentHp;
                OnChangeCurrentHp?.Invoke(CurrentHp);
            }

            if (!Mathf.Approximately(CurrentRpm, _lastSentRpm))
            {
                _lastSentRpm = CurrentRpm;
                OnChangeCurrentRpm?.Invoke(CurrentRpm);
            }
        }
    }
}