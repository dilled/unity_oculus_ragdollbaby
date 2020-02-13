using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleAdPause : MonoBehaviour
{
    public float playTime;
    public float timePassed = 0;
    public Image waitImage;
    public bool eka = true;
    public GameObject mainMenu;
    public GameObject player;
    public GameObject quitButton;
    public GameObject quitButton2;

    void Start()
    {
        playTime = GetComponentInParent<PlaytimeAds>().adTime;
        timePassed = 0f;
        m_LatestDisplayedAdTime = Time.time;
    }
    public void SetPause()
    {
        Time.timeScale = 0.001f;
    }
    public void AdWatched()
    {
        GetComponentInParent<PlaytimeAds>().AdClickedWait();
        m_LatestDisplayedAdTime = Time.time;
        //UnPause();
        timePassed = 0f;
        eka = true;
        mainMenu.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(false);
        quitButton2.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (eka)
        {
            m_LatestDisplayedAdTime = Time.time;
            eka = false;
        }
        timePassed = (Time.time - m_LatestDisplayedAdTime) / Time.timeScale;// 100;
        waitImage.fillAmount = timePassed / playTime;


        if (m_LatestDisplayedAdTime == -1f
                 || timePassed >= playTime)
        {
            
            
            m_LatestDisplayedAdTime = Time.time;
            //UnPause();
            timePassed = 0f;
            eka = true;
            mainMenu.gameObject.SetActive(true);
            player.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(false);
            quitButton2.gameObject.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }
    private float m_LatestDisplayedAdTime = -1f;
}
