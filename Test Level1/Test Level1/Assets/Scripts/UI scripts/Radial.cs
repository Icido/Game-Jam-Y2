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
    public GameObject Pointer2;
    public GameObject Pointer3;
    public GameObject Pointer4;
    void Update()
    {
        float horizontalPos = Input.GetAxis("Horizontal");
        float verticalPos = Input.GetAxis("Vertical");

        //Debug.Log("Im Working");
        //Debug.Log(horizontalPos);
        //Debug.Log(verticalPos);
        if(Pointer.active == false && Pointer2.active == false && Pointer3.active == false && Pointer4.active == false)
        { 
            //Top
            if (horizontalPos > -0.3 && horizontalPos < 0.3 && verticalPos > 0.18 && Inventory.active == false && PartyUI.active == false)
            {
                //Debug.Log("Top Active");
                thingUP = GameObject.FindGameObjectWithTag("Up").GetComponent<Button>();
                thingUP.Select();
                thingUP.OnSelect(null);
            }

            //Right
            if (horizontalPos > 0.18 && verticalPos < 0.3 && verticalPos > -0.3 && Inventory.active == false && PartyUI.active == false)
            {
               // Debug.Log("Right Active");
                thingRight = GameObject.FindGameObjectWithTag("Right").GetComponent<Button>();
                thingRight.Select();
                thingRight.OnSelect(null);

            }

            //Bot
            if (horizontalPos < 0.3 && horizontalPos > -0.3 && verticalPos < -0.18 && Inventory.active == false && PartyUI.active == false)
            {
                thingDown = GameObject.FindGameObjectWithTag("Down").GetComponent<Button>();
                thingDown.Select();
                thingDown.OnSelect(null);

              //  Debug.Log("Bot Active");

            }

            // Left
            if (horizontalPos < -0.18 && verticalPos > -0.3 && verticalPos < 0.3 && Inventory.active == false && PartyUI.active == false)
            {
              //  Debug.Log("Left Active");
                thingLeft = GameObject.FindGameObjectWithTag("Left").GetComponent<Button>();
                thingLeft.Select();
                thingLeft.OnSelect(null);
            }
}
            //dz
            //if (horizontalPos < 0.18 && horizontalPos > -0.18 && verticalPos < 0.18 && verticalPos > -0.18)
            //{
            //    Debug.Log("DeadZone Active");
            //}
       
    }

}

