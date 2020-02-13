using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public void Drop()
    {
        Inventory.instance.items[0].RemoveFromInventory();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Drop();
        }
    }
}
