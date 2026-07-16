using System;
using Engines;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private MainHeliEngine mainHeliEngine;
        [SerializeField] private TextMeshProUGUI textHp;
        [SerializeField] private TextMeshProUGUI textRpm;

        private void OnEnable()
        {
            mainHeliEngine.OnChangeCurrentHp += OnTextHpChanged;
            mainHeliEngine.OnChangeCurrentRpm += OnTextRpmChanged;
        }

        private void OnDisable()
        {
            mainHeliEngine.OnChangeCurrentHp -= OnTextHpChanged;
            mainHeliEngine.OnChangeCurrentRpm -= OnTextRpmChanged;
        }

        private void OnTextHpChanged(float text) => textHp.text = $"HP : {text.ToString()}";
        private void OnTextRpmChanged(float text) => textRpm.text = $"RPM: {text.ToString()}";
    }
}