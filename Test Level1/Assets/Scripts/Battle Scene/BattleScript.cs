using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleScript : MonoBehaviour
{

    #region Variables
    public GameObject player;
    public Text runText;

    public bool inBattle;
    private bool ranAway;
    public GameObject mageBattleCharacter, clericBattleCharacter, rogueBattleCharacter, tankBattleCharacter;
    private GameObject enemy1, enemy2, enemy3, leadEnemy;
    private GameObject firstEnemy, secondEnemy, thirdEnemy;

    public GameObject Empty, Skeleton, Bat, Hermit;
    private GameObject[] randomEnemySpawnArray;
    private GameObject[] playerArray;
    private GameObject[] enemyArray;
    public GameObject[] turnArray;
    private GameObject[] selectionArray;

    public Button AttackButton;

    public Camera mainCam;
    public Camera battleCam;

    //UI shtuff Doughnut touche
    public Button attackSelInMenu;
    public Button AttackStandard;
    public GameObject attackOne;
    public GameObject attackTwo;
    public GameObject attackThree;
    public GameObject attackFour;
    public GameObject Spells;
    public GameObject Items;
    public GameObject Run;
    public GameObject Attack;
    public GameObject BattleCam;

    BaseEntityClass target, target1, target2, target3, target4;

    public GameObject cursor, cursor2, cursor3, cursor4;

    public GameObject damageIndicator, damageIndicator2, damageIndicator3, damageIndicator4;

    private GameObject currentSelection;
    private GameObject multiSelect1, multiSelect2, multiSelect3, multiSelect4;

    public GameObject spellOne, spellTwo, spellThree, spellFour;

    private int lairCounter = 0;
    public int currentTurn;
    private int currSel = 0;
    private int groupNumber = 0;
    private bool isMulti = false;
    private bool isRevive = false;
    private int aggroValues = 0;

    private List<string> enemySpellType;
    private List<string> enemyAbilityType;

    public bool inputWait = false;
    public bool selectionWait = false;
    bool expAwarded = false;
    bool initialChoice = true;

    int expDrop;

    public string currentAttackString;



    public enum BattleState
    {
        START,
        PLAYERCHOICE,
        ENEMYCHOICE,
        SELECTIONSINGLE,
        SELECTIONMULTI,
        SELECTIONSELF,
        EXECUTIONOFATTACK,
        WIN,
        LOSE,
        RANAWAY,
        PROCESSING
    }

    private BattleState currentState;

    #endregion

    // Use this for initialization
    void Start()
    {
        inBattle = false;
        playerArray = new GameObject[] { mageBattleCharacter, clericBattleCharacter, rogueBattleCharacter, tankBattleCharacter };
        randomEnemySpawnArray = new GameObject[] { Empty, Empty, Empty, Skeleton, Bat, Hermit, Skeleton, Bat, Hermit };
    }

    #region Battle_Start
    // This function will be called at the start of the battle 
    public void StartBattle(GameObject collidedEnemy)
    {
        runText.text = "";
        battleCam.gameObject.SetActive(true);
        mainCam.gameObject.SetActive(false);
        player.gameObject.SetActive(false);

        AttackButton.Select();
        AttackButton.OnSelect(null);

        ranAway = false;
        expAwarded = false;

        List<string> eSpellType = new List<string>();
        List<string> eAbilityType = new List<string>();
        enemySpellType = eSpellType;
        enemyAbilityType = eAbilityType;

        int pID1, pID2, pID3, pID4;
        int eID1, eID2, eID3, eID4;

        pID1 = mageBattleCharacter.GetComponent<MageClass>().ID;
        pID2 = clericBattleCharacter.GetComponent<ClericClass>().ID;
        pID3 = rogueBattleCharacter.GetComponent<RogueClass>().ID;
        pID4 = tankBattleCharacter.GetComponent<TankClass>().ID;

        mageBattleCharacter.transform.position = new Vector3(-40 + (pID1 * 0.75f), mageBattleCharacter.transform.position.y, mageBattleCharacter.transform.position.z);
        clericBattleCharacter.transform.position = new Vector3(-40 + (pID2 * 0.75f), clericBattleCharacter.transform.position.y, clericBattleCharacter.transform.position.z);
        rogueBattleCharacter.transform.position = new Vector3(-40 + (pID3 * 0.75f), rogueBattleCharacter.transform.position.y, rogueBattleCharacter.transform.position.z);
        tankBattleCharacter.transform.position = new Vector3(-40 + (pID4 * 0.75f), tankBattleCharacter.transform.position.y, tankBattleCharacter.transform.position.z);

        leadEnemy = collidedEnemy;

        leadEnemy.GetComponent<BaseEntityClass>().ID = 4;
        eID4 = leadEnemy.GetComponent<BaseEntityClass>().ID;
        leadEnemy.GetComponent<BaseEntityClass>().Initialise();

        if (leadEnemy.GetComponent<BaseEntityClass>().getEntityType() == BaseEntityClass.Types.Boss)
        {
            lairCounter = 0;
            enemy3 = randomEnemySpawnArray[0];
            enemy2 = randomEnemySpawnArray[0];
            enemy1 = randomEnemySpawnArray[0];
        }
        else
        {
            enemy3 = randomEnemySpawnArray[UnityEngine.Random.Range(0, randomEnemySpawnArray.Length - 1)];
            enemy2 = randomEnemySpawnArray[UnityEngine.Random.Range(0, randomEnemySpawnArray.Length - 1)];
            enemy1 = randomEnemySpawnArray[UnityEngine.Random.Range(0, randomEnemySpawnArray.Length - 1)];
        }

        if (leadEnemy.GetComponent<BaseEntityClass>().getEntityType() == BaseEntityClass.Types.Boss)
        {
            leadEnemy.transform.position = new Vector3(-30 - (eID4 * 0.75f), 3.4f, collidedEnemy.transform.position.z);
        }
        else
        {
            leadEnemy.transform.position = new Vector3(-30 - (eID4 * 0.75f), 2.5f, collidedEnemy.transform.position.z);
        }

        thirdEnemy = Instantiate(enemy3, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        thirdEnemy.GetComponent<BaseEntityClass>().ID = 3;
        eID3 = thirdEnemy.GetComponent<BaseEntityClass>().ID;
        thirdEnemy.GetComponent<BaseEntityClass>().Initialise();
        thirdEnemy.transform.position = new Vector3(-30 - (eID3 * 0.75f), 2.5f, enemy3.transform.position.z);

        secondEnemy = Instantiate(enemy2, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        secondEnemy.GetComponent<BaseEntityClass>().ID = 2;
        eID2 = secondEnemy.GetComponent<BaseEntityClass>().ID;
        secondEnemy.GetComponent<BaseEntityClass>().Initialise();
        secondEnemy.transform.position = new Vector3(-30 - (eID2 * 0.75f), 2.5f, enemy2.transform.position.z);

        firstEnemy = Instantiate(enemy1, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        firstEnemy.GetComponent<BaseEntityClass>().ID = 1;
        eID1 = firstEnemy.GetComponent<BaseEntityClass>().ID;
        firstEnemy.GetComponent<BaseEntityClass>().Initialise();
        firstEnemy.transform.position = new Vector3(-30 - (eID1 * 0.75f), 2.5f, enemy1.transform.position.z);

        enemyArray = new GameObject[] { leadEnemy, thirdEnemy, secondEnemy, firstEnemy };
        selectionArray = new GameObject[] { mageBattleCharacter, clericBattleCharacter, rogueBattleCharacter, tankBattleCharacter, leadEnemy, thirdEnemy, secondEnemy, firstEnemy };

        sortTurnOrder();

        inBattle = true;

        leadEnemy.GetComponent<BaseEntityClass>().assignLevel(mageBattleCharacter, clericBattleCharacter, rogueBattleCharacter, tankBattleCharacter);
        thirdEnemy.GetComponent<BaseEntityClass>().assignLevel(mageBattleCharacter, clericBattleCharacter, rogueBattleCharacter, tankBattleCharacter);
        secondEnemy.GetComponent<BaseEntityClass>().assignLevel(mageBattleCharacter, clericBattleCharacter, rogueBattleCharacter, tankBattleCharacter);
        firstEnemy.GetComponent<BaseEntityClass>().assignLevel(mageBattleCharacter, clericBattleCharacter, rogueBattleCharacter, tankBattleCharacter);

        expDrop = totalEnemyExperienceDrop(leadEnemy.GetComponent<BaseEntityClass>(), firstEnemy.GetComponent<BaseEntityClass>(), secondEnemy.GetComponent<BaseEntityClass>(), thirdEnemy.GetComponent<BaseEntityClass>());

        currentState = BattleState.START;

    }

    void sortTurnOrder()
    {
        turnArray = new GameObject[] { mageBattleCharacter, clericBattleCharacter, rogueBattleCharacter, tankBattleCharacter, leadEnemy, thirdEnemy, secondEnemy, firstEnemy };

        GameObject currentTurnSort;

        for (int sorting = 0; sorting < turnArray.Length; sorting++)
        {
            for (int currentEntity = 0; currentEntity < turnArray.Length - (sorting + 1); currentEntity++)
            {
                if (turnArray[currentEntity].GetComponent<BaseEntityClass>().stats.dexAttribute < turnArray[currentEntity + 1].GetComponent<BaseEntityClass>().stats.dexAttribute)
                {
                    currentTurnSort = turnArray[currentEntity + 1];
                    turnArray[currentEntity + 1] = turnArray[currentEntity];
                    turnArray[currentEntity] = currentTurnSort;
                }
            }
        }
    }
    #endregion

    #region Battle_Loop
    // Update is called once per frame
    void Update()
    {
        // Awaits input for player within menu
        // Moves cursor with analogue stick
        // Click stick1 to select
        // Click stick2 to back
        if (BattleCam.activeSelf)
        {
            if (Input.GetButton("Return"))
            {
                if (true != Attack.activeSelf)
                {
                    Attack.SetActive(true);
                    Spells.SetActive(true);
                    Items.SetActive(true);
                    Run.SetActive(true);

                    attackOne.SetActive(false);
                    attackTwo.SetActive(false);
                    attackThree.SetActive(false);
                    attackFour.SetActive(false);

                    spellOne.SetActive(false);
                    spellTwo.SetActive(false);
                    spellThree.SetActive(false);
                    spellFour.SetActive(false);

                    AttackStandard.Select();
                    AttackStandard.OnSelect(null);
                }
            }

            //switch battlestate 
            BattleSequence();
        }



        // Highlight the entity and its turn
    }

    void BattleSequence()
    {

        //Debug.Log(currentState);
        switch (currentState)
        {
            #region START
            case (BattleState.START):
                currentTurn = 0;
                currentState = BattleState.PROCESSING;
                break;
            #endregion

            #region PLAYERCHOICE
            case (BattleState.PLAYERCHOICE):

                if (true != attackOne.activeSelf && true != spellOne.activeSelf)
                {
                    Spells.SetActive(true);
                    Items.SetActive(true);
                    Run.SetActive(true);
                    Attack.SetActive(true);
                }

                cursor.SetActive(false);
                cursor2.SetActive(false);
                cursor3.SetActive(false);
                cursor4.SetActive(false);


                Run.GetComponent<Button>().onClick.AddListener(delegate { RunKidRun(); });

                break;
            #endregion

            #region ENEMYCHOICE
            case (BattleState.ENEMYCHOICE):
                int randomAttack = 0;
                //Runaway attempt and success check
                if (turnArray[currentTurn].GetComponent<BaseEntityClass>().getEntityType() != BaseEntityClass.Types.Boss)
                {
                    turnArray[currentTurn].GetComponent<BaseEntityClass>().EnemyRunAway();
                }


                if (true == turnArray[currentTurn].GetComponent<BaseEntityClass>().getEscapedBattle())
                {
                    expDrop -= turnArray[currentTurn].GetComponent<BaseEntityClass>().experience;
                    turnArray[currentTurn].GetComponent<BaseEntityClass>().setAlive(false);
                    turnArray[currentTurn].SetActive(false);
                    currentTurn++;
                    currentState = BattleState.PROCESSING;
                }

                //Enemy chooses ability
                if (turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyTempSpells != 0 && turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyTempAbilities != 0)
                {
                    randomAttack = UnityEngine.Random.Range(0, 3);
                }
                else if (turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyTempSpells != 0 && turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyTempAbilities == 0)
                {
                    randomAttack = UnityEngine.Random.Range(0, 2);
                }
                else if (turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyTempSpells == 0 && turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyTempAbilities != 0)
                {
                    randomAttack = UnityEngine.Random.Range(0, 2);
                    if (randomAttack == 1)
                    {
                        randomAttack = 2;
                    }
                }

                if (randomAttack == 0)
                {
                    //Basic attack
                    enemyTargeting();

                    foreach (GameObject player in playerArray)
                    {
                        if (player.GetComponent<BaseEntityClass>().getIsSentinel())
                        {
                            target = player.GetComponent<BaseEntityClass>();
                        }
                    }

                    turnArray[currentTurn].GetComponent<BaseEntityClass>().Attack(target);
                    
                    StartCoroutine(damageDisplay("Attack"));

                }
                else if (randomAttack == 1)
                {
                    //Spell
                    int randomSpell = UnityEngine.Random.Range(0, turnArray[currentTurn].GetComponent<BaseEntityClass>().enemySpellList.Count);
                    string currentEnemyAttackString = turnArray[currentTurn].GetComponent<BaseEntityClass>().enemySpellList[randomSpell];

                    if (enemySpellType[randomSpell] == "Single")
                    {
                        enemyTargeting();
                        System.Type enemyThisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                        System.Reflection.MethodInfo enemyTheMethod = enemyThisType.GetMethod(currentEnemyAttackString);
                        enemyTheMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { target.GetComponent<BaseEntityClass>() });
                    }
                    else if (enemySpellType[randomSpell] == "Multi")
                    {
                        target1 = playerArray[0].GetComponent<BaseEntityClass>();
                        target2 = playerArray[1].GetComponent<BaseEntityClass>();
                        target3 = playerArray[2].GetComponent<BaseEntityClass>();
                        target4 = playerArray[3].GetComponent<BaseEntityClass>();
                        System.Type enemyThisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                        System.Reflection.MethodInfo enemyTheMethod = enemyThisType.GetMethod(currentEnemyAttackString);
                        enemyTheMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { target1.GetComponent<BaseEntityClass>(), target2.GetComponent<BaseEntityClass>(),
                                                                                                                target3.GetComponent<BaseEntityClass>(), target4.GetComponent<BaseEntityClass>() });
                    }
                    else if (enemySpellType[randomSpell] == "SingleAlly" && turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsTaunted() == false)
                    {
                        target = enemyArray[UnityEngine.Random.Range(0, 4)].GetComponent<BaseEntityClass>();
                        randomTarget(enemyArray);
                        System.Type enemyThisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                        System.Reflection.MethodInfo enemyTheMethod = enemyThisType.GetMethod(currentEnemyAttackString);
                        enemyTheMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { target.GetComponent<BaseEntityClass>() });
                    }
                    else if (enemySpellType[randomSpell] == "MultiAlly" && turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsTaunted() == false)
                    {
                        target1 = enemyArray[0].GetComponent<BaseEntityClass>();
                        target2 = enemyArray[1].GetComponent<BaseEntityClass>();
                        target3 = enemyArray[2].GetComponent<BaseEntityClass>();
                        target4 = enemyArray[3].GetComponent<BaseEntityClass>();
                        System.Type enemyThisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                        System.Reflection.MethodInfo enemyTheMethod = enemyThisType.GetMethod(currentEnemyAttackString);
                        enemyTheMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { target1.GetComponent<BaseEntityClass>(), target2.GetComponent<BaseEntityClass>(),
                                                                                                                target3.GetComponent<BaseEntityClass>(), target4.GetComponent<BaseEntityClass>() });
                    }
                    else if (enemySpellType[randomSpell] == "Self" && turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsTaunted() == false)
                    {
                        System.Type enemyThisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                        System.Reflection.MethodInfo enemyTheMethod = enemyThisType.GetMethod(currentEnemyAttackString);
                        enemyTheMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { turnArray[currentTurn].GetComponent<BaseEntityClass>() });
                    }
                    else
                    {
                        //Basic attack
                        enemyTargeting();

                        foreach (GameObject player in playerArray)
                        {
                            if (player.GetComponent<BaseEntityClass>().getIsSentinel())
                            {
                                target = player.GetComponent<BaseEntityClass>();
                            }
                        }

                        turnArray[currentTurn].GetComponent<BaseEntityClass>().Attack(target);
                    }
                }
                else
                {
                    //Ability
                    int randomAbility = UnityEngine.Random.Range(0, turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList.Count);
                    string currentEnemyAttackString = turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList[randomAbility];

                    if (enemyAbilityType[randomAbility] == "Single")
                    {
                        enemyTargeting();
                        System.Type enemyThisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                        System.Reflection.MethodInfo enemyTheMethod = enemyThisType.GetMethod(currentEnemyAttackString);
                        enemyTheMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { target.GetComponent<BaseEntityClass>() });
                    }
                    else if (enemyAbilityType[randomAbility] == "Multi")
                    {
                        target1 = playerArray[0].GetComponent<BaseEntityClass>();
                        target2 = playerArray[1].GetComponent<BaseEntityClass>();
                        target3 = playerArray[2].GetComponent<BaseEntityClass>();
                        target4 = playerArray[3].GetComponent<BaseEntityClass>();
                        System.Type enemyThisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                        System.Reflection.MethodInfo enemyTheMethod = enemyThisType.GetMethod(currentEnemyAttackString);
                        enemyTheMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { target1.GetComponent<BaseEntityClass>(), target2.GetComponent<BaseEntityClass>(),
                                                                                                                target3.GetComponent<BaseEntityClass>(), target4.GetComponent<BaseEntityClass>() });
                    }
                    else if (enemyAbilityType[randomAbility] == "SingleAlly" && turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsTaunted() == false)
                    {
                        target = enemyArray[UnityEngine.Random.Range(0, 4)].GetComponent<BaseEntityClass>();
                        randomTarget(enemyArray);
                        System.Type enemyThisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                        System.Reflection.MethodInfo enemyTheMethod = enemyThisType.GetMethod(currentEnemyAttackString);
                        enemyTheMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { target.GetComponent<BaseEntityClass>() });
                    }
                    else if (enemyAbilityType[randomAbility] == "MultiAlly" && turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsTaunted() == false)
                    {
                        target1 = enemyArray[0].GetComponent<BaseEntityClass>();
                        target2 = enemyArray[1].GetComponent<BaseEntityClass>();
                        target3 = enemyArray[2].GetComponent<BaseEntityClass>();
                        target4 = enemyArray[3].GetComponent<BaseEntityClass>();
                        System.Type enemyThisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                        System.Reflection.MethodInfo enemyTheMethod = enemyThisType.GetMethod(currentEnemyAttackString);
                        enemyTheMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { target1.GetComponent<BaseEntityClass>(), target2.GetComponent<BaseEntityClass>(),
                                                                                                                target3.GetComponent<BaseEntityClass>(), target4.GetComponent<BaseEntityClass>() });
                    }
                    else if (enemyAbilityType[randomAbility] == "Self" && turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsTaunted() == false)
                    {
                        System.Type enemyThisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                        System.Reflection.MethodInfo enemyTheMethod = enemyThisType.GetMethod(currentEnemyAttackString);
                        enemyTheMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { turnArray[currentTurn].GetComponent<BaseEntityClass>() });
                    }
                    else
                    {
                        //Basic attack
                        enemyTargeting();

                        foreach (GameObject player in playerArray)
                        {
                            if (player.GetComponent<BaseEntityClass>().getIsSentinel())
                            {
                                target = player.GetComponent<BaseEntityClass>();
                            }
                        }

                        turnArray[currentTurn].GetComponent<BaseEntityClass>().Attack(target);
                    }
                }


                //End turn
                currentTurn++;
                currentState = BattleState.PROCESSING;

                break;
            #endregion

            #region SELECTION-Single
            case (BattleState.SELECTIONSINGLE):

                //if(initialChoice)
                //{
                //    if(isRevive)
                //    {
                //        int tempSel = 0;
                //        for(int player = 0; player > selectionArray.Length - 1; player++)
                //        {
                //            if(selectionArray[player].GetComponent<BaseEntityClass>().getIsAlive() == false && player.GetComponent<BaseEntityClass>().getIsPlayer() == true)
                //            {
                //                currentSelection = selectionArray[player];
                //                currSel = tempSel;
                //            }
                //        }
                //    }
                //    //currentSelection = selectionArray[];


                //    initialChoice = false;
                //}

                checkCurrentSelectionRight();

                if (Input.GetButton("Back")) /* && false == inputWait*/
                {
                    currentState = BattleState.PLAYERCHOICE;
                    inputWait = false;
                }

                currentSelection = selectionArray[currSel];

                if (Input.GetAxis("Horizontal") > 0.2) /* && false == inputWait*/
                {
                    inputWait = false;

                    currSel++;
                    checkCurrentSelectionRight();

                    inputWait = true;

                    System.Threading.Thread.Sleep(300);
                }

                if (Input.GetAxis("Horizontal") < -0.2) /* && false == inputWait*/
                {
                    inputWait = false;

                    currSel--;
                    checkCurrentSelectionLeft();

                    inputWait = true;

                    System.Threading.Thread.Sleep(300);
                }

                cursor.transform.position = new Vector3(currentSelection.transform.position.x, currentSelection.transform.position.y + 1f, 0f);

                if (Input.GetButtonUp("Submit") && true == inputWait)
                {
                    //Find attack/ability/spell with current string
                    isMulti = false;
                    inputWait = false;
                    currentState = BattleState.EXECUTIONOFATTACK;
                }

                else if (Input.GetButtonUp("Submit") && false == inputWait)
                {
                    inputWait = true;
                }

                //StartCoroutine(waitForInput());

                break;
            #endregion

            #region SELECTION-Multi
            case (BattleState.SELECTIONMULTI):

                if (Input.GetButton("Back")) /* && false == inputWait*/
                {
                    cursor.SetActive(false);
                    cursor2.SetActive(false);
                    cursor3.SetActive(false);
                    cursor4.SetActive(false);

                    inputWait = false;

                    currentState = BattleState.PLAYERCHOICE;
                }

                multiSelect1 = selectionArray[0 + (groupNumber * 4)];
                multiSelect2 = selectionArray[1 + (groupNumber * 4)];
                multiSelect3 = selectionArray[2 + (groupNumber * 4)];
                multiSelect4 = selectionArray[3 + (groupNumber * 4)];

                if (Input.GetAxis("Horizontal") > 0.2) /* && false == inputWait*/
                {
                    inputWait = false;

                    if (groupNumber == 0)
                    {
                        groupNumber = 1;
                    }
                    else
                    {
                        groupNumber = 0;
                    }

                    inputWait = true;

                    System.Threading.Thread.Sleep(500);
                }

                if (Input.GetAxis("Horizontal") < -0.2) /* && false == inputWait*/
                {
                    inputWait = false;

                    if (groupNumber == 0)
                    {
                        groupNumber = 1;
                    }
                    else
                    {
                        groupNumber = 0;
                    }

                    inputWait = true;

                    System.Threading.Thread.Sleep(500);
                }

                if (true == multiSelect1.GetComponent<BaseEntityClass>().getIsAlive())
                {
                    cursor.SetActive(true);
                    cursor.transform.position = new Vector3(multiSelect1.transform.position.x, multiSelect1.transform.position.y + 1f, 0f);
                }
                else
                {
                    cursor.SetActive(false);
                }

                if (true == multiSelect2.GetComponent<BaseEntityClass>().getIsAlive())
                {
                    cursor2.SetActive(true);
                    cursor2.transform.position = new Vector3(multiSelect2.transform.position.x, multiSelect2.transform.position.y + 1f, 0f);
                }
                else
                {
                    cursor2.SetActive(false);
                }

                if (true == multiSelect3.GetComponent<BaseEntityClass>().getIsAlive())
                {
                    cursor3.SetActive(true);
                    cursor3.transform.position = new Vector3(multiSelect3.transform.position.x, multiSelect3.transform.position.y + 1f, 0f);
                }
                else
                {
                    cursor3.SetActive(false);
                }

                if (true == multiSelect4.GetComponent<BaseEntityClass>().getIsAlive())
                {
                    cursor4.SetActive(true);
                    cursor4.transform.position = new Vector3(multiSelect4.transform.position.x, multiSelect4.transform.position.y + 1f, 0f);
                }
                else
                {
                    cursor4.SetActive(false);
                }

                if (Input.GetButton("Submit") && true == inputWait)
                {
                    isMulti = true;
                    //currentTurn++;
                    inputWait = false;
                    currentState = BattleState.EXECUTIONOFATTACK;

                }

                else if (Input.GetButtonUp("Submit") && false == inputWait)
                {
                    inputWait = true;
                }

                break;
            #endregion

            #region SELECTION-Self
            case (BattleState.SELECTIONSELF):

                if (Input.GetButton("Back")) /* && false == inputWait*/
                {
                    currentState = BattleState.PLAYERCHOICE;
                    inputWait = false;
                }

                currentSelection = turnArray[currentTurn];

                cursor.transform.position = new Vector3(currentSelection.transform.position.x, currentSelection.transform.position.y + 1f, 0f);

                if (Input.GetButtonUp("Submit") && true == inputWait)
                {
                    isMulti = false;
                    inputWait = false;
                    currentState = BattleState.EXECUTIONOFATTACK;

                }
                else if (Input.GetButtonUp("Submit") && false == inputWait)
                {
                    inputWait = true;
                }

                break;
            #endregion

            #region EXECUTIONOFATTACK

            case (BattleState.EXECUTIONOFATTACK):

                System.Type thisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                System.Reflection.MethodInfo theMethod = thisType.GetMethod(currentAttackString);

                if (false == isMulti)
                {
                    theMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { currentSelection.GetComponent<BaseEntityClass>() });
                }
                else
                {
                    theMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { multiSelect1.GetComponent<BaseEntityClass>(), multiSelect2.GetComponent<BaseEntityClass>(), multiSelect3.GetComponent<BaseEntityClass>(), multiSelect4.GetComponent<BaseEntityClass>() });
                }
                currentTurn++;
                currentState = BattleState.PROCESSING;

                break;

            #endregion

            #region WIN
            case (BattleState.WIN):

                Spells.SetActive(false);
                Items.SetActive(false);
                Run.SetActive(false);
                Attack.SetActive(false);

                if (expAwarded == false)
                {

                    for (int i = 0; i < playerArray.Length; i++)
                    {
                        if (playerArray[i].GetComponent<BaseEntityClass>().getIsAlive() == true)
                        {
                            playerArray[i].GetComponent<BaseEntityClass>().experience += expDrop;
                            playerArray[i].GetComponent<BaseEntityClass>().hasCharacterMetLevelUpRequirements(playerArray[i].GetComponent<BaseEntityClass>());
                        }

                    }

                    expAwarded = true;
                }

                runText.text = "You win! Press Submit to continue...";

                if (Input.GetButton("Submit"))
                {
                    StartCoroutine(charlieSheenWinning());
                }

                break;
            #endregion

            #region LOSE
            case (BattleState.LOSE):
                runText.text = "Oh dear, you are dead!";

                if (Input.GetButton("Submit"))
                {
                    StartCoroutine(charlieSheenLosing());
                }

                break;
            #endregion

            #region RANAWAY
            case (BattleState.RANAWAY):

                Spells.SetActive(false);
                Items.SetActive(false);
                Run.SetActive(false);
                Attack.SetActive(false);

                if (Input.GetButton("Submit"))
                {
                    StartCoroutine(runningAwayThing());
                }

                break;
            #endregion

            #region PROCESSING
            case (BattleState.PROCESSING):

                Spells.SetActive(false);
                Items.SetActive(false);
                Run.SetActive(false);
                Attack.SetActive(false);

                cursor.SetActive(false);
                cursor2.SetActive(false);
                cursor3.SetActive(false);
                cursor4.SetActive(false);

                AttackStandard.Select();
                AttackStandard.OnSelect(null);

                //runText.text = "";
                
                System.Threading.Thread.Sleep(1000);

                if (currentTurn == turnArray.Length)
                {
                    currentTurn = 0;
                }

                checkCurrentTurn();

                if (leadEnemy.GetComponent<BaseEntityClass>().getEntityType() == BaseEntityClass.Types.Boss)
                {
                    lairAction();
                }

                if (true == turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsTaunted())
                {
                    if (turnArray[currentTurn].GetComponent<BaseEntityClass>().tauntCounter <= 0)
                    {
                        turnArray[currentTurn].GetComponent<BaseEntityClass>().setIsTaunted(false);
                    }
                    else
                    {
                        turnArray[currentTurn].GetComponent<BaseEntityClass>().tauntCounter--;
                    }
                }

                if (true == turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsSentinel())
                {
                    turnArray[currentTurn].GetComponent<BaseEntityClass>().setIsSentinel(false);
                }

                if (true == turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsPlayer())
                {
                    if (true == turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsAlive() && false == turnArray[currentTurn].GetComponent<BaseEntityClass>().getEscapedBattle())
                    {
                        int abilitiesHash = turnArray[currentTurn].GetComponent<BaseEntityClass>().abilityList.GetHashCode();
                        string abilitiesString = turnArray[currentTurn].GetComponent<BaseEntityClass>().abilityList.ToString();
                        abilitySearch(abilitiesString, abilitiesHash);

                        //This can be put on a separate attack button rather than on the abilities page (when we have 5 buttons
                        attackOne.GetComponent<Button>().onClick.AddListener(delegate { changeStateToSelection("Attack", false); });

                        int spellsHash = turnArray[currentTurn].GetComponent<BaseEntityClass>().spellList.GetHashCode();
                        string spellsString = turnArray[currentTurn].GetComponent<BaseEntityClass>().spellList.ToString();
                        spellSearch(spellsString, spellsHash);

                        currentState = BattleState.PLAYERCHOICE;
                    }
                    else
                    {
                        currentTurn++;
                    }
                }

                if (false == turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsPlayer())
                {
                    if (true == turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsAlive() && false == turnArray[currentTurn].GetComponent<BaseEntityClass>().getEscapedBattle())
                    {
                        int enemyAbilitiesHash = turnArray[currentTurn].GetComponent<BaseEntityClass>().abilityList.GetHashCode();
                        string enemyAbilitiesString = turnArray[currentTurn].GetComponent<BaseEntityClass>().abilityList.ToString();

                        int enemySpellsHash = turnArray[currentTurn].GetComponent<BaseEntityClass>().spellList.GetHashCode();
                        string enemySpellsString = turnArray[currentTurn].GetComponent<BaseEntityClass>().spellList.ToString();

                        enemyAbilitiesSpells(enemyAbilitiesString, enemyAbilitiesHash, enemySpellsString, enemySpellsHash);

                        currentState = BattleState.ENEMYCHOICE;
                    }
                    else if (false == turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsAlive())
                    {
                        turnArray[currentTurn].SetActive(false);
                        //Also need to prepare awarding experience to players
                        currentTurn++;
                    }
                    else
                    {
                        turnArray[currentTurn].SetActive(false);
                        currentTurn++;
                    }
                }

                if (true != leadEnemy.GetComponent<BaseEntityClass>().getIsAlive() && true != thirdEnemy.GetComponent<BaseEntityClass>().getIsAlive() && true != secondEnemy.GetComponent<BaseEntityClass>().getIsAlive() && true != firstEnemy.GetComponent<BaseEntityClass>().getIsAlive())
                {
                    currentState = BattleState.WIN;
                }

                if (true != tankBattleCharacter.GetComponent<BaseEntityClass>().isAlive && true != rogueBattleCharacter.GetComponent<BaseEntityClass>().isAlive && true != clericBattleCharacter.GetComponent<BaseEntityClass>().isAlive && true != mageBattleCharacter.GetComponent<BaseEntityClass>().isAlive)
                {
                    currentState = BattleState.LOSE;
                }

                break;
                #endregion
        }
    }

    public void checkCurrentTurn()
    {
        if (currentTurn == turnArray.Length)
        {
            currentTurn = 0;
        }

        if (turnArray[currentTurn].GetComponent<BaseEntityClass>().getEntityType() == BaseEntityClass.Types.NonExistent)
        {
            currentTurn++;
            checkCurrentTurn();
        }
        currentState = BattleState.PROCESSING;
    }

    public void randomTarget(GameObject[] entityArray)
    {
        if (false == target.getIsAlive())
        {
            target = entityArray[UnityEngine.Random.Range(0, entityArray.Length)].GetComponent<BaseEntityClass>();
            randomTarget(entityArray);
        }
    }

    public void enemyTargeting()
    {
        aggroValues = 0;

        foreach (GameObject player in playerArray)
        {
            if (turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsTaunted() == true)
            {
                if (player == turnArray[currentTurn].GetComponent<BaseEntityClass>().getTauntingEnemy())
                {
                    player.GetComponent<BaseEntityClass>().setAggroValue(player.GetComponent<BaseEntityClass>().getAggroValue() * 5);
                }
            }

            if (player.GetComponent<BaseEntityClass>().getHealthPercentage() <= 25)
            {
                player.GetComponent<BaseEntityClass>().setAggroValue(player.GetComponent<BaseEntityClass>().getAggroValue() * 2);
            }

            aggroValues += player.GetComponent<BaseEntityClass>().getAggroValue();
        }

        int randomTargeting = UnityEngine.Random.Range(0, aggroValues);

        if (randomTargeting < playerArray[0].GetComponent<BaseEntityClass>().getAggroValue())
        {
            target = playerArray[0].GetComponent<BaseEntityClass>();
        }
        else if (randomTargeting < playerArray[0].GetComponent<BaseEntityClass>().getAggroValue() +
                                    playerArray[1].GetComponent<BaseEntityClass>().getAggroValue())
        {
            target = playerArray[1].GetComponent<BaseEntityClass>();
        }
        else if (randomTargeting < playerArray[0].GetComponent<BaseEntityClass>().getAggroValue() +
                                    playerArray[1].GetComponent<BaseEntityClass>().getAggroValue() +
                                    playerArray[2].GetComponent<BaseEntityClass>().getAggroValue())
        {
            target = playerArray[2].GetComponent<BaseEntityClass>();
        }
        else
        {
            target = playerArray[3].GetComponent<BaseEntityClass>();
        }

        foreach (GameObject player in playerArray)
        {
            player.GetComponent<BaseEntityClass>().setAggroValue(10);
        }

        if (target.getIsAlive() == false)
        {
            enemyTargeting();
        }
    }


    #endregion

    #region Enemy_abilities_spells
    public void enemyAbilitiesSpells(string abilities, int abilityHash, string spells, int spellHash)
    {
        //ABILITIES
        //None = 0,
        //Taunt = 1,
        //Sentinel = 2,
        //Shield_Slam = 4,
        //Shiv = 8,
        //Quick_Attack = 16,
        //Shadow_Strike = 32,
        //Meditate = 64,
        //Poison_Sting = 128

        //SPELLS
        //None = 0,
        //Firebolt = 1,
        //Flaming_Orb = 2,
        //Heal = 4,
        //Group_Heal = 8,
        //Revive = 16,
        //Smite = 32

        turnArray[currentTurn].GetComponent<BaseEntityClass>().enemySpellList.Clear();
        turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList.Clear();

        enemySpellType.Clear();
        enemyAbilityType.Clear();

        //When checking if they have spells/abilities, if they don't have enough mana, lower the hash so that it can't try and use a random skill.

        if (abilityHash != 0)
        {
            string tempAbility = "";
            string targetType = "";
            if (abilities.Contains("Taunt") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Taunt.GetHashCode())
            {
                abilities = abilities.Remove(abilities.IndexOf("Taunt"), "Taunt".Length);
                tempAbility = "Taunt";
                targetType = "Single";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList.Add(tempAbility);
                enemyAbilityType.Add(targetType);
            }
            if (abilities.Contains("Sentinel") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Sentinel.GetHashCode())
            {
                abilities = abilities.Remove(abilities.IndexOf("Sentinel"), "Sentinel".Length);
                tempAbility = "Sentinel";
                targetType = "Self";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList.Add(tempAbility);
                enemyAbilityType.Add(targetType);
            }
            if (abilities.Contains("Shield_Slam") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Shield_Slam.GetHashCode())
            {
                abilities = abilities.Remove(abilities.IndexOf("Shield_Slam"), "Shield_Slam".Length);
                tempAbility = "Shield_Slam";
                targetType = "Single";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList.Add(tempAbility);
                enemyAbilityType.Add(targetType);
            }
            if (abilities.Contains("Shiv") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Shiv.GetHashCode())
            {
                abilities = abilities.Remove(abilities.IndexOf("Shiv"), "Shiv".Length);
                tempAbility = "Shiv";
                targetType = "Single";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList.Add(tempAbility);
                enemyAbilityType.Add(targetType);
            }
            if (abilities.Contains("Quick_Attack") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Quick_Attack.GetHashCode())
            {
                abilities = abilities.Remove(abilities.IndexOf("Quick_Attack"), "Quick_Attack".Length);
                tempAbility = "Quick_Attack";
                targetType = "Single";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList.Add(tempAbility);
                enemyAbilityType.Add(targetType);
            }
            if (abilities.Contains("Shadow_Strike") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Shadow_Strike.GetHashCode())
            {
                abilities = abilities.Remove(abilities.IndexOf("Shadow_Strike"), "Shadow_Strike".Length);
                tempAbility = "Shadow_Strike";
                targetType = "Single";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList.Add(tempAbility);
                enemyAbilityType.Add(targetType);
            }
            if (abilities.Contains("Meditate") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Meditate.GetHashCode())
            {
                abilities = abilities.Remove(abilities.IndexOf("Meditate"), "Meditate".Length);
                tempAbility = "Meditate";
                targetType = "Self";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList.Add(tempAbility);
                enemyAbilityType.Add(targetType);
            }
            if (abilities.Contains("Poison_Sting") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Meditate.GetHashCode())
            {
                abilities = abilities.Remove(abilities.IndexOf("Poison_Sting"), "Poison_Sting".Length);
                tempAbility = "Poison_Sting";
                targetType = "Single";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList.Add(tempAbility);
                enemyAbilityType.Add(targetType);
            }

            tempAbility = "";
            if (turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyAbilityList.Count == 0)
            {
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyTempAbilities = 0;
            }
            else
            {
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyTempAbilities = turnArray[currentTurn].GetComponent<BaseEntityClass>().abilityList.GetHashCode();
            }
        }

        if (spellHash != 0)
        {
            string tempSpell = "";
            string targetType = "";
            if (spells.Contains("Firebolt") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Firebolt.GetHashCode())
            {
                spells = spells.Remove(spells.IndexOf("Firebolt"), "Firebolt".Length);
                tempSpell = "Firebolt";
                targetType = "Single";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemySpellList.Add(tempSpell);
                enemySpellType.Add(targetType);
            }
            if (spells.Contains("Flaming_Orb") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Flaming_Orb.GetHashCode())
            {
                spells = spells.Remove(spells.IndexOf("Flaming_Orb"), "Flaming_Orb".Length);
                tempSpell = "Flaming_Orb";
                targetType = "Multi";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemySpellList.Add(tempSpell);
                enemySpellType.Add(targetType);
            }
            if (spells.Contains("Group_Heal") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Group_Heal.GetHashCode())
            {
                spells = spells.Remove(spells.IndexOf("Group_Heal"), "Group_Heal".Length);
                tempSpell = "Group_Heal";
                targetType = "MultiAlly";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemySpellList.Add(tempSpell);
                enemySpellType.Add(targetType);
            }
            if (spells.Contains("Heal") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Heal.GetHashCode())
            {
                spells = spells.Remove(spells.IndexOf("Heal"), "Heal".Length);
                tempSpell = "Heal";
                targetType = "SingleAlly";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemySpellList.Add(tempSpell);
                enemySpellType.Add(targetType);
            }
            if (spells.Contains("Revive") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Revive.GetHashCode())
            {
                spells = spells.Remove(spells.IndexOf("Revive"), "Revive".Length);
                tempSpell = "Revive";
                targetType = "SingleAlly";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemySpellList.Add(tempSpell);
                enemySpellType.Add(targetType);
            }
            if (spells.Contains("Smite") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Smite.GetHashCode())
            {
                spells = spells.Remove(spells.IndexOf("Smite"), "Smite".Length);
                tempSpell = "Smite";
                targetType = "Single";
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemySpellList.Add(tempSpell);
                enemySpellType.Add(targetType);
            }

            tempSpell = "";
            if (turnArray[currentTurn].GetComponent<BaseEntityClass>().enemySpellList.Count == 0)
            {
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyTempSpells = 0;
            }
            else
            {
                turnArray[currentTurn].GetComponent<BaseEntityClass>().enemyTempSpells = turnArray[currentTurn].GetComponent<BaseEntityClass>().spellList.GetHashCode();
            }

        }
    }

    #endregion

    #region Player_abilities_spells
    public void abilitySearch(string abilities, int hash)
    {
        //Taunt = 1,
        //Sentinel = 2,
        //Shield_Slam = 4,
        //Shiv = 8,
        //Quick_Attack = 16,
        //Shadow_Strike = 32,
        //Meditate = 64

        int numButtons = 4;

        attackOne.SetActive(true);
        attackTwo.SetActive(true);
        attackThree.SetActive(true);
        attackFour.SetActive(true);

        Button Attack2 = attackTwo.GetComponent<Button>();
        Attack2.onClick.RemoveAllListeners();

        Button Attack3 = attackThree.GetComponent<Button>();
        Attack3.onClick.RemoveAllListeners();

        Button Attack4 = attackFour.GetComponent<Button>();
        Attack4.onClick.RemoveAllListeners();

        if (hash != 0)
        {
            for (int buttonCount = 2; buttonCount < numButtons + 1; buttonCount++)
            {
                string tempAbility = "";
                string targetType = "";
                bool reviveSkill = false;
                if (abilities.Contains("Taunt") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Taunt.GetHashCode())
                {
                    abilities = abilities.Remove(abilities.IndexOf("Taunt"), "Taunt".Length);
                    GameObject.Find("Attack" + buttonCount).GetComponentInChildren<Text>().text = "Taunt";
                    tempAbility = "Taunt";
                    targetType = "Single";
                }
                else if (abilities.Contains("Sentinel") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Sentinel.GetHashCode())
                {
                    abilities = abilities.Remove(abilities.IndexOf("Sentinel"), "Sentinel".Length);
                    GameObject.Find("Attack" + buttonCount).GetComponentInChildren<Text>().text = "Sentinel";
                    tempAbility = "Sentinel";
                    targetType = "Self";
                }
                else if (abilities.Contains("Shield_Slam") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Shield_Slam.GetHashCode())
                {
                    abilities = abilities.Remove(abilities.IndexOf("Shield_Slam"), "Shield_Slam".Length);
                    GameObject.Find("Attack" + buttonCount).GetComponentInChildren<Text>().text = "Shield Slam";
                    tempAbility = "Shield_Slam";
                    targetType = "Single";
                }
                else if (abilities.Contains("Shiv") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Shiv.GetHashCode())
                {
                    abilities = abilities.Remove(abilities.IndexOf("Shiv"), "Shiv".Length);
                    GameObject.Find("Attack" + buttonCount).GetComponentInChildren<Text>().text = "Shiv";
                    tempAbility = "Shiv";
                    targetType = "Single";
                }
                else if (abilities.Contains("Quick_Attack") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Quick_Attack.GetHashCode())
                {
                    abilities = abilities.Remove(abilities.IndexOf("Quick_Attack"), "Quick_Attack".Length);
                    GameObject.Find("Attack" + buttonCount).GetComponentInChildren<Text>().text = "Quick Attack";
                    tempAbility = "Quick_Attack";
                    targetType = "Single";
                }
                else if (abilities.Contains("Shadow_Strike") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Shadow_Strike.GetHashCode())
                {
                    abilities = abilities.Remove(abilities.IndexOf("Shadow_Strike"), "Shadow_Strike".Length);
                    GameObject.Find("Attack" + buttonCount).GetComponentInChildren<Text>().text = "Shadow Strike";
                    tempAbility = "Shadow_Strike";
                    targetType = "Single";
                }
                else if (abilities.Contains("Meditate") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Meditate.GetHashCode())
                {
                    abilities = abilities.Remove(abilities.IndexOf("Meditate"), "Meditate".Length);
                    GameObject.Find("Attack" + buttonCount).GetComponentInChildren<Text>().text = "Meditate";
                    tempAbility = "Meditate";
                    targetType = "Self";
                }
                else if (abilities.Contains("Poison_Sting") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Meditate.GetHashCode())
                {
                    abilities = abilities.Remove(abilities.IndexOf("Poison_Sting"), "Poison_Sting".Length);
                    GameObject.Find("Attack" + buttonCount).GetComponentInChildren<Text>().text = "Poison_Sting";
                    tempAbility = "Poison_Sting";
                    targetType = "Single";
                }
                else
                {
                    GameObject.Find("Attack" + buttonCount).GetComponentInChildren<Text>().text = "";
                    tempAbility = "";
                }

                if (!tempAbility.Equals(""))
                {
                    if (targetType.Equals("Single"))
                    {
                        if (buttonCount == 2)
                        {
                            Attack2.onClick.AddListener(delegate { changeStateToSelection(tempAbility, reviveSkill); });
                        }
                        if (buttonCount == 3)
                        {
                            Attack3.onClick.AddListener(delegate { changeStateToSelection(tempAbility, reviveSkill); });
                        }
                        if (buttonCount == 4)
                        {
                            Attack4.onClick.AddListener(delegate { changeStateToSelection(tempAbility, reviveSkill); });
                        }
                    }
                    else if (targetType.Equals("Self"))
                    {
                        if (buttonCount == 2)
                        {
                            Attack2.onClick.AddListener(delegate { changeStateToSelectionSelf(tempAbility); });
                        }
                        if (buttonCount == 3)
                        {
                            Attack3.onClick.AddListener(delegate { changeStateToSelectionSelf(tempAbility); });
                        }
                        if (buttonCount == 4)
                        {
                            Attack4.onClick.AddListener(delegate { changeStateToSelectionSelf(tempAbility); });
                        }
                    }
                }
            }
        }

        else
        {
            GameObject.Find("Attack2").GetComponentInChildren<Text>().text = "";
            GameObject.Find("Attack3").GetComponentInChildren<Text>().text = "";
            GameObject.Find("Attack4").GetComponentInChildren<Text>().text = "";
        }

        attackOne.SetActive(false);
        attackTwo.SetActive(false);
        attackThree.SetActive(false);
        attackFour.SetActive(false);
    }

    public void spellSearch(string spells, int hash)
    {
        //None = 0,
        //Firebolt = 1,
        //Flaming_Orb = 2,
        //Heal = 4,
        //Group_Heal = 8,
        //Revive = 16,
        //Smite = 32

        spellOne.SetActive(true);
        spellTwo.SetActive(true);
        spellThree.SetActive(true);
        spellFour.SetActive(true);

        int numButtons = 4;

        Button Spell1 = spellOne.GetComponent<Button>();
        Spell1.onClick.RemoveAllListeners();

        Button Spell2 = spellTwo.GetComponent<Button>();
        Spell2.onClick.RemoveAllListeners();

        Button Spell3 = spellThree.GetComponent<Button>();
        Spell3.onClick.RemoveAllListeners();

        Button Spell4 = spellFour.GetComponent<Button>();
        Spell4.onClick.RemoveAllListeners();


        if (hash != 0)
        {
            for (int buttonCount = 1; buttonCount < numButtons + 1; buttonCount++)
            {
                string tempSpell = "";
                string targetType = "";
                bool reviveSkill = false;
                if (spells.Contains("Firebolt") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Firebolt.GetHashCode())
                {
                    spells = spells.Remove(spells.IndexOf("Firebolt"), "Firebolt".Length);
                    GameObject.Find("Spell" + buttonCount).GetComponentInChildren<Text>().text = "Firebolt";
                    tempSpell = "Firebolt";
                    targetType = "Single";
                }
                else if (spells.Contains("Flaming_Orb") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Flaming_Orb.GetHashCode())
                {
                    spells = spells.Remove(spells.IndexOf("Flaming_Orb"), "Flaming_Orb".Length);
                    GameObject.Find("Spell" + buttonCount).GetComponentInChildren<Text>().text = "Flaming Orb";
                    tempSpell = "Flaming_Orb";
                    targetType = "Multi";
                }
                else if (spells.Contains("Group_Heal") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Group_Heal.GetHashCode())
                {
                    spells = spells.Remove(spells.IndexOf("Group_Heal"), "Group_Heal".Length);
                    GameObject.Find("Spell" + buttonCount).GetComponentInChildren<Text>().text = "Multi Heal";
                    tempSpell = "Group_Heal";
                    targetType = "Multi";
                }
                else if (spells.Contains("Heal") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Heal.GetHashCode())
                {
                    spells = spells.Remove(spells.IndexOf("Heal"), "Heal".Length);
                    GameObject.Find("Spell" + buttonCount).GetComponentInChildren<Text>().text = "Heal";
                    tempSpell = "Heal";
                    targetType = "Single";
                }
                else if (spells.Contains("Revive") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Revive.GetHashCode())
                {
                    spells = spells.Remove(spells.IndexOf("Revive"), "Revive".Length);
                    GameObject.Find("Spell" + buttonCount).GetComponentInChildren<Text>().text = "Revive";
                    tempSpell = "Revive";
                    targetType = "Single";
                    reviveSkill = true;
                }
                else if (spells.Contains("Smite") && turnArray[currentTurn].GetComponent<BaseEntityClass>().getMana() >= BaseEntityClass.SkillCosts.Smite.GetHashCode())
                {
                    spells = spells.Remove(spells.IndexOf("Smite"), "Smite".Length);
                    GameObject.Find("Spell" + buttonCount).GetComponentInChildren<Text>().text = "Smite";
                    tempSpell = "Smite";
                    targetType = "Single";
                }
                else
                {
                    GameObject.Find("Spell" + buttonCount).GetComponentInChildren<Text>().text = "";
                    tempSpell = "";
                    targetType = "";
                }

                if (!tempSpell.Equals(""))
                {
                    if (targetType.Equals("Single"))
                    {
                        if (buttonCount == 1)
                        {
                            Spell1.onClick.AddListener(delegate { changeStateToSelection(tempSpell, reviveSkill); });
                        }
                        if (buttonCount == 2)
                        {
                            Spell2.onClick.AddListener(delegate { changeStateToSelection(tempSpell, reviveSkill); });
                        }
                        if (buttonCount == 3)
                        {
                            Spell3.onClick.AddListener(delegate { changeStateToSelection(tempSpell, reviveSkill); });
                        }
                        if (buttonCount == 4)
                        {
                            Spell4.onClick.AddListener(delegate { changeStateToSelection(tempSpell, reviveSkill); });
                        }
                    }
                    else if (targetType.Equals("Multi"))
                    {
                        if (buttonCount == 1)
                        {
                            Spell1.onClick.AddListener(delegate { changeStateToSelectionMulti(tempSpell); });
                        }
                        if (buttonCount == 2)
                        {
                            Spell2.onClick.AddListener(delegate { changeStateToSelectionMulti(tempSpell); });
                        }
                        if (buttonCount == 3)
                        {
                            Spell3.onClick.AddListener(delegate { changeStateToSelectionMulti(tempSpell); });
                        }
                        if (buttonCount == 4)
                        {
                            Spell4.onClick.AddListener(delegate { changeStateToSelectionMulti(tempSpell); });
                        }
                    }
                    else if (targetType.Equals("Self"))
                    {
                        if (buttonCount == 1)
                        {
                            Spell1.onClick.AddListener(delegate { changeStateToSelectionSelf(tempSpell); });
                        }
                        if (buttonCount == 2)
                        {
                            Spell2.onClick.AddListener(delegate { changeStateToSelectionSelf(tempSpell); });
                        }
                        if (buttonCount == 3)
                        {
                            Spell3.onClick.AddListener(delegate { changeStateToSelectionSelf(tempSpell); });
                        }
                        if (buttonCount == 4)
                        {
                            Spell4.onClick.AddListener(delegate { changeStateToSelectionSelf(tempSpell); });
                        }
                    }

                }
            }
        }
        else
        {
            for (int buttonCount = 1; buttonCount < numButtons + 1; buttonCount++)
            {
                GameObject.Find("Spell" + buttonCount).GetComponentInChildren<Text>().text = "";
            }
        }

        spellOne.SetActive(false);
        spellTwo.SetActive(false);
        spellThree.SetActive(false);
        spellFour.SetActive(false);
    }
    #endregion

    #region Player_selection_single
    public void changeStateToSelection(string thisAttack, bool isReviveSkill)
    {
        currentState = BattleState.SELECTIONSINGLE;

        isRevive = isReviveSkill;

        currentAttackString = thisAttack;

        Spells.SetActive(false);
        Items.SetActive(false);
        Run.SetActive(false);
        Attack.SetActive(false);

        attackOne.SetActive(false);
        attackTwo.SetActive(false);
        attackThree.SetActive(false);
        attackFour.SetActive(false);

        spellOne.SetActive(false);
        spellTwo.SetActive(false);
        spellThree.SetActive(false);
        spellFour.SetActive(false);

        cursor.SetActive(true);
    }

    public void checkCurrentSelectionRight()
    {

        if (currSel > (selectionArray.Length - 1))
        {
            currSel = 0;
        }
        if (true == isRevive)
        {
            if (false == selectionArray[currSel].GetComponent<BaseEntityClass>().getIsAlive() && false == selectionArray[currSel].GetComponent<BaseEntityClass>().getIsPlayer())
            {
                currSel++;

                checkCurrentSelectionRight();
            }
        }
        else
        {
            if (false == selectionArray[currSel].GetComponent<BaseEntityClass>().getIsAlive())
            {
                currSel++;

                checkCurrentSelectionRight();
            }
        }
    }

    public void checkCurrentSelectionLeft()
    {
        if (currSel < 0)
        {
            currSel = (selectionArray.Length - 1);
        }

        if (true == isRevive)
        {
            if (false == selectionArray[currSel].GetComponent<BaseEntityClass>().getIsAlive() && false == selectionArray[currSel].GetComponent<BaseEntityClass>().getIsPlayer())
            {
                currSel--;

                checkCurrentSelectionLeft();
            }
        }
        else
        {
            if (false == selectionArray[currSel].GetComponent<BaseEntityClass>().getIsAlive())
            {
                currSel--;

                checkCurrentSelectionLeft();
            }
        }

    }
    #endregion

    #region Player_selection_multi
    public void changeStateToSelectionMulti(string thisAttack)
    {
        currentState = BattleState.SELECTIONMULTI;

        currentAttackString = thisAttack;

        Spells.SetActive(false);
        Items.SetActive(false);
        Run.SetActive(false);
        Attack.SetActive(false);

        attackOne.SetActive(false);
        attackTwo.SetActive(false);
        attackThree.SetActive(false);
        attackFour.SetActive(false);

        spellOne.SetActive(false);
        spellTwo.SetActive(false);
        spellThree.SetActive(false);
        spellFour.SetActive(false);

        cursor.SetActive(true);
        cursor2.SetActive(true);
        cursor3.SetActive(true);
        cursor4.SetActive(true);
    }

    #endregion

    #region Player_selection_self
    public void changeStateToSelectionSelf(string thisAttack)
    {
        currentState = BattleState.SELECTIONSELF;

        currentAttackString = thisAttack;

        Spells.SetActive(false);
        Items.SetActive(false);
        Run.SetActive(false);
        Attack.SetActive(false);

        attackOne.SetActive(false);
        attackTwo.SetActive(false);
        attackThree.SetActive(false);
        attackFour.SetActive(false);

        spellOne.SetActive(false);
        spellTwo.SetActive(false);
        spellThree.SetActive(false);
        spellFour.SetActive(false);

        cursor.SetActive(true);
    }

    #endregion

    #region RunAway
    IEnumerator runningAwayThing()
    {
        yield return new WaitForSeconds(2f);

        if (true == ranAway)
        {
            inBattle = false;
            player.gameObject.SetActive(true);
            battleCam.gameObject.SetActive(false);
            mainCam.gameObject.SetActive(true);

            Destroy(leadEnemy);
            Destroy(thirdEnemy);
            Destroy(secondEnemy);
            Destroy(firstEnemy);

            runText.text = "";
        }
        else
        {
            runText.text = "";
            currentTurn++;
            currentState = BattleState.PROCESSING;
        }
    }

    public void RunKidRun() //This needs to be ideally put somewhere and changed 
    {
        if (UnityEngine.Random.Range(0, 100) <= 100)
        {
            runText.text = "Successfully ran away";
            ranAway = true;
        }

        else
        {
            runText.text = "The enemy blocked your escape";
            ranAway = false;
        }

        currentState = BattleState.RANAWAY;
    }
    #endregion

    #region Winning_&_Losing
    IEnumerator charlieSheenWinning()
    {
        yield return new WaitForSeconds(1f);

        inBattle = false;
        player.gameObject.SetActive(true);
        battleCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);

        Destroy(leadEnemy);
        Destroy(thirdEnemy);
        Destroy(secondEnemy);
        Destroy(firstEnemy);
    }

    IEnumerator charlieSheenLosing()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);

    }
    #endregion

    int enemyExperienceDrop(BaseEntityClass e1)
    {
        int exp = e1.experienceDrop(e1.getEntityType(), e1.level);
        return exp;
    }

    int totalEnemyExperienceDrop(BaseEntityClass e1, BaseEntityClass e2, BaseEntityClass e3, BaseEntityClass e4)
    {
        int exp = enemyExperienceDrop(e1) + enemyExperienceDrop(e2) + enemyExperienceDrop(e3) + enemyExperienceDrop(e4);
        return exp;
    }

    void lairAction()
    {
        if (lairCounter >= 10)
        {
            foreach (GameObject playerNum in playerArray)
            {
                int bossLevelMax = leadEnemy.GetComponent<BaseEntityClass>().getLevel() + 1;
                int bossLevelMin = bossLevelMax - 5;
                if (bossLevelMin < 0)
                {
                    bossLevelMin = 0;
                }
                playerNum.GetComponent<BaseEntityClass>().takeDamage(UnityEngine.Random.Range(bossLevelMin, bossLevelMax));
            }
            lairCounter = 0;
        }
        else
        {
            lairCounter += UnityEngine.Random.Range(0, 3);
        }

        return;
    }


    IEnumerator damageDisplay(string stringAttack)
    {
        if(!isMulti)
        {
            runText.text = turnArray[currentTurn].GetComponent<BaseEntityClass>().getJob() + " used " + stringAttack;

            damageIndicator.GetComponent<Text>().text = turnArray[currentTurn].GetComponent<BaseEntityClass>().getDamageValue().ToString();
        }

        //runText.text = "Attacked!";
        




        yield return new WaitForSeconds(0.5f);



        runText.text = "";
        damageIndicator.GetComponent<Text>().text = "";



    }
    
    
}
