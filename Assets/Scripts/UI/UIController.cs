using Engines;
using Rotors;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private MainHeliEngine mainHeliEngine;
        [SerializeField] private MainRotor mainRotor;
        [SerializeField] private TextMeshProUGUI textHp;
        [SerializeField] private TextMeshProUGUI textRpm;
        [SerializeField] private TextMeshProUGUI textBladesAngle; 

        private void OnEnable()
        {
            mainHeliEngine.OnChangeCurrentHp += OnTextHpChanged;
            mainHeliEngine.OnChangeCurrentRpm += OnTextRpmChanged;
            mainRotor.OnRotate += OnTextBladesAngleChanged;
        }

        private void OnDisable()
        {
            mainHeliEngine.OnChangeCurrentHp -= OnTextHpChanged;
            mainHeliEngine.OnChangeCurrentRpm -= OnTextRpmChanged;
            mainRotor.OnRotate -= OnTextBladesAngleChanged;
        }

        private void OnTextHpChanged(float text) => textHp.text = $"HP : {text.ToString()}";
        private void OnTextRpmChanged(float text) => textRpm.text = $"RPM: {text.ToString()}";

        private void OnTextBladesAngleChanged(float text) =>
            textBladesAngle.text = $"Blades Angle: {text.ToString()}";
    }
}