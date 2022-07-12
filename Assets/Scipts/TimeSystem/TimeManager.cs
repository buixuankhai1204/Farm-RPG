using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : SingletonBehaviour<TimeManager>
{
    private int gameYear = 1;
    private Season gameSeason = Season.Spring;
    private int gameDay = 1;
    private int gameHour = 6;
    private int gameMinute = 30;
    private int gameSecond = 0;
    private string gameDayOfWeek = "Mon";

    private bool gameClockPause = false;
    private float gameTick = 0f;

    private void Start()
    {
        EventHandler.CallAdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute,
            gameSecond);
    }

    private void Update()
    {
        if (!gameClockPause)
        {
            GameTick();
        }
    }

    private void GameTick()
    {
        gameTick += Time.deltaTime;
        if (gameTick >= Settings.secondsPerGameSecond)
        {
            gameTick -= Settings.secondsPerGameSecond;
            UpdateGameSecond();
        }

    }

    private void UpdateGameSecond()
    {
        gameSecond++;
        if (gameSecond > 59)
        {
            gameSecond = 0;
            gameMinute++;

            if (gameMinute > 59)
            {
                gameMinute = 0;
                gameHour++;
            }

            if (gameHour > 23)
            {
                gameHour = 0;
                gameDay++;

                if (gameDay > 30)
                {
                    gameDay = 1;
                    int gs = (int)gameSeason;
                    gs++;
                    gameSeason = (Season)gs;

                    if (gs > 3)
                    {
                        gs = 0;
                        gameYear++;

                        EventHandler.CallAdvanceGameyearEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour,
                            gameMinute, gameSecond);
                    }

                    EventHandler.CallAdvanceGameSeasonEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour,
                        gameMinute, gameSecond);
                }

                gameDayOfWeek = DayOfWeek();
                EventHandler.CallAdvanceGameDayEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute,
                    gameSecond);
            }

            EventHandler.CallAdvanceGameHourEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute,
                gameSecond);
        }
        Debug.Log("Game Season: "+gameSeason + " Game Day: " + gameDay + " game Hour: " + gameHour + " GameMinute: " + gameMinute);
        EventHandler.CallAdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute,
            gameSecond);
    }

    private string DayOfWeek()
    {
        int todayDays = ((int)gameSeason * 30) + gameDay;
        int dayOfWeek = todayDays % 7;

        switch (dayOfWeek)
        {
            case 1:
                return "Mon";

            case 2:
                return "Tue";

            case 3:
                return "Wed";


            case 4:
                return "Thu";


            case 5:
                return "Fri";


            case 6:
                return "Sat";


            case 7:
                return "Sun";
            default:
                return "";
        }
    }
}