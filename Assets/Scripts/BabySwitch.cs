using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySwitch : MonoBehaviour
{
   // public GameObject RagDoll;
    public GameObject baby;
    public GameObject babyAngry;
    public bool switching = false;
    bool babyVisible = false;
    bool babyAngryVisible = true;
    public int switchcount = 0;
    public Transform babyPlacingAfterClimb;
        
    public void BabySwitchClimb()
    {   
        baby.transform.position = babyPlacingAfterClimb.position;
    }
    
    public void BabySwitching()
    {
        babyAngry.transform.position = baby.transform.position;
        babyAngry.transform.rotation = baby.transform.rotation;
        switching = true;
        
    }
    public void Switch()
    {
        babyVisible = !babyVisible;
        babyAngryVisible = !babyAngryVisible;
        switchcount += 1;
        if (switchcount == 31)
        {
            babyAngry.GetComponent<BabyAngryController>().Switched();
            switching = false; 
        }
    }
    void Update()
    {        
        if (switching && switchcount <= 30)
        {
            baby.SetActive(babyVisible);
            babyAngry.SetActive(babyAngryVisible);
           // RagDoll.SetActive(babyAngryVisible);
            Switch();
        }
        
    }
}
