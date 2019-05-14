using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleManaBars : MonoBehaviour {

    public GameObject Character;
    public Slider manaBar;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        manaBar.maxValue = Character.GetComponent<BaseEntityClass>().maxMana;
        manaBar.minValue = 0;
        manaBar.value = Character.GetComponent<BaseEntityClass>().mana;
    }
}
