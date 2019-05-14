using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSwap : MonoBehaviour {

    public Button attackSelInMenu;
    public Button AttackStandard;
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;
    public GameObject attack4;
    public GameObject Spells;
    public GameObject Items;
    public GameObject Run;
    public GameObject Attack;

    public void pushSwap()
    {
        if (Input.GetButtonDown("Submit"))
        {
            attack1.SetActive(true);
            attack2.SetActive(true);
            attack3.SetActive(true);
            attack4.SetActive(true);

            attackSelInMenu.Select();
            attackSelInMenu.OnSelect(null);

            Attack.SetActive(false);
            Spells.SetActive(false);
            Items.SetActive(false);
            Run.SetActive(false);
        }

    }

}


