using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour {

    //if true, this object can be added to inventory
    public bool inventory;
    public string itemType;//This is to tell what type of item the object is.
    public bool openable;
    public bool locked;
    public GameObject itemNeeded;

    public void Interaction()
    {
        //adding to inventroy
        gameObject.SetActive(false);
    }

    public void Open()
    {
        Destroy(gameObject);
    }

}

