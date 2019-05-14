using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseOnClick : MonoBehaviour {

    public Inventory inventory;

    public GameObject HeroSelect;

    int saveButton;
    int buttonNumber;

    GameObject potion;

    public Button knightButton;
    public Button inv1;

    public GameObject knight;
    public GameObject mage;
    public GameObject cleric;
    public GameObject rogue;
   

    public void UseItem(int buttonNumber)
    {
        GameObject potion = inventory.FindItemByType("Potion", buttonNumber);

        if (potion != null)
        { 

            HeroSelect.SetActive(true);

            knightButton.Select();
            saveButton = buttonNumber;

        }
    }

    public void healKnight()

    {
        buttonNumber = saveButton;
        GameObject potion = inventory.FindItemByType("Potion", buttonNumber);
        knight.GetComponent<BaseEntityClass>().healDamage(20);
        inventory.RemoveItem(potion, buttonNumber);
        HeroSelect.SetActive(false);
        inv1.Select();
    }

    public void healCleric()
    {
        buttonNumber = saveButton;
        GameObject potion = inventory.FindItemByType("Potion", buttonNumber);
        cleric.GetComponent<BaseEntityClass>().healDamage(20);
        inventory.RemoveItem(potion, buttonNumber);
        HeroSelect.SetActive(false);
        inv1.Select();
    }
    public void healRogue()
    {
        buttonNumber = saveButton;
        GameObject potion = inventory.FindItemByType("Potion", buttonNumber);
        rogue.GetComponent<BaseEntityClass>().healDamage(20);
        inventory.RemoveItem(potion, buttonNumber);
        HeroSelect.SetActive(false);
        inv1.Select();
    }
    public void healMage()
    {
        buttonNumber = saveButton;
        GameObject potion = inventory.FindItemByType("Potion", buttonNumber);      
        mage.GetComponent<BaseEntityClass>().healDamage(20);
        inventory.RemoveItem(potion, buttonNumber);
        HeroSelect.SetActive(false);
        inv1.Select();
    }
}


