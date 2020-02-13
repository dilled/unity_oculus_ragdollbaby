using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator animator;
    GameObject pstate;
    public GameObject continueButton;

    private void Start()
    {
        string path = Application.persistentDataPath + "/Babysitter.sav";
        if (File.Exists(path))
        {
            if (continueButton != null)
            {
                continueButton.SetActive(true);
            }
        }
        pstate = GameObject.Find("PlayerState");
    }
    public void Fade(string what)
    {
        animator.SetTrigger("fadeOut");
        Invoke(what, 3);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
    public void AgainNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        pstate.GetComponent<Player>().LoadPlayer();
    }
    public void NewGame()
    {
        pstate.GetComponent<Player>().LoadPlayerDef();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }
    public void SettedLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
       
        Application.Quit();
    }
    
}
