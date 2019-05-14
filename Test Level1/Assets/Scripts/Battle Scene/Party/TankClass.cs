using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankClass : BaseEntityClass {

	// Use this for initialization
	void Start ()
    {
        setIsPlayer(true);
        weaponAttack = 3;
        ID = 4;

        stats.atkAttribute = 8;
        stats.defAttribute = 10;
        stats.vitAttribute = 9;
        stats.dexAttribute = 6;
        stats.intAttribute = 4;

        setEntityType(Types.Player);

        characterJob = "Knight";

        abilityList = Abilities.Sentinel | Abilities.Taunt | Abilities.Shield_Slam;

        spellList = Spells.None;

        setPartyLead(true);
        setBaseHealthManaCost();
    }

    // Update is called once per frame
    void Update ()
    {
        updateMaxHpMpCritChanceDefending();
    }
}
