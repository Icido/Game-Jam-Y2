using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryOpen : MonoBehaviour
{

    public Button inv1;
    public Button knight;
    public Button party;
    public GameObject partyHUD;
    public GameObject inventory;
    public GameObject invButton;
    public GameObject battlecam;

    public AudioClip openSound;

    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        //if (!battlecam.activeSelf && invButton.activeSelf)
        //{
        //    if (Input.GetKeyDown("joystick button 9")) //button 8 is left stick down, 9 is right stick down - remove escape functionality for final version
        //    {
        //        openInv();
        //    }
        //}

        if (!battlecam.activeSelf && inventory.activeSelf)
        {
            if (Input.GetKeyDown("joystick button 8")) //button 8 is left stick down, 9 is right stick down - remove escape functionality for final version
            {
                closeInv();
            }
        }

        if (!battlecam.activeSelf && partyHUD.activeSelf)
        {
            if (Input.GetKeyDown("joystick button 8")) //button 8 is left stick down, 9 is right stick down - remove escape functionality for final version
            {
                closeParty();
            }
        }
    }

    public void openInv()
    {
        source.PlayOneShot(openSound, 1f);
        inventory.SetActive(true);
        inv1.Select();
        inv1.OnSelect(null);

    }

    public void closeInv()
    {
        party.Select();
        party.OnSelect(null);

        inventory.SetActive(false);

    }

    public void openParty()
    {
        partyHUD.SetActive(true);

        knight.Select();
        knight.OnSelect(null);
    }

    public void closeParty()
    {
        party.Select();
        party.OnSelect(null);

        partyHUD.SetActive(false);

    }
}