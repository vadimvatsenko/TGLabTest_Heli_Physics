* **ThrottleInput** - ручка газу / Керування потужністю двигуна
* **CollectiveInput** - загальний кут установки лопатей несного гвинта, керування висотою
* **CyclicInput** - Нахиляють ніс вертольота вниз або вгору (рух або гальма). **Pitch** - нахил в ліво або право. **Roll** - наклони вперед та назад
* **PedalInput** - поворот ліво чи право, хвіст Yaw в ТЗ

* **HP** - кінська сила
* **Rpm** - обертальний момент

```mermaid
---
title: HeliPhysics
---
classDiagram
    namespace HeliObj {
        class HeliGroup
        class CenterPointGravity
        class Body
        class Engines
        class MainEngine
        class Legs
        class R
        class L
        class RotorController
        class MainRotor
        class TailRotor
        class Tail
        class Rigidbody
        
        class BaseHeliInput
        class BasePhysics
        class BaseRbController
        class HeliController
        
        class MainHeliEngine
        
        
    }
    
    HeliGroup -- CenterPointGravity
    HeliGroup -- Body
    HeliGroup -- Engines
    HeliGroup -- Legs
    HeliGroup -- RotorController
    HeliGroup -- Tail
    Engines -- MainEngine
    Legs -- R
    Legs -- L
    RotorController -- MainRotor
    RotorController -- TailRotor
    
    HeliGroup ..> Rigidbody
    HeliGroup ..> BaseHeliInput
    HeliGroup ..> BasePhysics
    
    BasePhysics ..> Rigidbody
    BasePhysics ..> BaseHeliInput
    BasePhysics ..> MainHeliEngine
    
    HeliController --|> BaseRbController : Inheritance
    
    BaseHeliInput : + int float delayInput
    BaseHeliInput : - InputSystem_Actions _inputSystemActions
    BaseHeliInput : - float _throttleInputFromInput
    BaseHeliInput : + float ThrottleInput
    BaseHeliInput : - bool _isHoldingThrottleInput
    BaseHeliInput : + float CollectiveInput
    BaseHeliInput : - float _collectiveInputFromInput
    BaseHeliInput : - bool _isHoldingCollectiveInput
    BaseHeliInput : + Vector2 CyclicInput
    BaseHeliInput : - float PedalInput
    BaseHeliInput : - OnEnable()
    BaseHeliInput : - Update()
    BaseHeliInput : - StickyThrottleInput()
    BaseHeliInput : - StickyCollectiveInput()
    BaseHeliInput : - OnCyclicPerformed(InputAction.CallbackContext context)
    BaseHeliInput : - OnCyclicCanceled(InputAction.CallbackContext context)
    BaseHeliInput : - OnThrottleInputPerformed(InputAction.CallbackContext context)
    BaseHeliInput : - OnThrottleInputCanceled(InputAction.CallbackContext context)
    BaseHeliInput : - OnCollectiveInputPerformed(InputAction.CallbackContext context)
    BaseHeliInput : - OnCollectiveInputCanceled(InputAction.CallbackContext context)
    BaseHeliInput : - OnPedalInputPerformed(InputAction.CallbackContext context)
    BaseHeliInput : - OnPedalInputCanceled(InputAction.CallbackContext context)
    BaseHeliInput : - OnDisable()
    
    BasePhysics: + float maxLiftForce
    BasePhysics: + float maxAltitude
    BasePhysics: + float aerodynamicEfficiencyExponent
    BasePhysics: + float tailForce
    BasePhysics: + float cyclingForce
    BasePhysics: + float cyclicForceMultiplier
    BasePhysics: + float autoLevelForce
    BasePhysics: + Vector3 _flatForward
    BasePhysics: + float _forwardDot
    BasePhysics: + Vector3 _flatRight
    BasePhysics: + float _rightDot
    BasePhysics: + MainHeliEngine _heliEngine
    BasePhysics: + Rigidbody _rb
    BasePhysics: + BaseHeliInput _input
    BasePhysics: + Start()
    BasePhysics: + UpdateAllPhysics()
    BasePhysics: + HandleLift()
    BasePhysics: + AdvicePhysicsLift()
    BasePhysics: + HandleCyclic()
    BasePhysics: + HandlePedals()
    BasePhysics: + CalculateAngles()
    BasePhysics: + AutoLevel()
    
    BaseRbController: - float weight
    BaseRbController: - Transform centerGravity
    BaseRbController: # Rigidbody Rb
    BaseRbController: - Awake()
    BaseRbController: - FixedUpdate()
    BaseRbController: # HandlePhysics()
    
    HeliController : + List~MainHeliEngine~ engines
    HeliController : + BaseHeliInput _baseHeliInput
    HeliController : + RotorController _rotorController
    HeliController : + BasePhysics _basePhysics
    HeliController : + Start()
    HeliController : + HandlePhysics()
    HeliController : + HandleRotors()
    HeliController : + HandleEngines()
    HeliController : + HandleUpdatePhysics()
    
    
    
```




# Тестове завдання на посаду Unity Developer

**Мета:** Реалізувати фізично коректну модель польоту вертоліта.

**Загальний опис задачі:**  
Потрібно створити невеликий *playable prototype*, у якому користувач може керувати літальним апаратом у 3D-сцені.

---

### 🎯 Основний акцент
*   **Фізика руху** (реалістична поведінка сил).
*   **Керування** (чутливість та відгук).
*   **Стабільність польоту** (система автовирівнювання).
*   **Структура коду** та адекватна архітектура.

---

### 📋 Мінімальні вимоги

#### 1. Основний рух:
*   [x] Зліт
*   [x] Посадка
*   [x] Зависання (Hover)
*   [x] Рух вперед / назад
*   [x] Рух вліво / вправо
*   [x] Поворот по **Yaw** (пеленг)
*   [x] Нахили **Pitch** (тангаж) / **Roll** (крен)

#### 2. Фізика компонентів:
> ⚠️ **Важливо:** Не використовувати готові *flight-controller assets*. Усі розрахунки мають бути чесними.
*   Підйомна сила (*Lift Force*)
*   Тяга (*Thrust*)
*   Гравітація (*Gravity*)
*   Інерція (*Inertia*)
*   Опір повітря (*Drag*)
*   Обертальний момент (*Torque*)

#### 3. Керування (Нова Input System):
*   `W` `A` `S` `D` — Рух (нахили Cyclic)
*   `Space` / `Left Ctrl` — Висота (крок гвинта Collective)
*   `Q` / `E` — Поворот (педалі Yaw)
*   `UpArrow` / `DownArrrow` - кут нахилу леза

---

### 🛠️ Технічні вимоги

*   **Рекомендована версія Unity:** `Unity 6000.2.7f2` (або актуальна версія Unity 6).
*   **Графічний конвеєр:** `HDRP` (High Definition Render Pipeline).
*   **Візуал:** Допускається повна реалізація на 3D-примітивах (куби, сфери, циліндри). Головне — логіка, а не графіка.

---

### ⏱️ Срок виконання
**1 тиждень** з моменту отримання даного завдання.

### 📦 Що потрібно здати:
1. Посилання на публічний **Git-репозиторій** із вихідним кодом проєкту.
2. **Архів з готовим білдом** під Windows / PC для швидкого тестування.

