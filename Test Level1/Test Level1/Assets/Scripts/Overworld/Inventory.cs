using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject[] inventory = new GameObject[12];
    public Button[] InventoryButtons = new Button[12];
    
    public AudioClip pickupSound;

    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void  AddItem(GameObject item)
    {
        bool itemAdded = false;
        source.PlayOneShot(pickupSound, 1f);
        //Find the first available open space in our inventory
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                //Update inventory UI
                InventoryButtons[i].image.overrideSprite = item.GetComponent<SpriteRenderer>().sprite;
                Debug.Log(item.name + " was added");
                itemAdded = true;
                //interact with Object
                item.SendMessage("Interaction");
                break;
            }

        }

        //Inventory full
        if (!itemAdded)
        {
            Debug.Log("Inventory is Full, can't add item.");
        }

    }

    public GameObject FindItemByType(string itemType, int i)
    {
        //for(int i = 0; i < inventory.Length; i++)
        //{
            if(inventory[i] != null)
            {
                if(inventory[i].GetComponent<InteractableObjects>().itemType == itemType)
                {
                    //we found the item of the type we wanted.
                    return inventory[i];
                }
            }
       // }
        //item of the type was not found
        return null;
    }

    public void RemoveItem(GameObject item, int i)
    { 

        //for (i = 0; i < inventory.Length; i++)
        //{
            if(inventory [i] == item)
            {
                //we found the item, now remove it
                inventory[i] = null;
                Debug.Log(item.name + " was removed from inventory");
                //remove from inventory UI
                InventoryButtons[i].image.overrideSprite = null;
                //break;
            }
        //}
    }

    public bool FindItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                return true;
            }   
        }
        return false;
    }
    
}
