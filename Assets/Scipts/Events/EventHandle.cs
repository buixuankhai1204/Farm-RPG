using System;

public delegate void MovementDelegate(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle,
    bool isCarrying,
    ToolEffect toolEffect, bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isPickingRight,
    bool isPickingLeft, bool isPickingUp, bool isPickingDown, bool isLightingToolRight, bool isLightingToolLeft,
    bool isLightingToolUp, bool isLightingToolDown,
    bool isSwimmingToolRight, bool isSwimmingToolLeft, bool isSwimmingToolUp, bool isSwimmingToolDown, bool idleRight,
    bool idleLeft, bool idleUp, bool idleDown);

public static class EventHandler
{
    public static event MovementDelegate movementEvent;

    public static void CallMovementEvent(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle,
        bool isCarrying,
        ToolEffect toolEffect, bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
        bool isPickingRight,
        bool isPickingLeft, bool isPickingUp, bool isPickingDown, bool isLightingToolRight, bool isLightingToolLeft,
        bool isLightingToolUp, bool isLightingToolDown,
        bool isSwimmingToolRight, bool isSwimmingToolLeft, bool isSwimmingToolUp, bool isSwimmingToolDown,
        bool idleRight,
        bool idleLeft, bool idleUp, bool idleDown)
    {
        if (movementEvent != null)
        {
            movementEvent(inputX, inputY, isWalking, isRunning, isIdle, isCarrying, toolEffect, isUsingToolRight,
                isUsingToolLeft, isUsingToolUp, isUsingToolDown,
                isPickingRight, isPickingLeft, isPickingUp, isPickingDown, isLightingToolRight, isLightingToolLeft,
                isLightingToolUp, isLightingToolDown, isSwimmingToolRight,
                isSwimmingToolLeft, isSwimmingToolUp, isSwimmingToolDown, idleRight, idleLeft, idleUp, idleDown);
        }
    }
}
