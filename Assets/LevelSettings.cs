using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public Level[] level;
    public int currentLevel = 1;


    public bool CurrentSleep()
    {
        currentLevel = GetComponent<Player>().level;
        if (level.Length - 1 < currentLevel) currentLevel = 0;
        return level[currentLevel].sleep;
    }
    public float CurrentMinutes()
    {
        currentLevel = GetComponent<Player>().level;
        if (level.Length -1 < currentLevel) currentLevel = 0;
        return level[currentLevel].minutes;
    }
    public float CurrentTired()
    {
        currentLevel = GetComponent<Player>().level;
        if (level.Length -1< currentLevel) currentLevel = 0;
        return level[currentLevel].currentTired;
    }
    public float CurrentMood()
    {
        currentLevel = GetComponent<Player>().level;
        if (level.Length -1< currentLevel) currentLevel = 0;
        return level[currentLevel].currentMood;
    }
    
    public float CurrentHungry()
    {
        currentLevel = GetComponent<Player>().level;
        if (level.Length -1< currentLevel) currentLevel = 0;
        return level[currentLevel].currentHungry;
    }
    public float CurrentDiaper()
    {
        currentLevel = GetComponent<Player>().level;
        if (level.Length -1< currentLevel) currentLevel = 0;
        return level[currentLevel].currentDiaper;
    }
}
