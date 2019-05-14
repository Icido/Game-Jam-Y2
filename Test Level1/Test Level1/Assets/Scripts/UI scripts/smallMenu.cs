using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class smallMenu : MonoBehaviour {

    public Button party;


    public GameObject partyButton;
    public GameObject exitMenu;
    public GameObject invButton;
    public GameObject exitGame;

    public GameObject radialOn;

    public GameObject battlecam;

    public GameObject inventory;
    public GameObject partyHUD;


    public bool menuActive = false;


    void Update()
    { if (!battlecam.activeSelf)
        {
            if (Input.GetKeyDown("joystick button 8")  || Input.GetKeyDown("escape")) //button 8 is left stick down, 9 is right stick down - remove escape functionality for final version
            {
                openMenu();
            }
        }
    }


    public void openMenu()
    {
        if (menuActive == false)
        {
            party.Select();
            party.OnSelect(null);

            radialOn.SetActive(true);
            partyButton.SetActive(true);
            invButton.SetActive(true);
            exitMenu.SetActive(true);
            exitGame.SetActive(true);

            Time.timeScale = 0.0f;

            menuActive = true;
        }

        else if (menuActive == true && !inventory.activeSelf && !partyHUD.activeSelf)
        {
            radialOn.SetActive(false);
            invButton.SetActive(false);
            partyButton.SetActive(false);
            exitMenu.SetActive(false);
            exitGame.SetActive(false);


            Time.timeScale = 1.0f;

            menuActive = false;
        }
    }

}
