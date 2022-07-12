using System;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    public static event Action<InventoryLocation, List<InventoryItem>> InventoryUpdateEvent;

    public static void CallInventoryUpdateEvent(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        if (InventoryUpdateEvent != null)
        {
            InventoryUpdateEvent(inventoryLocation, inventoryList);
        }
    }
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

    public static event Action<int, Season, int, string, int, int, int> AdvanceGameMinuteEvent;

    public static void CallAdvanceGameMinuteEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek,
        int gameHour, int gameMinute, int gameSecond)
    {
        if (AdvanceGameMinuteEvent != null)
        {
            AdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
        }
    }
    
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameHourEvent;

    public static void CallAdvanceGameHourEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek,
        int gameHour, int gameMinute, int gameSecond)
    {
        if (AdvanceGameHourEvent != null)
        {
            AdvanceGameHourEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
        }
    }
    
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameDayEvent;

    public static void CallAdvanceGameDayEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek,
        int gameHour, int gameMinute, int gameSecond)
    {
        if (AdvanceGameDayEvent != null)
        {
            AdvanceGameDayEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
        }
    }
    
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameSeasonEvent;

    public static void CallAdvanceGameSeasonEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek,
        int gameHour, int gameMinute, int gameSecond)
    {
        if (AdvanceGameSeasonEvent != null)
        {
            AdvanceGameSeasonEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
        }
    }
    
    public static event Action<int, Season, int, string, int, int, int> AdvanceGameyearEvent;

    public static void CallAdvanceGameyearEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek,
        int gameHour, int gameMinute, int gameSecond)
    {
        if (AdvanceGameyearEvent != null)
        {
            AdvanceGameyearEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
        }
    }
}
