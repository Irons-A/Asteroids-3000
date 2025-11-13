using Scripts.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.PlayerInput
{
    public static class InputHelper
    {
        public static MovementState CalculateMovementState(bool acceleratePressed, bool deceleratePressed)
        {
            if (deceleratePressed)
            {
                return MovementState.Decelerating;
            }
            else if (acceleratePressed)
            {
                return MovementState.Accelerating;
            }
            else
            {
                return MovementState.None;
            }
        }
    }
}
