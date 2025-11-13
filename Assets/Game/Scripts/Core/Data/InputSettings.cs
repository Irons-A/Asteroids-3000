using Scripts.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.Data
{
    [Serializable]
    public class InputSettings
    {
        public KeyboardMouseSettings KeyboardMouse;
        public GamepadSettings Gamepad;
        public MobileSettings Mobile;
    }
}
