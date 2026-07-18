using System;
using UnityEngine;

// клас відповідає за оновлення поточної кінської сила та швидкості оберту ЦД
namespace Engines
{
    public class MainHeliEngine : MonoBehaviour
    {
        [Header("Engine Characteristics")]
        [SerializeField] private float maxHp = 140f;
        [SerializeField] float maxRpm = 2700f;
        public float MaxRpm => maxRpm;
        
        // час у секундах, за який двигун розкручується з 0 до 100%
        [SerializeField] private float engineResponseTime = 5.0f;
        
        [SerializeField] private AnimationCurve powerCurve 
            = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        
        public Action<float> OnChangeCurrentHp;
        public Action<float> OnChangeCurrentRpm;
        
        private float _lastSentHp;
        private float _lastSentRpm;
        
        // поточна кінська сила
        public float CurrentHp { get; private set; }
        // поточний обертальний момент
        public float CurrentRpm { get; private set; }
        
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