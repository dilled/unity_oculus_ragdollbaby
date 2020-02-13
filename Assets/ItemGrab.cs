using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrab : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        Physics.IgnoreCollision(player.gameObject.GetComponent<Collider>(), GetComponent<Collider>());

    }
    
}
