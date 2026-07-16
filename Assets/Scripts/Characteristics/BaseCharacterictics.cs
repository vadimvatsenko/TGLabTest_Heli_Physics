using Engines;
using InputSystem;
using UnityEngine;

namespace Characteristics
{
    public class BaseCharacterictics : MonoBehaviour
    {
        [Header("Lift Properties")] 
        [SerializeField] private float maxLiftForce = 3.0f;
        [SerializeField] private float maxAltitude = 200f;
        [SerializeField] private float aerodynamicEfficiencyExponent = 0.66f;

        [Header("Tail Rotor Properties")] 
        [SerializeField] private float tailForce = 2f;
        
        [Header("Cyclic Properties")]
        [SerializeField] private float cyclingForce = 2f;
        [SerializeField] private float cyclicForceMultiplier = 1000f;

        [Header("Auto Level Properties")] 
        [SerializeField] private float autoLevelForce = 2f;

        // змінні для розрахунку нахилу 
        private Vector3 _flatForward;
        private float _forwardDot;
        private Vector3 _flatRight;
        private float _rightDot;
        
        // це перший двигун, я передбачаю, що він один !!!
        private MainHeliEngine _heliEngine;
        private Rigidbody _rb;
        private BaseHeliInput _input;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _input = GetComponent<BaseHeliInput>();
            _heliEngine = GetComponentInChildren<MainHeliEngine>();
        }
        
        public void UpdateCharacterictics()
        {
            CalculateAngles();
            Autolevel();
            HandleLift();
            HandleCyclic();
            HandlePedals();
        }

        /// <summary>
        /// F = m * g (сила гравітації * масу = сила тяжості)
        /// Physics.gravity.magnitude = 9.81
        /// mass = 500
        /// Vector3(0, 1, 0) * 9.81 * 500 = Vector3(0, 4905, 0)
        /// Vector3 liftForce = transform.up * (Physics.gravity.magnitude * rb.mass);
        /// rb.AddForce(liftForce, ForceMode.Force);
        /// ForceMode.Force - постійно використовуємо силу
        /// </summary>

        ///<summary>
        /// 
        /// </summary>>
        protected virtual void HandleLift()
        {
            CalcLift();
            //FreezeLift();
        }

        private void CalcLift()
        {
            /*Vector3 liftForce = transform.up * ((Physics.gravity.magnitude + maxLiftForce) * rb.mass);
            float normalizedRpm = _heliEngine.CurrentRpm / 6f;
            rb.AddForce(liftForce * (Mathf.Pow(normalizedRpm, 2f) * Mathf.Pow(input.CollectiveInput, 2f)), ForceMode.Force);*/
            
            // базова сила тяжіння (F = m * g) за Загорданом
            float gravityForce = Physics.gravity.magnitude * _rb.mass;
            // Розраховуємо повну силу .Компенсація ваги + надлишок для зльоту. Максимально можлива тяга гвинта
            float maxPossibleForce = gravityForce + (maxLiftForce * _rb.mass);
            // Нормалізація обертів двигуна
            float mormalizedRpm = Mathf.Clamp01(_heliEngine.CurrentRpm / _heliEngine.MaxRpm);
            // ефект щільності повітря 
            float currentAltitude = transform.position.y;
            float airDensityFactor = Mathf.Clamp01(1f - (currentAltitude / maxAltitude));
            
            // 6. НЕЛЬНІЙНИЙ ККД ЗА ЗАГОРДАНОМ (Формула ступеня 2/3)
            // Ми об'єднуємо оберти та колектив у загальний коефіцієнт потужності,
            // а потім застосовуємо степінь 2/3 (0.66f) для імітації втрат ККД на тертя повітря об лопаті.
            float rawPowerCoeff = mormalizedRpm * _input.CollectiveInput;
            float aerodynamicEfficiency = Mathf.Pow(rawPowerCoeff, aerodynamicEfficiencyExponent);
            
            // 7. ФІНАЛЬНИЙ ВЕКТОР ПІДЙОМНОЇ СИЛИ
            // Напрямок: завжди локальний "верх" вертольота (transform.up).
            // Величина: Максимальна сила * Ефективність лопатей * Щільність повітря на цій висоті.
            float finalLiftMagnitude = maxPossibleForce * aerodynamicEfficiency * airDensityFactor;
            Vector3 liftForceVector = transform.up * finalLiftMagnitude;

            // 8. ЗАСТОСУВАННЯ ФІЗИЧНОЇ СИЛИ
            // Використовуємо ForceMode.Force, який враховує масу вертольота і FixedUpdate-інтервал.
            _rb.AddForce(liftForceVector, ForceMode.Force);

            // Debug-вивід для налаштування (можна вимкнути в релізі)
            Debug.DrawRay(transform.position, liftForceVector / _rb.mass, Color.green);
        }

        private void FreezeLift()
        {
            Vector3 liftForce = transform.up * (Physics.gravity.magnitude * _rb.mass);
            _rb.AddForce(liftForce, ForceMode.Force);
        }

        protected virtual void HandleCyclic()
        {
            float cyclicZForce = _input.CyclicInput.x * cyclingForce;
            _rb.AddRelativeTorque(Vector3.forward * cyclicZForce, ForceMode.Acceleration);
            
            float cyclicXForce = _input.CyclicInput.y * cyclingForce;
            _rb.AddRelativeTorque(Vector3.right * cyclicXForce, ForceMode.Acceleration);
        }
        
        // поворот по осі
        protected virtual void HandlePedals()
        {
            _rb.AddTorque(transform.up * (_input.PedalInput * tailForce), ForceMode.Acceleration);
        }

        private void CalculateAngles()
        {
            _flatForward = transform.forward;
            _flatForward.y = 0;
            _flatForward.Normalize();
            
            _flatRight = transform.right;
            _flatRight.y = 0;
            _flatRight.Normalize();
            
            _forwardDot = Vector3.Dot(transform.up, _flatForward);
            _rightDot = Vector3.Dot(transform.up, _flatRight);
        }

        private void Autolevel()
        {
            float rightForce = -_forwardDot * autoLevelForce;
            float forwardForce = _rightDot * autoLevelForce;
            
            _rb.AddRelativeTorque(Vector3.right * rightForce, ForceMode.Acceleration);
            _rb.AddRelativeTorque(Vector3.forward * forwardForce, ForceMode.Acceleration);
        }
    }
}