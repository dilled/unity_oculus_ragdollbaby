using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    #region Singleton

    public static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        /*
        position[0] = GetComponent<PlayerManager>().startPos.position.x;
        position[1] = GetComponent<PlayerManager>().startPos.position.y;
        position[2] = GetComponent<PlayerManager>().startPos.position.z;
        diamonds = GetComponent<Inventory>().diamonds;
        ableSwim = player.GetComponent<CharacterStats>().ableSwim;
        maxHealth = player.GetComponent<CharacterStats>().maxHealth;
        maxStamina = player.GetComponent<CharacterStats>().maxStamina;

        currentQuest = friend.GetComponent<Quests>().currentQuest;
    */
    }
    #endregion
    public int level;
    public GameObject gameManager;

    public void SavePlayer(Transform player)
    {       
        gameManager = GameObject.Find("GameManager");
        level = gameManager.GetComponent<Timer>().level;
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        level = data.level;
    }

    public void LoadPlayerDef()
    {
        level = 1;
    }
    
}
