using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleHealthBars : MonoBehaviour {

    public GameObject Character;
    public Slider healthBar;
    public GameObject characterIcon;

	// Use this for initialization
	void Start ()
    {
     
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthBar.maxValue = Character.GetComponent<BaseEntityClass>().maxHealth;
        healthBar.minValue = 0;
        healthBar.value = Character.GetComponent<BaseEntityClass>().health;

        if (healthBar.value <= healthBar.minValue)
        {
            characterIcon.GetComponent<Image>().color = Color.red;
        }
        else
        {
            characterIcon.GetComponent<Image>().color = Color.white;
        }
	}
}
