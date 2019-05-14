using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageClass : BaseEntityClass {

	// Use this for initialization
	void Start ()
    {
        setIsPlayer(true);
        weaponAttack = 1;
        ID = 1;

        stats.atkAttribute = 4;
        stats.defAttribute = 6;
        stats.vitAttribute = 8;
        stats.dexAttribute = 9;
        stats.intAttribute = 10;

        setEntityType(Types.Player);

        characterJob = "Mage";

        abilityList = Abilities.Meditate;

        //spellList = Spells.Firebolt;
        spellList = Spells.Firebolt | Spells.Flaming_Orb;

        setBaseHealthManaCost();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        updateMaxHpMpCritChanceDefending();
    }
}
