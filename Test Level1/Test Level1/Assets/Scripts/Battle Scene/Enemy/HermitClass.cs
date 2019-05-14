using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermitClass : BaseEntityClass
{

    void Start()
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

        stats.atkAttribute = 1;
        stats.defAttribute = 2;
        stats.vitAttribute = 4;
        stats.dexAttribute = 3;
        stats.intAttribute = 7;

        setEntityType(Types.Hermit);

        characterJob = "Hermit";

        abilityList = Abilities.None;

        spellList = Spells.Firebolt | Spells.Heal;

        setBaseHealthManaCost();
    }
}
