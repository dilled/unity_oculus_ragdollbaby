using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCam : MonoBehaviour
{
    public GameObject camPos;

    void Start()
    {
        transform.position = camPos.transform.position;
        transform.rotation = camPos.transform.rotation;
    }
    
}
