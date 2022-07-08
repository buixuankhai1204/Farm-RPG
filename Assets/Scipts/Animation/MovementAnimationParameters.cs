
using System;
using UnityEngine;

public class MovementAnimationParameters : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        AnimationEventPlayFootStepSound();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.movementEvent += SetAnimationParameters;
    }
    
    private void OnDisable()
    {
        EventHandler.movementEvent += SetAnimationParameters;
    }


    public void SetAnimationParameters(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle,
        bool isCarrying,
        ToolEffect toolEffect, bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
        bool isPickingRight,
        bool isPickingLeft, bool isPickingUp, bool isPickingDown, bool isLiftingToolRight, bool isLiftingToolLeft,
        bool isLiftingToolUp, bool isLiftingToolDown,
        bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
        bool idleRight,
        bool idleLeft, bool idleUp, bool idleDown)
    {
        animator.SetFloat(Settings.xInput, inputX);
        animator.SetFloat(Settings.yInput, inputY);
        animator.SetBool(Settings.isWalking, isWalking);
        animator.SetBool(Settings.isRunning, isRunning);
        animator.SetInteger(Settings.toolEffect, (int)toolEffect);

        if (isUsingToolRight)
        {
            animator.SetTrigger(Settings.isUsingToolRight);
        }
        
        if (isUsingToolLeft)
        {
            animator.SetTrigger(Settings.isUsingToolLeft);
        }
        
        if (isUsingToolUp)
        {
            animator.SetTrigger(Settings.isUsingToolUp);
        }
        
        if (isUsingToolDown)
        {
            animator.SetTrigger(Settings.isUsingToolDown);
        }
        
        if (isCarrying)
        {
            animator.SetTrigger(Settings.isCarrying);
        }

        if (isPickingRight)
        {
            animator.SetTrigger(Settings.isPickingRight);
        }
        
        if (isPickingLeft)
        {
            animator.SetTrigger(Settings.isPickingLeft);
        }
        
        if (isPickingUp)
        {
            animator.SetTrigger(Settings.isPickingUp);
        }
        
        if (isPickingDown)
        {
            animator.SetTrigger(Settings.isPickingDown);
        }
        
        if (isLiftingToolRight)
        {
            animator.SetTrigger(Settings.isLiftingToolRight);
        }

        if (isLiftingToolLeft)
        {
            animator.SetTrigger(Settings.isLiftingToolLeft);
        }
        
        if (isLiftingToolUp)
        {
            animator.SetTrigger(Settings.isLiftingToolUp);
        }
        
        if (isLiftingToolDown)
        {
            animator.SetTrigger(Settings.isLiftingToolDown);
        }

        if (isSwingingToolRight)
        {
            animator.SetTrigger(Settings.isSwingingToolRight);
        }
        
        if (isSwingingToolLeft)
        {
            animator.SetTrigger(Settings.isSwingingToolLeft);
        }
        
        if (isSwingingToolUp)
        {
            animator.SetTrigger(Settings.isSwingingToolUp);
        }
        
        if (isSwingingToolDown)
        {
            animator.SetTrigger(Settings.isSwingingToolDown);
        }
        
        if (idleRight)
        {
            animator.SetTrigger(Settings.idleRight);
        }
        
        if (idleLeft)
        {   
            animator.SetTrigger(Settings.idleLeft);
        }
        
        if (idleUp)
        {
            animator.SetTrigger(Settings.idleUp);
        }
        
        if (idleDown)
        {
            animator.SetTrigger(Settings.idleDown);
        }
    }
    private void AnimationEventPlayFootStepSound()
    {
        
    }
}
