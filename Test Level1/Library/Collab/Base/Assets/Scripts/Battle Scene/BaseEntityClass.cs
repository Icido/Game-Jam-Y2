using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BaseEntityClass : MonoBehaviour
{

    [Serializable]
    public struct Attributes
    {
        public int atkAttribute;
        public int defAttribute;
        public int vitAttribute;
        public int dexAttribute;
        public int intAttribute;
    }

    public Attributes stats;

    public enum Types
    {
        NonExistent,
        Small,
        Medium,
        Large
    };


    public int maxHealth; //ought to be protected but for some reason needs to be public because black boxing == 0
    public int health;

    public int maxMana;
    public int mana;

    public int manaCost;
    public int critChance;

    protected int level = 1;
    protected int experience = 0;
    protected int nextLevelUp = 100;

    protected Types enemyType;


    public bool isPartyLead = false;
    public bool isAlive = true;
    public bool isPlayer = true;
    public bool escapedBattle = false;
    public bool isDefending = false;
    public bool isMyTurn;

    public string characterJob = "";
    public string Weapon = "";
    public int weaponAttack;

    public int attackPriority = 0;
    public int ID;

    //Physical
    [Flags]
    public enum Abilities
    {
        Taunt = 1,
        Sentinel = 2,
        Shield_Slam = 4,
        Shiv = 8,
        Quick_Attack = 16,
        Shadow_Strike = 32,
        Meditate = 64
    };

    //Non-physical aka magical
    [Flags]
    public enum Spells
    {
        Firebolt = 1,
        Flaming_Orb = 2,
        Heal = 4,
        Group_Heal = 8,
        Revive = 16
    };


    #region Abilities

    void tauntEnemy()
    {
        attackPriority = 0;
        manaCost = 1;
        //Increases threat generation by a value (like 500%)
    }

    void sentinelShield(BaseEntityClass jobRole)
    {
        attackPriority = 1;
        manaCost = 2;
        //Block the damage the jobRole will take
    }

    void shieldSlam(BaseEntityClass enemy)
    {
        attackPriority = 0;
        if (UnityEngine.Random.Range(1, 100) <= 90)
        {
            enemy.takeDamage(UnityEngine.Random.Range(7, 11));
        }
        else
        {
            //UI Element displays "attack has missed"
        }
    }

    void Shiv(BaseEntityClass enemy)
    {
        attackPriority = 0;
        if (UnityEngine.Random.Range(1, 100) <= 90)
        {
            if (CriticalHit())
            {
                enemy.takeDamage(2 * (stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(7, 11) - enemy.stats.defAttribute));
            }
            else
            {
                enemy.takeDamage((stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(7, 11) - enemy.stats.defAttribute));
            }
        }

        else
        {
            //UI Element displays "attack has missed"
        }
    }

    void QuickAttack(BaseEntityClass enemy)
    {
        manaCost = 1;
        attackPriority = 1;
        if (UnityEngine.Random.Range(1, 100) <= 90)
        {
            if (CriticalHit())
            {
                enemy.takeDamage(2 * (stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(5, 9) - enemy.stats.defAttribute));
            }
            else
            {
                enemy.takeDamage((stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(5, 9) - enemy.stats.defAttribute));
            }
        }
        else
        {
            //UI Element displays "attack has missed"
        }

    }

    void ShadowStrike(BaseEntityClass enemy)
    {
        manaCost = 2;
        attackPriority = 0;
        if (UnityEngine.Random.Range(1, 100) <= 50)
        {
            enemy.takeDamage(2 * (stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(7, 11) - enemy.stats.defAttribute));
        }
        else
        {
            //UI Element displays "attack has missed"
        }
    }

    public void Meditate()
    {
        attackPriority = 0;

        if (mana < maxMana)
        {
            mana += (stats.intAttribute / 2);
            if (mana > maxMana)
            {
                mana = maxMana;
            }
        }
    }

    #endregion

    #region Spells

    public void fireboltAttack(BaseEntityClass enemy)
    {
        attackPriority = 0;
        manaCost = 2;
        if (mana >= manaCost)
        {
            mana -= manaCost;
            enemy.takeDamage(UnityEngine.Random.Range(7, 11));
        }
    }

    public void flamingOrbAttack(BaseEntityClass enemy1, BaseEntityClass enemy2, BaseEntityClass enemy3, BaseEntityClass enemy4)
    {
        attackPriority = 0;
        manaCost = 10;

        if (mana >= manaCost)
        {
            mana -= manaCost;
            if (UnityEngine.Random.Range(1, 100) <= 90)
            {
                enemy1.takeDamage(UnityEngine.Random.Range(5, 10));
            }
            if (UnityEngine.Random.Range(1, 100) <= 90)
            {
                enemy2.takeDamage(UnityEngine.Random.Range(5, 10));
            }
            if (UnityEngine.Random.Range(1, 100) <= 90)
            {
                enemy3.takeDamage(UnityEngine.Random.Range(5, 10));
            }
            if (UnityEngine.Random.Range(1, 100) <= 90)
            {
                enemy4.takeDamage(UnityEngine.Random.Range(5, 10));
            }
        }
    }


    void Heal(BaseEntityClass jobRole)
    {
        manaCost = 3;
        if (mana >= manaCost)
        {
            mana -= manaCost;
            jobRole.takeDamage(UnityEngine.Random.Range(-20, -15));
        }
    }

    void groupHeal(BaseEntityClass partyMember1, BaseEntityClass partyMember2, BaseEntityClass partyMember3, BaseEntityClass partyMember4)
    {
        manaCost = 10;
        if (mana >= manaCost)
        {
            mana -= manaCost;
            partyMember1.takeDamage(UnityEngine.Random.Range(-15, -5));
            partyMember2.takeDamage(UnityEngine.Random.Range(-15, -5));
            partyMember3.takeDamage(UnityEngine.Random.Range(-15, -5));
            partyMember4.takeDamage(UnityEngine.Random.Range(-15, -5));
        }
    }

    void Revive(BaseEntityClass jobRole)
    {
        manaCost = 5;
        if (mana >= manaCost)
        {
            mana -= manaCost;

            jobRole.health = 0;
            jobRole.takeDamage(-jobRole.maxHealth / 2);
            jobRole.setAlive(true);
        }
    }

    #endregion

    public virtual void Initialise()
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame 
    void Update()
    {

    }

    #region getFunctions

    int getHealth()
    {
        return health;
    }

    int getMana()
    {
        return mana;
    }


    int getLevel()
    {
        return level;
    }

    int getExperiencePoints()
    {
        return experience;
    }

    int getNextLevelUp()
    {
        return nextLevelUp;
    }

    public bool getIsAlive()
    {
        return isAlive;
    }

    bool getPartyLead()
    {
        return isPartyLead;
    }

    public bool getEscapedBattle()
    {
        return escapedBattle;
    }

    public bool getIsMyTurn()
    {
        return isMyTurn;
    }

    public bool getIsDefending()
    {
        return isDefending;
    }

    #endregion

    #region setFunctions

    public void setHealth(int healthValue)
    {
        health = healthValue;
    }

    public void setMana(int manaValue)
    {
        mana = manaValue;
    }

    public void setAlive(bool Alive)
    {
        isAlive = Alive;
    }

    public void setPartyLead(bool partyLead)
    {
        isPartyLead = partyLead;
        //Will need a check later on to make sure there is only one party leader at a time
    }

    public void setEscapedBattle(bool hasEscaped)
    {
        escapedBattle = hasEscaped;
    }

    public void setIsMyTurn(bool isItMyTurn)
    {
        isMyTurn = isItMyTurn;
    }

    public void setIsDefending(bool value)
    {
        isDefending = value;
    }

    #endregion

    #region levelUpStuff  

    void levelUp(BaseEntityClass jobRole) //Job within the brackets
    {
        level++;
        experience = 0;

        jobRole.stats.atkAttribute += UnityEngine.Random.Range(1, 2);
        jobRole.stats.defAttribute += UnityEngine.Random.Range(1, 2);
        jobRole.stats.vitAttribute += UnityEngine.Random.Range(1, 2);
        jobRole.stats.intAttribute += UnityEngine.Random.Range(1, 2);
        jobRole.stats.dexAttribute += UnityEngine.Random.Range(1, 2);

        if (jobRole.characterJob == "Mage")
        {
            jobRole.stats.intAttribute += UnityEngine.Random.Range(3, 5);
        }

        if (jobRole.characterJob == "Tank")
        {
            jobRole.stats.defAttribute += UnityEngine.Random.Range(1, 2);
            jobRole.stats.vitAttribute += UnityEngine.Random.Range(2, 4);
        }
        if (jobRole.characterJob == "Cleric")
        {
            jobRole.stats.intAttribute += UnityEngine.Random.Range(1, 4);
            jobRole.stats.vitAttribute += UnityEngine.Random.Range(1, 2);
        }

        if (jobRole.characterJob == "Rogue")
        {
            jobRole.stats.atkAttribute += UnityEngine.Random.Range(1, 3);
            jobRole.stats.dexAttribute += UnityEngine.Random.Range(2, 3);
        }
    }
    void hasCharacterMetLevelUpRequirements(BaseEntityClass jobRole)
    {
        if (jobRole.experience > jobRole.nextLevelUp)
        {
            int overflow = jobRole.experience - jobRole.nextLevelUp;
            nextLevelUp = nextLevelUp + nextLevelUp / 2;
            levelUp(jobRole);
            jobRole.experience += overflow;
        }
    }

    #endregion

    public void Attack(BaseEntityClass enemy) //Enemy within brackets
    {
        if (UnityEngine.Random.Range(1, 100) <= 90)
        {
            enemy.takeDamage(stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(1, 6) - enemy.stats.defAttribute);
        }
    }

    public bool CriticalHit()
    {
        if (UnityEngine.Random.Range(1, 100) <= critChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Defend()
    {
        setIsDefending(true);
    }

    public void PlayerRunAway(BaseEntityClass e1, BaseEntityClass e2, BaseEntityClass e3, BaseEntityClass e4)
    {
        //random chance to escape (for boss it 0)
        //less members in party increase chance, similarly to fast party members

        //((PlayerLevel + Dex + (1+rand(20)) / partyAlive) > avgThreshold
    }

    public void EnemyRunAway()
    {
        if (enemyType == Types.Small && health <= (maxHealth * 0.2) && UnityEngine.Random.Range(1, 100) <= runawayThreshold(level, stats.dexAttribute))
        {
            escapedBattle = true;
        }
    }

    //Amount of experience to be given to each individual player. Pass this int value to players
    int experienceDrop(int enemyType, int enemyLevel, int partySize)
    {
        return ((enemyType * enemyLevel) / partySize);
    }

    public void takeDamage(int damageVal)
    {
        if (true == isAlive)
        {
            if (true == getIsDefending())
            {
                health -= damageVal / 2;
            }

            else
            {
                health -= damageVal;
            }

            if (health <= 0)
            {
                setAlive(false);
            }
        }
    }

    int runawayThreshold(int enemyLevel, int enemyDex)
    {
        return enemyLevel + enemyDex;
    }

    public void setBaseHealthManaCost()
    {
        maxHealth = 4 * stats.vitAttribute;
        maxMana = 2 * stats.intAttribute;

        health = maxHealth;
        mana = maxMana;

        manaCost = 0;
    }

    public void updateMaxHpMpCritChanceDefending()
    {
        maxHealth = 4 * stats.vitAttribute;
        maxMana = 2 * stats.intAttribute;
        critChance = ((stats.dexAttribute + level) / 10 * (UnityEngine.Random.Range(1, 6)));
        setIsDefending(false);
    }
}
