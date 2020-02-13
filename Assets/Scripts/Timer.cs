using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool stopped = false;
    public int soundCount = 0;
    public AudioClip scoreAudio;
    public AudioClip scoreLossAudio;
    public AudioSource audioSource;

    public GameObject scoreAnimator;
    public GameObject levelShow;
    GameObject pstate;
    public int level;
    public bool levelPassed = false;
    public Text timer;
    public GameObject baby;
    public GameObject babyAngry;
    public float minutes;
    public float defMinutes;
    public int hour;
    public int min;
    public int lastHour;
    public bool score = false;
    
    public Transform poops;
    public Transform poopsToEat;
    GameObject poop;
    public int i = 1;

    void Start()
    {
        pstate = GameObject.Find("PlayerState");
        if (pstate != null)
        {
            level = pstate.GetComponent<Player>().level;
            minutes = pstate.GetComponent<LevelSettings>().CurrentMinutes();
        }
        else
        {
            level = 0;
            minutes = 300;
        }
        levelShow.GetComponent<SetLevelText>().SetText(level);
        lastHour = hour;
        defMinutes = minutes;
    }

    public void CountScore()
    {
        GameObject mus = GameObject.Find("Music");
        mus.GetComponent<MusicPlayer>().StopAudio();
        baby.GetComponent<CharacterSounds>().StopAudio();
        scoreAnimator.GetComponent<Animator>().enabled = true;
        score = true;
        //baby.GetComponent<BabyController>().stopped = true;
        //baby.GetComponent<BabyToDo>().enabled = false;
        //babyAngry.GetComponent<BabyAngryController>().StopAngryBaby();
        baby.SetActive(false);
        babyAngry.SetActive(false);
    }

    public void NextLevel()
    {

        //Time.timeScale = 0.01f;
        //SceneManager.LoadScene("LevelUp", LoadSceneMode.Additive);

        //defMinutes += 10f;
        if (baby.GetComponent<CharacterStats>().points > 0f)
        {
            level += 1;
            levelShow.GetComponent<SetLevelText>().SetText(level);
            //minutes = defMinutes;
            if (pstate != null)
            {
                pstate.GetComponent<Player>().SavePlayer(transform);
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 1f;
    }
    public void Score()
    {
        if (baby.GetComponent<CharacterStats>().currentMood > 0f)
        {
            soundCount += 1;
            baby.GetComponent<CharacterStats>().ScoreMood();
            if (soundCount > 5)
            {
                audioSource.PlayOneShot(scoreAudio, 0.8f);
                soundCount = 0;
            }
        }
        else
        {
            
            //scored = true;
            ScoreLoss();

        }
    }
    public void DestroyPoop()
    {
        Destroy(poop);
    }
    public void ScoreLoss()
    {
        
        
        if (poops.childCount > 0)
        {
            Debug.Log("scoreloss poops" + poops.childCount);
            foreach (Transform poopi in poops)
            {
                
                poop = poopi.gameObject;
                Invoke("DestroyPoop", i);
                soundCount += 1;
                baby.GetComponent<CharacterStats>().ScoreMoodLoss();
                if (soundCount > 10)
                {
                    audioSource.PlayOneShot(scoreLossAudio, 0.8f);
                    soundCount = 0;
                }
                i += 1;
            }
        }
        if (poopsToEat.childCount > 0)
        {
            //soundCount += 1;
            if (soundCount > 10)
            {
                //audioSource.PlayOneShot(scoreLossAudio, 0.8f);
                soundCount = 0;
            }
            Debug.Log("scoreloss to eat" + poopsToEat.childCount);
                foreach (Transform poopi in poopsToEat)
                {
                
                poop = poopi.gameObject;
                    Invoke("DestroyPoop", i);
                    
                    baby.GetComponent<CharacterStats>().ScoreMoodLoss();
                
                i += 1;
                }

                
            }
        if (poopsToEat.childCount == 0 && poops.childCount == 0)
        {
            Invoke("NextLevel", 4);
        }
    }

    void Update()
    {
        
        if (score)
        {
            Score();
        }
        else
        {
            if (!stopped)
            {
                minutes -= Time.deltaTime;
            }
            min = Mathf.RoundToInt(minutes);
            hour = min / 60;

            min = min - hour * 60;
            if (lastHour != hour)
            {
                baby.GetComponent<CharacterStats>().SpeedUp();
                lastHour = hour;
            }
            if (min >= 10)
            {
                timer.GetComponent<Text>().text = hour.ToString() + " : " + min.ToString();
            }
            else
            {
                timer.GetComponent<Text>().text = hour.ToString() + " : 0" + min.ToString();
            }
            if (minutes <= 0)
            {
                CountScore();
            }
        }
    }
    public void Stopped()
    {
        stopped = true;
    }
}
