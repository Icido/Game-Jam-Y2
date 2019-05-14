using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSpap : MonoBehaviour {

    public Button SpellSelInMenu;
    public Button SpellStandard;
    public GameObject spell1;
    public GameObject spell2;
    public GameObject spell3;
    public GameObject spell4;
    public GameObject Spells;
    public GameObject Items;
    public GameObject Run;
    public GameObject Attack;

    public void pushSwap()
    {
        if (Input.GetButtonDown("Submit"))
        {
            spell1.SetActive(true);
            spell2.SetActive(true);
            spell3.SetActive(true);
            spell4.SetActive(true);

            SpellSelInMenu.Select();
            SpellSelInMenu.OnSelect(null);

            Attack.SetActive(false);
            Spells.SetActive(false);
            Items.SetActive(false);
            Run.SetActive(false);
        }

    }
}
