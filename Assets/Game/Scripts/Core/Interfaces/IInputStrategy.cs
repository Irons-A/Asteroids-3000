using Scripts.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Interfaces
{
    public interface IInputStrategy
    {
        void Initialize(InputSettings config);
        PlayerInputData GetInput();
        bool IsDevicePresent();
        void Enable();
        void Disable();
    }
}
