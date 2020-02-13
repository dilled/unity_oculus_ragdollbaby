using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    [Range(0, 10)]
    public float currentTired;
    [Range(0, 10)]
    public float currentHungry;
    [Range(0, 10)]
    public float currentMood;
    [Range(0, 10)]
    public float currentDiaper;
    public float minutes;
    public bool sleep = true;

}
