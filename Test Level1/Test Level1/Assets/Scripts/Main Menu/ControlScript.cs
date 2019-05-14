using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ControlScript : MonoBehaviour {

    public EventSystem myEvents;
    public GameObject newGame;
    public GameObject loadGame;
    public GameObject controlButton;
    public GameObject exitGame;

    public GameObject back;

    public GameObject title;

    public GameObject controller;

    public void menuSwap()
    {
        newGame.SetActive(false);
        loadGame.SetActive(false);
        controlButton.SetActive(false);
        exitGame.SetActive(false);

        back.SetActive(true);
        controller.SetActive(true);
        myEvents.SetSelectedGameObject(back);
        

        title.SetActive(false);
    }

    public void swapBack()
    {
        newGame.SetActive(true);
        loadGame.SetActive(true);
        controlButton.SetActive(true);
        exitGame.SetActive(true);

        back.SetActive(false);
        controller.SetActive(false);
        myEvents.SetSelectedGameObject(newGame);
       

        title.SetActive(true);
    }

}
