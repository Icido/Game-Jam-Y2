using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{

    private Rigidbody2D platformRigibody;

    private GameObject player;

    PlayerController.PlayerClass playerClass;

    // Use this for initialization
    void Start()
    {
        platformRigibody = GetComponent<Rigidbody2D>();

        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            playerClass = player.GetComponent<PlayerController>().GetPlayerClass();
            if (playerClass == PlayerController.PlayerClass.Warrior)
            {
                platformRigibody.bodyType = RigidbodyType2D.Dynamic;

                //platformRigibody.gravityScale = 1f;
            }

        }

    }





    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            platformRigibody.velocity = new Vector2(0, platformRigibody.velocity.y);
            

        }
    }



   
}
