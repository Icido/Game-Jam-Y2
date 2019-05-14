﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Warps : MonoBehaviour {
    
    public GameObject target;
    public GameObject cameraPosition;


    void Awake()
    {
        // Make sure we drop the object in unity
        Assert.IsNotNull(target);

        // This is for hide the warps sprites in runtime
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            // Update position
            coll.transform.position = target.transform.GetChild(0).transform.position;
            cameraPosition.transform.position = target.transform.GetChild(1).transform.position;
            //col.transform.position = target.transform.position;
        }
    }
}
