using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipsTouch : MonoBehaviour
{
    public GameObject parent;

    public void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Floor")
        {
            Debug.Log("hitted   " + other.gameObject.name);
        }
        if (GetComponentInParent<BabyAngryController>().grabbed)
        {
            if (other.transform.tag == "Floor")
            {
                Debug.Log("grabbed  hitted   " + other.gameObject.name);
                GetComponentInParent<BabyAngryController>().WakeUp(transform);
               // parent.transform.position = transform.position;
             

            }
        }
    }

}
