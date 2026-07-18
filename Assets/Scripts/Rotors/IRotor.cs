using InputSystem;

namespace Rotors
{
    public interface IRotor
    {
        void UpdateRotor(float dps, BaseHeliInput input);
    }
}
