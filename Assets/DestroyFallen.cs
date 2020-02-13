using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFallen : MonoBehaviour
{
    public bool smelly = false;
    public float countDown = 0f;
    
    void Update()
    {
        if (!smelly)
        {
            countDown += .1f;
        }

        if (countDown > 20)
        {
            smelly = true;
            countDown = 0f;
           // GetComponent<BoxCollider>().enabled = true;
        }

        if (transform.position.y < -2f)
        {
            Destroy(gameObject);
        }
        
    }
}
