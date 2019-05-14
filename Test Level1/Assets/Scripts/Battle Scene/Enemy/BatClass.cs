using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatClass : BaseEntityClass
{
	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update()
    {
        updateMaxHpMpCritChanceDefending();
    }

    public override void Initialise()
    {
        weaponAttack = 1;

        stats.atkAttribute = 4;
        stats.defAttribute = 2;
        stats.vitAttribute = 3;
        stats.dexAttribute = 50;
        stats.intAttribute = 1;

        setEntityType(Types.Bat);

        characterJob = "Bat";

        abilityList = Abilities.None;

        spellList = Spells.None;

        setBaseHealthManaCost();
    }
}
