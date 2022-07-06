using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerMovement : MonoBehaviour
{
    public  float xInput;
    public  float yInput;
    public  bool isWalking;
    public  bool isRunning;
    public  bool isIdle;
    public  ToolEffect toolEffect;
    public  bool isCarrying;
    public  bool isUsingToolRight;
    public  bool isUsingToolLeft;
    public  bool isUsingToolUp;
    public  bool isUsingToolDown;
    public  bool isLiftingToolRight;
    public  bool isLiftingToolLeft;
    public  bool isLiftingToolUp;
    public  bool isLiftingToolDown;
    public  bool isSwingingToolRight;
    public  bool isSwingingToolLeft;
    public  bool isSwingingToolUp;
    public  bool isSwingingToolDown;
    public  bool isPickingRight;
    public  bool isPickingLeft;
    public  bool isPickingUp;
    public  bool isPickingDown;
   
    //share Animation Parameters
    public  bool idleRight;
    public  bool idleLeft;
    public  bool idleUp;
    public  bool idleDown;

    private void Update()
    {
        EventHandler.CallMovementEvent(xInput, yInput, isWalking, isRunning,  isIdle, isCarrying, toolEffect, isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown,
            isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown, isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown,
            isPickingRight, isPickingLeft, isPickingUp, isPickingDown, idleRight, idleLeft, idleUp, idleDown);
    }
}
