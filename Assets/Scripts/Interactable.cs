
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject dropButton;
    public GameObject pickUpButton;

    public float radius = 3f;
    public Transform interactionTransform;
    
    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;

    public virtual void Interact()
    {
       // Debug.Log("interacting " + transform.name);
    }
    void Start()
    {
       // dropButton = GameObject.Find("DropButton");
       // pickUpButton = GameObject.Find("PickUpButton");
        pickUpButton.SetActive(false);
        dropButton.SetActive(false);
    }
    void Update()
    {
        if (Inventory.instance.items.Count == 0)
        {
            if (isFocus && !hasInteracted)
            {
                float distance = Vector3.Distance(player.position, interactionTransform.position);

                if (distance <= radius)
                {
                    if (transform.name == "Baby")
                    {
                        if (transform.GetComponent<BabyController>().sleeping  || transform.GetComponent<BabyController>().tired || transform.GetComponent<CharacterStats>().inBed)
                        {
                            pickUpButton.SetActive(true);
                            Interact();
                        }
                    }
                    else
                    {
                        pickUpButton.SetActive(true);
                        Interact();
                        // Debug.Log("Interact");
                        //hasInteracted = true;
                    }
                }
                else
                {
                    pickUpButton.SetActive(false);
                }


            }
            
        }
        else
        {

            pickUpButton.SetActive(false);

        }
    }
    public void OnFocused (Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}
