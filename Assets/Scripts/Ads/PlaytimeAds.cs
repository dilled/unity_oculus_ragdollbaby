using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaytimeAds : MonoBehaviour
{
    public float playTime;
    public float adTime;
    public float timePassed;
    public Image waitImage;
    public GameObject waitMenu;
    public GameObject player;
    
    public float timePassedBefore;
    public float m_IntervalBetweenAdsInSecondes = 300f;
    public float timePassedWait;
    public float timePassedWaitBefore;
    public bool clicked;
    public Button adButton;
    public Button adWait;

    public void AdClicked()
    {
        clicked = true;
        timePassedWaitBefore = 0f;
        timePassedWait = 0f;
        m_LatestDisplayedAdTimeWait = Time.time;
        //adButton.gameObject.SetActive(false);
        //adWait.gameObject.SetActive(true);
        
    }
    public void AdClickedWait()
    {
        
        timePassedBefore = 0f;
        m_LatestDisplayedAdTime = Time.time;
        //adButton.gameObject.SetActive(false);
        //adWait.gameObject.SetActive(true);
        
    }

    void Start()
    {
        m_LatestDisplayedAdTime = Time.time;
        m_LatestDisplayedAdTimeWait = Time.time;
        
        
        //m_LatestDisplayedAdTime = playerState.GetComponent<Player>().timePassed;
    }
    public void SetPause()
    {
        Time.timeScale = 0.001f;
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
    }
    void Update()
    {
        timePassed = Time.time - m_LatestDisplayedAdTime + timePassedBefore;
        // waitImage.fillAmount = timePassed / playTime;
        timePassedWait = Time.time - m_LatestDisplayedAdTimeWait + timePassedWaitBefore;


        if (m_LatestDisplayedAdTime == -1f
                 || timePassed >= playTime)
        {
           
            waitMenu.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
            SetPause();
            m_LatestDisplayedAdTime = Time.time;
        }
        if (!clicked
                 || timePassedWait >= m_IntervalBetweenAdsInSecondes)
        {
            adButton.gameObject.SetActive(true);
            adWait.gameObject.SetActive(false);
            clicked = false;
            //playerState.GetComponent<Player>().clicked = false;
            //gameManager.GetComponent<PlaytimeAds>().clicked = false;
        }
        else
        {
            clicked = true;
            adButton.gameObject.SetActive(false);
            adWait.gameObject.SetActive(true);
        }
    }
    private float m_LatestDisplayedAdTime = -1f;
    private float m_LatestDisplayedAdTimeWait = -1f;
}
