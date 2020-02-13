using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaperControl : MonoBehaviour
{
    public GameObject diaper;
    public GameObject baby;
    public GameObject destroyedVersion;
    public GameObject poopie;
    public GameObject poops;
    public GameObject unicorn;

    public bool poop = false;
    public int wait = 0;
    public int poopWaitTime = 100;
    int defPoopWaitTime;
    public int diaperDropTime = 2;

    private void Start()
    {
        defPoopWaitTime = poopWaitTime;
    }
    public void DropDiaper()
    {
        Invoke("DroppingDiaper", diaperDropTime);
    }

    public void DroppingDiaper()
    {
        Transform diap = baby.transform;
        diaper.SetActive(false);
        GameObject diaperClone = Instantiate(destroyedVersion, diap.position, diap.rotation);
        diaperClone.transform.parent = poops.transform;
        poop = true;
    }

    public void PutDiaper()
    {
        diaper.SetActive(true);
        poop = false;
        poopWaitTime = defPoopWaitTime;
    }

    private void Update()
    {
        if (poop)
        {
            if (wait >= poopWaitTime)
            {
                Transform diap = baby.transform;
                GameObject poopClone = Instantiate(poopie, diap.position, diap.rotation);
                poopClone.transform.parent = poops.transform;
                wait = 0;
                if (poopWaitTime > 101)
                {
                    poopWaitTime -= 100;
                }
                unicorn.GetComponent<FriendController>().AllNotEaten();
            }
            else
            {
                wait += 1;
            }
        }
    }
}
