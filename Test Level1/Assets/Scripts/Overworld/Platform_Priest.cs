using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Priest : MonoBehaviour {

    private Rigidbody2D platformRigibody;

    private GameObject player;

    PlayerController.PlayerClass playerClass;

    private Vector3 currentPosition;

    // Use this for initialization
    void Start () {
        platformRigibody = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");

        currentPosition = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
       
    }



    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            playerClass = player.GetComponent<PlayerController>().GetPlayerClass();
            if(playerClass != PlayerController.PlayerClass.Priest)
            {
                platformRigibody.bodyType = RigidbodyType2D.Dynamic;
                Invoke("RestartPosition", 2);

                //platformRigibody.gravityScale = 1f;
            }
            else
            {
                player.GetComponent<PlayerController>().SetJumpTrue();
            }


        }

    }

    void RestartPosition()
    {
        platformRigibody.bodyType = RigidbodyType2D.Kinematic;
        transform.position = currentPosition;
    }
}

