using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonClass : BaseEntityClass {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        updateMaxHpMpCritChanceDefending();
    }

    public override void Initialise()
    {
        weaponAttack = 1;

        stats.atkAttribute = 8;
        stats.defAttribute = 5;
        stats.vitAttribute = 9;
        stats.dexAttribute = 6;
        stats.intAttribute = 12;

        setEntityType(Types.Demon);

        characterJob = "Demon";

        abilityList = Abilities.None;

        spellList = Spells.Firebolt | Spells.Flaming_Orb;

        setBaseHealthManaCost();
    }
}
