using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ClericClass : BaseEntityClass {

    //public GameObject battleManager;

    // Use this for initialization
    void Start ()
    {
        setIsPlayer(true);
        weaponAttack = 2;
        ID = 2;

        stats.atkAttribute = 6;
        stats.defAttribute = 9;
        stats.vitAttribute = 8;
        stats.dexAttribute = 4;
        stats.intAttribute = 10;

        setEntityType(Types.Player);

        characterJob = "Cleric";

        abilityList = Abilities.Meditate;

        spellList = Spells.Heal | Spells.Group_Heal | Spells.Revive | Spells.Smite;
        //spellList = Spells.Heal | Spells.Group_Heal | Spells.Revive;

        setBaseHealthManaCost();
    }

    // Update is called once per frame
    void Update ()
    {
        updateMaxHpMpCritChanceDefending();

        //if (Input.GetKeyDown("o"))
        //{
        //    int tempPID;
        //    tempPID = GameObject.Find("tankBattleCharacter").GetComponent<TankClass>().pID;
        //    GameObject.Find("tankBattleCharacter").GetComponent<TankClass>().pID = this.pID;
        //    this.pID = tempPID;
        //    GameObject.Find("tankBattleCharacter").GetComponent<TankClass>().setPartyLead(false);
        //    this.setPartyLead(true);
        //    battleManager.GetComponent<BattleScript>().StartBattle();
        //}
        //Switching the party positions and changing the party leader
    }


}
