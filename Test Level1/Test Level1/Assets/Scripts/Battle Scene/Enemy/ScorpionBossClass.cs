using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionBossClass : BaseEntityClass
{

    // Use this for initialization
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

        stats.atkAttribute = 7;
        stats.defAttribute = 5;
        stats.vitAttribute = 10;
        stats.dexAttribute = 2;
        stats.intAttribute = 1;

        setEntityType(Types.Boss);

        characterJob = "Stingclaw";

        abilityList = Abilities.Poison_Sting;

        spellList = Spells.None;

        setBaseHealthManaCost();
    }
}

