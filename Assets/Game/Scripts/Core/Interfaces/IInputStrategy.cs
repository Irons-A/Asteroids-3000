using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputStrategy
{
    void Initialize(InputSettings config);
    PlayerInputData GetInput();
    bool IsDevicePresent();
    void Enable();
    void Disable();
}
