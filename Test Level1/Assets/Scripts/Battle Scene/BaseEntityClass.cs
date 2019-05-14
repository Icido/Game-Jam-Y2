using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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
        NonExistent = 0,
        Bat = 35,
        Skeleton = 50,
        Hermit = 65,
        Boss = 600,
        Player
    };


    public int maxHealth; //ought to be protected but for some reason needs to be public because black boxing == 0
    public int health;

    public int maxMana;
    public int mana;

    public int manaCost;
    public int critChance;

    public int damage, damage1, damage2, damage3, damage4 = 0;
    public int heal, heal1, heal2, heal3, heal4 = 0;

    public int manaRegen = 0;

    public int level = 1;
    public int experience = 0;
    public int nextLevelUp = 100;

    protected Types entityType = Types.NonExistent;

    public bool isPartyLead = false;
    public bool isAlive = true;
    public bool isPlayer = false;
    public bool escapedBattle = false;
    public bool isDefending = false;
    public bool isMyTurn;
    public bool isTaunted = false;
    public bool isSentinel = false;

    public string characterJob = "";
    public string Weapon = "";
    public int weaponAttack;

    public int attackPriority = 0;
    public int tauntCounter = 0;
    public int aggroValue = 10;
    public int ID;

    public Text logText;

    public List<string> enemySpellList;
    public List<string> enemyAbilityList;
    public int enemyTempSpells;
    public int enemyTempAbilities;

    public GameObject tauntingEnemy;

    //Physical
    [Flags]
    public enum Abilities
    {
        None = 0,
        Taunt = 1,
        Sentinel = 2,
        Shield_Slam = 4,
        Shiv = 8,
        Quick_Attack = 16,
        Shadow_Strike = 32,
        Meditate = 64,
        Poison_Sting = 128
    };

    //Non-physical aka magical
    [Flags]
    public enum Spells
    {
        None = 0,
        Firebolt = 1,
        Flaming_Orb = 2,
        Heal = 4,
        Group_Heal = 8,
        Revive = 16,
        Smite = 32
    };

    public enum SkillCosts
    {
        Taunt = 1,
        Sentinel = 2,
        Shield_Slam = 0,
        Shiv = 0,
        Quick_Attack = 1,
        Shadow_Strike = 2,
        Meditate = 0,
        Poison_Sting = 0,
        Firebolt = 2,
        Flaming_Orb = 10,
        Heal = 3,
        Group_Heal = 10,
        Revive = 5,
        Smite = 2
    }

    public Spells spellList = Spells.None;
    public Abilities abilityList = Abilities.None;

    #region Abilities

    public void Taunt(BaseEntityClass enemy)
    {
        attackPriority = 0;
        manaCost = SkillCosts.Taunt.GetHashCode();
        //Increases threat generation by a value (like 500%)

        if (UnityEngine.Random.Range(1, 100) <= 90)
        {
            enemy.setIsTaunted(true);
            enemy.setTauntedCounter();
            enemy.setTauntingEnemy(this.gameObject);
            Debug.Log(this + " used Taunt on " + enemy);

        }
    }

    public void Sentinel(BaseEntityClass dummyEntity)
    {
        attackPriority = 1;
        manaCost = SkillCosts.Sentinel.GetHashCode();

        setIsSentinel(true);

        Debug.Log(this + " used Sentinel");
    }

    public void Shield_Slam(BaseEntityClass enemy)
    {
        attackPriority = 0;
        manaCost = SkillCosts.Shield_Slam.GetHashCode();
        critChance = ((stats.dexAttribute + level) / 10 * (UnityEngine.Random.Range(1, 6)));

        damage = 0;

        if (UnityEngine.Random.Range(1, 100) <= 90)
        {
            damage = UnityEngine.Random.Range(7, 11);

            Debug.Log(this + " attacked " + enemy + " with Shield Slam, dealing " + damage);

            enemy.takeDamage(damage);
        }
    }

    public void Shiv(BaseEntityClass enemy)
    {
        attackPriority = 0;
        manaCost = SkillCosts.Shiv.GetHashCode();
        critChance = ((stats.dexAttribute + level) / 10 * (UnityEngine.Random.Range(1, 6)));

        damage = 0;

        if (UnityEngine.Random.Range(1, 100) <= 90)
        {
            if (CriticalHit())
            {
                damage = 2 * (stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(7, 11) - enemy.stats.defAttribute);

                Debug.Log(this + " attacked " + enemy + " with Shiv, dealing " + damage);

                enemy.takeDamage(damage);
            }
            else
            {
                damage = (stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(7, 11) - enemy.stats.defAttribute);

                Debug.Log(this + " attacked " + enemy + " with Shiv, dealing " + damage);

                enemy.takeDamage(damage);
            }
        }
    }

    public void Quick_Attack(BaseEntityClass enemy)
    {
        manaCost = SkillCosts.Quick_Attack.GetHashCode();
        attackPriority = 1;
        critChance = ((stats.dexAttribute + level) / 10 * (UnityEngine.Random.Range(1, 6)));

        damage = 0;

        if (UnityEngine.Random.Range(1, 100) <= 90)
        {
            if (CriticalHit())
            {
                damage = 2 * (stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(5, 9) - enemy.stats.defAttribute);

                Debug.Log(this + " attacked " + enemy + " with Quick Attack, dealing " + damage);

                enemy.takeDamage(damage);
            }
            else
            {
                damage = (stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(5, 9) - enemy.stats.defAttribute);

                Debug.Log(this + " attacked " + enemy + " with Quick Attack, dealing " + damage);

                enemy.takeDamage(damage);
            }
        }
    }

    public void Shadow_Strike(BaseEntityClass enemy)
    {
        manaCost = SkillCosts.Shadow_Strike.GetHashCode();
        attackPriority = 0;
        critChance = ((stats.dexAttribute + level) / 10 * (UnityEngine.Random.Range(1, 6)));

        damage = 0;
        if (UnityEngine.Random.Range(1, 100) <= 50)
        {
            damage = 2 * (stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(7, 11) - enemy.stats.defAttribute);

            Debug.Log(this + " attacked " + enemy + " with Shadow Strike, dealing " + damage);

            enemy.takeDamage(damage);
        }
    }

    public void Meditate(BaseEntityClass enemy)
    {
        attackPriority = 0;
        manaCost = SkillCosts.Meditate.GetHashCode();
        manaRegen = 0;

        if (mana < maxMana)
        {
            manaRegen = (stats.intAttribute / 2);
            mana += manaRegen;
            if (mana > maxMana)
            {
                mana = maxMana;
            }
        }

        Debug.Log(this + " used Meditate, regaining " + manaRegen);
    }

    public void Poison_Sting(BaseEntityClass enemy)
    {
        attackPriority = 0;
        manaCost = SkillCosts.Poison_Sting.GetHashCode();

        damage = 0;
        if (UnityEngine.Random.Range(1, 100) <= 80)
        {
            damage = (2 * stats.atkAttribute + UnityEngine.Random.Range(3, 7)) - enemy.stats.defAttribute;

            Debug.Log(this + " attacked " + enemy + " with Poison Sting, dealing " + damage);

            enemy.takeDamage(damage);
        }
    }

    #endregion

    #region Spells

    public void Firebolt(BaseEntityClass enemy)
    {
        attackPriority = 0;
        manaCost = SkillCosts.Firebolt.GetHashCode();
        critChance = ((stats.dexAttribute + level) / 10 * (UnityEngine.Random.Range(1, 6)));

        damage = 0;

        if (mana >= manaCost)
        {
            mana -= manaCost;
            damage = UnityEngine.Random.Range(7, 11);

            Debug.Log(this + " attacked " + enemy + " with Firebolt, dealing " + damage);

            enemy.takeDamage(damage);
        }

    }

    public void Flaming_Orb(BaseEntityClass enemy1, BaseEntityClass enemy2, BaseEntityClass enemy3, BaseEntityClass enemy4)
    {
        attackPriority = 0;
        manaCost = SkillCosts.Flaming_Orb.GetHashCode();
        critChance = ((stats.dexAttribute + level) / 10 * (UnityEngine.Random.Range(1, 6)));

        damage1 = 0;
        damage2 = 0;
        damage3 = 0;
        damage4 = 0;

        if (mana >= manaCost)
        {
            mana -= manaCost;
            if (UnityEngine.Random.Range(1, 100) <= 90)
            {
                damage1 = UnityEngine.Random.Range(5, 10);
                enemy1.takeDamage(damage1);
            }
            if (UnityEngine.Random.Range(1, 100) <= 90)
            {
                damage2 = UnityEngine.Random.Range(5, 10);
                enemy2.takeDamage(damage2);
            }
            if (UnityEngine.Random.Range(1, 100) <= 90)
            {
                damage3 = UnityEngine.Random.Range(5, 10);
                enemy3.takeDamage(damage3);
            }
            if (UnityEngine.Random.Range(1, 100) <= 90)
            {
                damage4 = UnityEngine.Random.Range(5, 10);
                enemy4.takeDamage(damage4);
            }

            Debug.Log(this + " attacked with Flaming Orb, dealing " + damage1 + " to " + enemy1 + ", " + damage2 + " to " + enemy2 + ", "
                        + damage3 + " to " + enemy3 + ", " + damage4 + " to " + enemy4);
        }
    }


    public void Heal(BaseEntityClass jobRole)
    {
        manaCost = SkillCosts.Heal.GetHashCode();
        critChance = ((stats.dexAttribute + level) / 10 * (UnityEngine.Random.Range(1, 6)));

        heal = 0;

        if (mana >= manaCost && jobRole.getIsAlive() == true)
        {
            mana -= manaCost;

            heal = UnityEngine.Random.Range(15, 20);

            Debug.Log(this + " healed " + jobRole + " with Heal, healing " + heal);

            jobRole.healDamage(heal);
        }

    }

    public void Group_Heal(BaseEntityClass partyMember1, BaseEntityClass partyMember2, BaseEntityClass partyMember3, BaseEntityClass partyMember4)
    {
        manaCost = SkillCosts.Group_Heal.GetHashCode();
        critChance = ((stats.dexAttribute + level) / 10 * (UnityEngine.Random.Range(1, 6)));

        heal1 = 0;
        heal2 = 0;
        heal3 = 0;
        heal4 = 0;

        if (mana >= manaCost)
        {
            mana -= manaCost;

            if (partyMember1.getIsAlive() == true)
            {
                heal1 = UnityEngine.Random.Range(5, 15);
                partyMember1.healDamage(heal1);
            }
            if (partyMember2.getIsAlive() == true)
            {
                heal2 = UnityEngine.Random.Range(5, 15);
                partyMember2.healDamage(heal2);
            }
            if (partyMember3.getIsAlive() == true)
            {
                heal3 = UnityEngine.Random.Range(5, 15);
                partyMember3.healDamage(heal3);
            }
            if (partyMember4.getIsAlive() == true)
            {
                heal4 = UnityEngine.Random.Range(5, 15);
                partyMember4.healDamage(heal4);
            }
            Debug.Log(this + " healed" + heal1 + " to " + partyMember1 + ", " + heal2 + " to " + partyMember2 + ", "
                        + heal3 + " to " + partyMember3 + ", " + heal4 + " to " + partyMember4);
        }
    }

    public void Revive(BaseEntityClass jobRole)
    {
        manaCost = SkillCosts.Revive.GetHashCode();

        heal = 0;

        if (mana >= manaCost)
        {
            mana -= manaCost;
            if (false == jobRole.getIsAlive())
            {
                jobRole.health = 0;
                heal = (jobRole.maxHealth / 2);

                Debug.Log(this + " revived " + jobRole + " with Revive, healing " + heal);

                jobRole.gameObject.SetActive(true);
                jobRole.setAlive(true);
                jobRole.healDamage(heal);
            }
            else
            {
                Debug.Log(this + " tried to revive " + jobRole + " with Revive, but they're alive.");
            }
        }

    }

    public void Smite(BaseEntityClass enemy)
    {
        attackPriority = 0;
        manaCost = SkillCosts.Smite.GetHashCode();
        critChance = ((stats.dexAttribute + level) / 10 * (UnityEngine.Random.Range(1, 6)));

        damage = 0;

        if (mana >= manaCost)
        {
            mana -= manaCost;
            damage = UnityEngine.Random.Range(5, 9);

            Debug.Log(this + " attacked " + enemy + " with Smite, dealing " + damage);

            enemy.takeDamage(damage);
        }
    }

    #endregion

    public virtual void Initialise()
    {

    }

    // Use this for initialization
    void Start()
    {
        stats.atkAttribute = 0;
        stats.defAttribute = 0;
        stats.dexAttribute = 0;
        stats.intAttribute = 0;
        stats.vitAttribute = 0;
    }

    // Update is called once per frame 
    void Update()
    {
    }

    #region getFunctions

    public int getHealth()
    {
        return health;
    }

    public int getMana()
    {
        return mana;
    }

    public int getLevel()
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

    public Types getEntityType()
    {
        return entityType;
    }

    public bool getIsPlayer()
    {
        return isPlayer;
    }

    public bool getIsTaunted()
    {
        return isTaunted;
    }

    public int getTauntCounter()
    {
        return tauntCounter;
    }

    public GameObject getTauntingEnemy()
    {
        return tauntingEnemy;
    }

    public bool getIsSentinel()
    {
        return isSentinel;
    }

    public int getHealthPercentage()
    {
        return (health * 100) / maxHealth;
    }

    public int getAggroValue()
    {
        return aggroValue;
    }

    public string getJob()
    {
        return characterJob;
    }

    public int getDamageValue()
    {
        return damage;
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

    public void setEntityType(Types type)
    {
        entityType = type;
    }

    public void setIsPlayer(bool value)
    {
        isPlayer = value;
    }

    public void setIsTaunted(bool value)
    {
        isTaunted = value;
    }

    public void setTauntedCounter()
    {
        tauntCounter = UnityEngine.Random.Range(1, 4);
    }

    public void setTauntingEnemy(GameObject enemyTaunt)
    {
        tauntingEnemy = enemyTaunt;
    }

    public void setIsSentinel(bool value)
    {
        isSentinel = value;
    }
    public void setAggroValue(int value)
    {
        aggroValue = value;
    }

    #endregion

    #region levelUpStuff  
    public void levelUp(BaseEntityClass jobRole) //Job within the brackets
    {
        level++;
        experience = 0;

        int tempHealth = jobRole.stats.vitAttribute;
        int tempMana = jobRole.stats.intAttribute;

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

        int healthIncrease = (jobRole.stats.vitAttribute - tempHealth) * 4;
        int manaIncrease = (jobRole.stats.intAttribute - tempMana) * 2;

        jobRole.health += healthIncrease;
        jobRole.mana += manaIncrease;
    }

    public void hasCharacterMetLevelUpRequirements(BaseEntityClass jobRole)
    {
        if (jobRole.experience > jobRole.nextLevelUp)
        {
            int overflow = jobRole.experience - jobRole.nextLevelUp;
            nextLevelUp = nextLevelUp + nextLevelUp / 2;
            levelUp(jobRole);
            jobRole.experience += overflow;
            hasCharacterMetLevelUpRequirements(jobRole);
        }

    }

    #endregion

    public void Attack(BaseEntityClass enemy) //Enemy within brackets
    {
        int damage = 0;

        if (UnityEngine.Random.Range(1, 100) <= 90)
        {
            damage = (stats.atkAttribute + weaponAttack + UnityEngine.Random.Range(1, 6) - enemy.stats.defAttribute);

            if (damage <= 0)
            {
                damage = 1;
            }

            Debug.Log(this + "attacked " + enemy + " with attack, dealing " + damage);

            enemy.takeDamage(damage);
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
        if (health <= (maxHealth * 0.2) && UnityEngine.Random.Range(1, 100) <= runawayThreshold(level, stats.dexAttribute))
        {
            escapedBattle = true;
        }
    }

    //Amount of experience to be given to each individual player. Pass this int value to players
    public int experienceDrop(Types enemyType, int enemyLevel)
    {
        return (enemyType.GetHashCode() * enemyLevel);
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
        }

        if (health <= 0)
        {
            Debug.Log(this + " has died.");
            setAlive(false);
            this.gameObject.SetActive(false);
        }
    }

    public void healDamage(int healVal)
    {
        if (true == isAlive)
        {
            health += healVal;

            if (health > maxHealth)
            {
                health = maxHealth;
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
        setIsDefending(false);
    }

    public void assignLevel(GameObject p1, GameObject p2, GameObject p3, GameObject p4)
    {
        int newLevel = (p1.GetComponent<BaseEntityClass>().level + p2.GetComponent<BaseEntityClass>().level + p3.GetComponent<BaseEntityClass>().level + p4.GetComponent<BaseEntityClass>().level) / 4;

        if (this.getEntityType() == Types.Boss)
        {
            newLevel += 3;
        }
        else
        {
            newLevel += UnityEngine.Random.Range(-3, 4);
        }

        if (newLevel < 1)
        {
            newLevel = 1;
        }

        int levelDif = newLevel - level;

        for (int i = 0; i < levelDif; i++)
        {
            levelUp(this);
        }

    }
}
