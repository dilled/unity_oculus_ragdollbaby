using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaking : MonoBehaviour
{
    public GameObject destroyedVersion;
    public Transform bottlePlacing;
    public AudioClip bottleCrack;
    public AudioSource audioSource;
    public bool dropped = false;
    public float fallTime = 0f;
    public BabyController babyController;
    public bool inuse = false;
    public float amount;

    void Start()
    {
        bottlePlacing.position = transform.position;
        bottlePlacing.rotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Floor")
        {
             audioSource.PlayOneShot(bottleCrack, 0.3f);
             Instantiate(destroyedVersion, transform.position, transform.rotation);
             transform.rotation = bottlePlacing.rotation;
             transform.position = bottlePlacing.position;
             babyController.BabyScared();
        }
        fallTime = 0f;
        dropped = false;
    }

    public void InUse(float amountLeft)
    {
        amount = amountLeft;
        inuse = true;
    }

    public void Dropped()
    {
        dropped = true;
    }

    void Update()
    {

        if (transform.position.y < -10f)
        {
            ReSpawn();
        }
        if (dropped)
        {
            fallTime += 1f;
        }
       if (inuse)
        {
            amount -= .05f;
            if(amount <= 0f)
            {
                Empty();
            }
        }
    }

    public void Empty()
    {
        if (Inventory.instance.items.Count > 0)
        {
            if (Inventory.instance.items[0].isBottle)
            {
                GetComponent<ItemPickup>().Drop();
                Inventory.instance.items[0].RemoveFromInventory();
            }
        }
        inuse = false;
        audioSource.PlayOneShot(bottleCrack, 0.3f);
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        ReSpawn();
        babyController.BabyEaten();
    }
    public void ReSpawn()
    {
        transform.parent = null;
        transform.rotation = bottlePlacing.rotation;
        transform.position = bottlePlacing.position;
    }
}
