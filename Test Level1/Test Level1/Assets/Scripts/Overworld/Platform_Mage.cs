using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Mage : MonoBehaviour
{



    private GameObject player;


    PlayerController.PlayerClass playerClass;

    private Animator PlatformAnimator;

    // Use this for initialization
    void Start()
    {
        PlatformAnimator = GetComponent<Animator>();

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
            
            player.transform.parent = transform;
            player.GetComponent<PlayerController>().SetJumpTrue();

            playerClass = player.GetComponent<PlayerController>().GetPlayerClass();
            if (playerClass == PlayerController.PlayerClass.Mage)
            {

                PlatformAnimator.SetBool("Move_Plat", true);
                

            }            
        }

    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {

            player.transform.parent = null;
        }

    }



}
