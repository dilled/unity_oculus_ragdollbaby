
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable
{

    public Item item;
    public GameObject hand;
    
    public GameObject baby;
    public GameObject babyHand;

    BabyController babyController;
    
    public bool pickedUp = false;
    
    
    public override void Interact()
    {
       // StartCoroutine("WaitForTouch_Player");
        
        if (Inventory.instance.items.Count > 0)
        {
            
            if (Inventory.instance.items[0] != item)
            {
                base.Interact();
                Debug.Log("inv>0 " + Inventory.instance.items[0] + " " + item);
                MayPickUp();
            }
            else
            {
                OnDefocused();
            }
        }
        else
        {
            base.Interact();
            //Debug.Log("inv<0");
            MayPickUp();
        }
        
    }
    
    void Start()
    {
        // hand = GameObject.Find("Hand");
        // baby = GameObject.Find("Baby");
        // babyHand = GameObject.Find("BabyHand");

        if (baby != null)
        {
            babyController = baby.GetComponent<BabyController>();
        }


    }
    
    public void MayPickUp()
    {
        //pickUpButton = GameObject.Find("PickUpButton");
        pickUpButton.GetComponent<PickUp>().SetItem(transform.gameObject);
        pickUpButton.GetComponent<PickUp>().SetImage(item.icon);
        pickUpButton.SetActive(true);
       // pickUpButton.GetComponentInChildren<Image>().sprite = item.icon;
    }
    public void Pickup()
    {
        
        Debug.Log("picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp)
        {
            
            dropButton.SetActive(true);
            //Destroy(gameObject);
        }
        if (item.isBottle)
        {
            GetComponent<Breaking>().InUse(item.amount);
            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = hand.transform;
            transform.position = hand.transform.position;
            transform.rotation = hand.transform.rotation;
            if (babyController.eating)
            {
                babyController.TakeAway();
            }
        }
        if (item.isBottleCrackedUpper || item.isBottleCrackedLower)
        {
            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = hand.transform;
            transform.position = hand.transform.position;
           
        }
        if (item.isBall)
        {
            //GetComponent<SwipeScript>().enabled = true;
            
            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = hand.transform;
            transform.position = hand.transform.position;
            
        }
        if (item.isDiaper)
        {
            //GetComponent<SwipeScript>().enabled = true;

            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = hand.transform;
            transform.position = hand.transform.position;
            
        }
        if (item.isBaby)
        {
            if (GetComponent<ItemPickup>().enabled)
            {
                //GetComponent<CharacterStats>().PickedUp();
                transform.GetComponent<Rigidbody>().useGravity = true;
                transform.GetComponent<Rigidbody>().isKinematic = true;
                baby.GetComponent<BabyController>().BabyPickUp();
                dropButton.SetActive(false);
                // pickUpButton.SetActive(true);
                GetComponent<CharacterStats>().PickedUp();
            }
            //GetComponent<SwipeScript>().enabled = true;

            // transform.GetComponent<Rigidbody>().useGravity = false;
            // transform.GetComponent<Rigidbody>().isKinematic = true;
            // transform.parent = hand.transform;
            // transform.position = hand.transform.position;

        }

    }
    
    public void Drop()
    {
        float distanceToBaby = Vector3.Distance(transform.position, baby.transform.position);
        
        if (distanceToBaby < 2f)
        {
            if (item.isBottle)
            {
                
               // GetComponent<Breaking>().InUse(item.amount);
                transform.GetComponent<Rigidbody>().useGravity = false;
                transform.GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = babyHand.transform;
                transform.position = babyHand.transform.position;
                transform.rotation = babyHand.transform.rotation;
                baby.GetComponent<BabyController>().BabyEating();
            }
            else if (item.isDiaper)
            {
                transform.GetComponent<Rigidbody>().useGravity = true;
                transform.GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                baby.GetComponent<BabyController>().DiaperNew();
                //Destroy(gameObject);
                GetComponent<ReSpawn>().ReSpawing();

            }
            else if (item.isBall)
            {
                transform.GetComponent<Rigidbody>().useGravity = true;
                transform.GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                GetComponent<ImpactReceiver>().AddImpact(Vector3.forward, 125f);
               // Debug.Log("dropball");
            }
            else if (item.isBaby)
            {
                //pickUpButton.SetActive(false);
                transform.GetComponent<Rigidbody>().useGravity = true;
                transform.GetComponent<Rigidbody>().isKinematic = true;
              //  Debug.Log("drop BABY");
                // transform.parent = null;
                // transform.position = hand.transform.position;
               // GetComponent<ItemPickup>().enabled = false;
            }
            else
            {
                transform.GetComponent<Rigidbody>().useGravity = true;
                transform.GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                // transform.position = hand.transform.position;
            }
        }
        else
        {
            if (item.isBottle)
            {
                GetComponent<Breaking>().Dropped();
                // Spawn a shattered object
                
                //Destroy(gameObject);
            }
            transform.GetComponent<Rigidbody>().useGravity = true;
            transform.GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = null;
            // transform.position = hand.transform.position;
        }
        dropButton.SetActive(false);
    }
    public IEnumerator WaitForTouch_Player()
    {

        bool found = false;
        Touch touch;
        Ray screenRay;
        RaycastHit hit;
        string objname;
        
        while (Input.touchCount == 0 || found == false)
        {
           
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touch = Input.GetTouch(0);
                screenRay = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(screenRay, out hit) && touch.tapCount ==2)
                    {
                        found = true;
                        pickedUp = !pickedUp;
                        yield return null;
                        
                        break;
                    
                    }
                
            }
            yield return null;
        }
        if (pickedUp)
        {
            //pickedUp = true;
           

            
            //yield return null;
            Pickup();
            
        }
        else
        {
            //pickedUp = false;
           

            
           // yield return null;
            Drop();
            
        }
        yield return null;
    }
}
