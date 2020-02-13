using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public GameObject item;
    public Image image;

    public void SetImage(Sprite icon)
    {
        image.GetComponentInChildren<Image>().sprite = icon;
    }
    public void SetItem(GameObject item1)
    {
        item = item1;
        
    }
    public void PickUpItem()
    {
        item.GetComponent<ItemPickup>().Pickup();
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if(item != null)
            PickUpItem();
        }
    }
}
