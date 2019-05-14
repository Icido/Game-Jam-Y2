using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueClass : BaseEntityClass {

	// Use this for initialization
	void Start ()
    {
        setIsPlayer(true);
        weaponAttack = 4;
        ID = 3;

        stats.atkAttribute = 9;
        stats.defAttribute = 4;
        stats.vitAttribute = 8;
        stats.dexAttribute = 10;
        stats.intAttribute = 6;

        setEntityType(Types.Player);

        characterJob = "Rogue";

        abilityList = Abilities.Quick_Attack | Abilities.Shadow_Strike | Abilities.Shiv;

        spellList = Spells.None;

        setBaseHealthManaCost();
    }


    // Update is called once per frame
    void Update ()
    {
        updateMaxHpMpCritChanceDefending();
    }
}
