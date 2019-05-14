using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{


    public GameObject interactObject = null;
    public InteractableObjects interactObjectScript = null;
    public Inventory inventory;
    public GameObject invButton;
    public GameObject inven;
    public bool RemoveitemWait = false;



    void Update()
    {
        if (Input.GetButtonDown("Submit") && interactObject)
        {
            //check if object is to be stored in inventory
            if (interactObjectScript.inventory)
            {
                inventory.AddItem(interactObject);
            }

            //check to see if the object can be opened (door)
            if (interactObjectScript.openable)
            {
                //check if door is locked
                if(interactObjectScript.locked)
                {
                    /////////////////////check to see if we have the needed key to unlock this door/////////////////////
                    ////////////////////search inventory for item needed///////////////////////////////////////////////
                    if (inventory.FindItem(interactObjectScript.itemNeeded))
                    {
                        interactObjectScript.locked = false;
                        Debug.Log(interactObject.name + " was unlocked");

                    } else
                    {
                        Debug.Log(interactObject.name + " was not unlocked");
                    }

                } else

                {
                    Debug.Log(interactObject.name + " is unlocked");
                    interactObjectScript.Open();
                }
            }

        }

        //if (Input.GetButton("Submit") && inven.activeSelf && RemoveitemWait == true) /*&& invButton.activeSelf*/
        //{
        //    //check inventory for a potion
        //    GameObject potion = inventory.FindItemByType("Potion");

        //    if (potion != null)
        //    {
        //        //Use potion and apply its affect

        //        //Delete potion from inventory
        //        inventory.RemoveItem(potion);

        //    }


        //    RemoveitemWait = false;


        //}
        //else if (Input.GetButtonUp("Submit") && inven.activeSelf && RemoveitemWait == false)
        //{

        //    RemoveitemWait = true;

        //}

        if (Input.GetButton("Back"))
        {
            RemoveitemWait = false;
        }
        //if (Input.GetButton("Use Potion"))
        //{
        //    //check inventory for a potion
        //    GameObject potion = inventory.FindItemByType("Mana Potion");
        //    if (potion != null)
        //    {
        //        //Use potion and apply its affect

        //        //Delete potion from inventory
        //        inventory.RemoveItem(potion);
        //    }





        //}


    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("interactableObject"))
        {
            Debug.Log(other.name);
            interactObject = other.gameObject;
            interactObjectScript = interactObject.GetComponent<InteractableObjects>();
            ////////////////PLAYER SWITCH//////////////////////////////////////////
            //GameObject players = GameObject.Find("Players");
            //players.GetComponent<Swap_Player>().SetInteractableItemTrue();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("interactableObject"))
        {
            if (other.gameObject == interactObject)
            {
                interactObject = null;
            }
            ////////////////PLAYER SWITCH//////////////////////////////////////////
            //GameObject players = GameObject.Find("Players");
            //players.GetComponent<Swap_Player>().SetInteractableItemFalse();

        }
    }

}