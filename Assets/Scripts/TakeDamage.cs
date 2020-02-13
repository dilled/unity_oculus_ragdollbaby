using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeDamage : MonoBehaviour
{
    public AudioClip attackAudio;
    public AudioSource audioSource;
    public Color normalColor;
    public Color underwaterColor;
    public float alpha = 0.4f;
    public float fog = 0.1f;
    bool takeHit = false;
    public float tHit = 0f;
    public float tHitHit = 5f;
    int hitCount = 0;
    bool sounded = false;
    void Start()
    {
        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underwaterColor = new Color(1f, 0f, 0.1030f, alpha);
        
    }
    public void AttackAudio()
    {

        audioSource.PlayOneShot(attackAudio, 1f);

    }
    // Update is called once per frame
    void Update()
    {
        if (takeHit)
        {
            tHit += Time.deltaTime * 1;
        }
        if (tHit >= 1f && !sounded)
        {
            AttackAudio();
            sounded = true;
        }
            if (tHit > tHitHit)
        {
            //AttackAudio();
            alpha += .4f;
            fog += .4f;
            RenderSettings.fogColor = new Color(1f, 0f, 0.1030f, alpha);
            RenderSettings.fogDensity = fog;
            RenderSettings.fog = true;
            sounded = false;
            tHit = 0.2f;
            hitCount += 1;
            
        }
        if (hitCount > 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    public void SetNormal()
    {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.01f;
        RenderSettings.fog = true;
    }

    public void NotTakeHit()
    {
        alpha = 0.4f;
        takeHit = false;
        SetNormal();
    }

    public void TakeHit()
    {
        takeHit = true;
    }
}
