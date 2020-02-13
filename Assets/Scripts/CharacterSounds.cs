using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    public AudioClip stepAudio;
    public AudioClip cryAudio;
    public AudioClip cryLoopAudio;
    public AudioClip attackAudio;
    public AudioClip drownAudio;
    public AudioClip splashAudio;
    public AudioSource audioSource;

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public void StepAudio()
    {

        audioSource.PlayOneShot(stepAudio, 0.3f);

    }
    public void AttackAudio()
    {

        audioSource.PlayOneShot(attackAudio, 1f);

    }
    public void CryAudio()
    {
        if (cryAudio != null)
            audioSource.PlayOneShot(cryAudio, 1f);

    }
    public void CryLoopAudio()
    {
        if (!GetComponent<BabyController>().eating)
        {
            if (cryLoopAudio != null)
            {
                audioSource.clip = cryLoopAudio;
                audioSource.Play();
                Debug.Log("cryyyyyyyyyyyyy");
            }
            //audioSource.PlayOneShot(cryLoopAudio, 1f);
        }
    }
    public void DrownAudio()
    {

        audioSource.PlayOneShot(drownAudio, 0.3f);

    }
    public void SplashAudio()
    {

        audioSource.PlayOneShot(splashAudio, 1f);

    }
}
