using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
