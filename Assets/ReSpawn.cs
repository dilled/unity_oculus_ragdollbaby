using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawn : MonoBehaviour
{
    public Transform placing;

    void Start()
    {
        placing.position = transform.position;
        placing.rotation = transform.rotation;
    }
    public void ReSpawing()
    {
        transform.parent = null;
        transform.rotation = placing.rotation;
        transform.position = placing.position;
    }
    void Update()
    {
        if (transform.position.y < -10f)
        {
            ReSpawing();
        }
    }
}
