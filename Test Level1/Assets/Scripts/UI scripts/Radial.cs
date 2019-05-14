using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radial : MonoBehaviour {
    float horizontalPos = 0;
    float verticalPos = 0;

    public GameObject Inventory;
    public GameObject PartyUI;

    Button thingUP;
    Button thingRight;
    Button thingDown;
    Button thingLeft;

    public GameObject Pointer;
    // Update is called once per frame
    void Update()
    {
        float horizontalPos = Input.GetAxis("Horizontal");
        float verticalPos = Input.GetAxis("Vertical");

        //Debug.Log("Im Working");
        //Debug.Log(horizontalPos);
        //Debug.Log(verticalPos);
        if (!Pointer.activeSelf)
        {
            //Top
            if (horizontalPos > -0.3 && horizontalPos < 0.3 && verticalPos > 0.18 && !Inventory.activeSelf && !PartyUI.activeSelf)
            {
                //  Debug.Log("Top Active");
                thingUP = GameObject.FindGameObjectWithTag("Up").GetComponent<Button>();
                thingUP.Select();

            }

            //Right
            if (horizontalPos > 0.18 && verticalPos < 0.3 && verticalPos > -0.3 && !Inventory.activeSelf && !PartyUI.activeSelf)
            {
                // Debug.Log("Right Active");
                thingRight = GameObject.FindGameObjectWithTag("Right").GetComponent<Button>();
                thingRight.Select();

            }

            // Left
            if (horizontalPos < -0.18 && verticalPos > -0.3 && verticalPos < 0.3 && !Inventory.activeSelf && !PartyUI.activeSelf)
            {
                //  Debug.Log("Left Active");
                thingLeft = GameObject.FindGameObjectWithTag("Left").GetComponent<Button>();
                thingLeft.Select();

            }

            //Bot
            if (horizontalPos < 0.3 && horizontalPos > -0.3 && verticalPos < -0.18 && !Inventory.activeSelf && !PartyUI.activeSelf)
            {
                thingDown = GameObject.FindGameObjectWithTag("Down").GetComponent<Button>();
                thingDown.Select();

                //  Debug.Log("Bot Active");
            }

            //dz
            if (horizontalPos < 0.18 && horizontalPos > -0.18 && verticalPos < 0.18 && verticalPos > -0.18)
            {
                //  Debug.Log("DeadZone Active");
            }
        }
    }

}

