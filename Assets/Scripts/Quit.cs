using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    

    public void QuitGame()
    {
        Debug.Log("Quit game");
        SceneManager.LoadScene(0);
    }
    public void QuitSave()
    {
        Debug.Log("Quit game");
        
        SceneManager.LoadScene(0);
    }
}
