using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonClass : BaseEntityClass
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
        weaponAttack = 2;

        stats.atkAttribute = 5;
        stats.defAttribute = 2;
        stats.vitAttribute = 4;
        stats.dexAttribute = 3;
        stats.intAttribute = 1;

        setEntityType(Types.Skeleton);

        characterJob = "Skeleton";

        abilityList = Abilities.Shiv;

        spellList = Spells.None;

        setBaseHealthManaCost();
    }
}
