using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 2;  // Amount of slots in inventory

    // Current list of items in inventory
    public List<Item> items = new List<Item>();
    
    
    private void Start()
    {
        
    }

    
    // Add a new item. If there is enough room we
    // return true. Else we return false.
   
    public bool Add(Item item)
    {
        items.Add(item);
        if (item.isBottle)
        {
            
            return true;

        }
        if (item.isBottleCracked)
        {

            return true;

        }
        if (item.isBall)
        {

            return true;

        }
        if (item.isDiaper)
        {

            return true;

        }
        if (item.isBaby)
        {

            return false;

        }
        if (!item.isDefaultItem)
        {
            
            // Check if out of space
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            
            // Trigger callback
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return false;
    }

    // Remove an item
    public void Remove(Item item)
    {
        if (item.isBottle)
        {

            GameObject.Find("Bottle").GetComponent<ItemPickup>().Drop();

        }
        if (item.isBottleCrackedUpper)
        {

            GameObject.Find("Upper").GetComponent<ItemPickup>().Drop();

        }
        if (item.isBottleCrackedLower)
        {

            GameObject.Find("Lower").GetComponent<ItemPickup>().Drop();

        }
        if (item.isBall)
        {

            GameObject.Find("Ball").GetComponent<ItemPickup>().Drop();

        }
        if (item.isBaby)
        {

            GameObject.Find("Baby").GetComponent<ItemPickup>().Drop();

        }
        if (item.isDiaper)
        {

            GameObject.Find("DiaperItem").GetComponent<ItemPickup>().Drop();

        }
        items.Remove(item);     // Remove item from list

        // Trigger callback
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}
