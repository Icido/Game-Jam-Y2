using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {

    private GameObject player;

    // Use this for initialization
    void Start () {
         player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwapPlayer(int newPlayer)
    {
        player.GetComponent<PlayerController>().ChangePlayer(newPlayer);
    }
}
