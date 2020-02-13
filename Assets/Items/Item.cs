
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public AudioClip audio = null;
    public bool isDefaultItem = false;
    
    public float amount;

    public bool isBottle = false;
    public bool isBottleCracked = false;
    public bool isBottleCrackedUpper = false;
    public bool isBottleCrackedLower = false;
    public bool isBall = false;
    public bool isBaby = false;
    public bool isDiaper = false;

    public virtual void Use()
    {
        
        Debug.Log("use"+ name);
    }
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
        
    }
    
}
