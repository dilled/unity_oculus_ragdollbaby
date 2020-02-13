using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/* Base class that player and enemies can derive from to include stats. */

public class CharacterStats : MonoBehaviour
{
    public int points = 0;
    public Text score;
    public float multiplier = 1f;
    public Image tiredBar;
    public Image hungryBar;
    public Image moodBar;
    public Image diaperBar;
    public bool inBed = true;

    public float maxTired = 10;
    public float currentTired { get; private set; }

    public float maxHungry = 10;
    public float currentHungry { get; private set; }

    public float maxMood = 10;
    public float currentMood { get; private set; }

    public float maxDiaper = 10;
    public float currentDiaper { get; private set; }

    public float eat = 10f;
    public float mood = 10f;
    public float sleep = 20f;
    public float diaper = 5f;

    BabyController playerController;
    GameObject pstate;

    void Awake()
    {
        playerController = GetComponent<BabyController>();
        pstate = GameObject.Find("PlayerState");
        if (pstate != null && pstate.GetComponent<Player>().level != 0)
        {
            currentTired = pstate.GetComponent<LevelSettings>().CurrentTired();
            currentMood = pstate.GetComponent<LevelSettings>().CurrentMood();
            currentHungry = pstate.GetComponent<LevelSettings>().CurrentHungry();
            currentDiaper = pstate.GetComponent<LevelSettings>().CurrentDiaper();
            diaperBar.fillAmount = currentDiaper / maxDiaper;
            hungryBar.fillAmount = currentHungry / maxHungry;
            moodBar.fillAmount = currentMood / maxMood;
            tiredBar.fillAmount = currentTired / maxTired;
        }
        else
        {
            currentTired = maxTired / 1.5f;
            currentMood = 0f;// maxMood / 2f;
            currentHungry = maxHungry / 3f;
            currentDiaper = maxDiaper / 2f;
        }
    }
    void start()
        {
        
        
    }
    public void SpeedUp()
    {
       // multiplier += .3f;
    }
    public void NewDiaper()
    {
        currentDiaper = 0f;      
        diaperBar.fillAmount = currentDiaper / maxDiaper;   
    }

    public void FillDiaper()
    {
        currentDiaper = Mathf.Clamp(currentDiaper, 0, maxDiaper);
        currentDiaper += Time.deltaTime * diaper * multiplier;
        diaperBar.fillAmount = currentDiaper / maxDiaper;
        if (currentDiaper >= maxDiaper)
        {
            if (!inBed)
            {
                playerController.DiaperFull();
            }
            else
            {
                playerController.BabyScared();
            }
        }
    }
    public void Eat()
    {
       // hungryBar.transform.parent.gameObject.SetActive(false);
        currentHungry = Mathf.Clamp(currentHungry, 0, maxHungry);
        currentHungry += Time.deltaTime * eat*4 ;
        hungryBar.fillAmount = currentHungry / maxHungry;

        if (currentHungry > 2f)
        {
            playerController.hungry = false;
        }
        if (currentHungry >= maxHungry)
        {
            playerController.BabyEaten();
        }
    }
    public void GainHungry()
    {
        //hungryBar.transform.parent.gameObject.SetActive(false);
        currentHungry = Mathf.Clamp(currentHungry, 0, maxHungry);
        currentHungry -= Time.deltaTime *eat/2 * multiplier;
        hungryBar.fillAmount = currentHungry / maxHungry;

        if (currentHungry < 2f)
        {
            playerController.BabyHungry();
        }
        if (currentHungry <= 0f)
        {
            LoseMood(mood * 2);
        }
    }

    public void GainMood()
    {
        //moodBar.transform.parent.gameObject.SetActive(false);
        currentMood = Mathf.Clamp(currentMood, 0, maxMood);
        currentMood += Time.deltaTime * mood;
        moodBar.fillAmount = currentMood / maxMood;
    }
    public void LoseMood(float mood)
    {
        Debug.Log("losemood");
        //moodBar.transform.parent.gameObject.SetActive(false);
        currentMood = Mathf.Clamp(currentMood, 0, maxMood);
        currentMood -= Time.deltaTime * mood/2 * multiplier;
        moodBar.fillAmount = currentMood / maxMood;
        if (currentMood <= maxMood /2)
        {
            if (inBed && currentTired > maxTired / 2)
            {
                //playerController.crying = false;
                //playerController.climbFromBed = true;
               // inBed = false;
            }
            else
            {
                //playerController.crying = true;
            }
        }
        if (currentMood <= 0)
        {
            GetComponentInParent<BabySwitch>().BabySwitching();
        }
    }
    public void ScoreMood()
    { 
        
        currentMood = Mathf.Clamp(currentMood, 0, maxMood);
        currentMood -= Time.deltaTime * 2f;
        points += 10;
        moodBar.fillAmount = currentMood / maxMood;
        score.GetComponent<Text>().text = points.ToString();
        if (currentMood <= 0)
        {
           
        }
    }

    public void ScoreMoodLoss()
    {
        points -= 1;      
        score.GetComponent<Text>().text = points.ToString();      
    }

    public void PickedUp()
    {
        inBed = !inBed;
        Debug.Log("picked up");
    }
    public void Sleep()
    {      
        currentTired = Mathf.Clamp(currentTired, 0, maxTired);
        if (inBed)
        {
            currentTired += Time.deltaTime * sleep *2;
            GainMood();
        }
        else
        {
            LoseMood(mood / 2);
        }

        tiredBar.fillAmount = currentTired / maxTired;
        if(currentTired >= maxTired)
        {
            playerController.BabyAwake();
        }
        if (currentTired >= maxTired/3)
        {
            playerController.tired = false;
        }

    }
    public void NotSleep()
    {
        currentTired = Mathf.Clamp(currentTired, 0, maxTired);
        currentTired -= Time.deltaTime * sleep/2 * multiplier;
        tiredBar.fillAmount = currentTired / maxTired;
        if (currentTired <= 2f && currentTired >= 0.3f)
        {
            playerController.crying = true;
            playerController.BabyTired();
            LoseMood(mood);
            
            
        }else if (currentTired < 0.3f)
        {
            playerController.BabySleep();
        }

    }
    
}
