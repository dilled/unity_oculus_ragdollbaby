using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip lullaby;
    public AudioSource audioSource;
    public bool fade = false;
    public bool sleep = false;
    public float volume =.2f;
    void Start()
    {
        audioSource.clip = lullaby;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();
    }
    
    public void Sleep()
    {
        sleep = true;
        fade = false;
    }

    public void Fade()
    {
        sleep = false;
        fade = true;
    }

    public void StopAudio()
    {
        audioSource.Stop();
        Debug.Log("stopaudio");
    }

    public void LullabyTired()
    {
        audioSource.clip = lullaby;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();
        Debug.Log("tired audio");
    }

    private void Update()
    {
        if (fade)
        {
            volume -= .005f;
            audioSource.volume = volume;
            if(volume == 0f)
            {
                StopAudio();
                fade = false;
            }
        }
        if (sleep)
        {
            volume += .005f;
            audioSource.volume = volume;
            if (volume >= .2f)
            {
                sleep = false;
            }
        }
    }
}
