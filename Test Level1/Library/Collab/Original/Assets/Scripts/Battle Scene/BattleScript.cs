using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleScript : MonoBehaviour {

    public GameObject player;
    public Text runText;

    public bool inBattle;
    private bool ranAway;
    public GameObject mageBattleCharacter, clericBattleCharacter, rogueBattleCharacter, tankBattleCharacter;
    private GameObject enemy1, enemy2, enemy3, leadEnemy;
    private GameObject firstEnemy, secondEnemy, thirdEnemy;

    public GameObject Empty, Skeleton, Bat, Hermit;
    private GameObject[] enemyArray;
    public GameObject[] playerArray;
    public GameObject[] turnArray;
    public GameObject[] selectionArray;

    public Button AttackButton;
    
    public Camera mainCam;
    public Camera battleCam;

    //UI shtuff Doughnut touche
    public Button attackSelInMenu;
    public Button AttackStandard;
    public GameObject attack1;
    public GameObject attack2;
    public GameObject attack3;
    public GameObject attack4;
    public GameObject Spells;
    public GameObject Items;
    public GameObject Run;
    public GameObject Attack;
    public GameObject BattleCam;

    public GameObject cursor;

    public GameObject spell1;
    public GameObject spell2;
    public GameObject spell3;
    public GameObject spell4;

    public int currentTurn;
    public int currSel = 0;

    public bool inputWait = false;
    public bool selectionWait = false;

    public string currentAttackString;

    public enum BattleState
    {
        START,
        PLAYERCHOICE,
        ENEMYCHOICE,
        SELECTION,
        WIN,
        LOSE,
        RANAWAY,
        PROCESSING
    }

    private BattleState currentState;

    // Use this for initialization
    void Start () {
        inBattle = false;
        playerArray = new GameObject[] { mageBattleCharacter, clericBattleCharacter, rogueBattleCharacter, tankBattleCharacter };
        enemyArray = new GameObject[] {Empty, Empty, Empty, Skeleton, Bat, Hermit, Skeleton, Bat, Hermit };
    }

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
        enemy3 = enemyArray[UnityEngine.Random.Range(0, enemyArray.Length - 1)];
        enemy2 = enemyArray[UnityEngine.Random.Range(0, enemyArray.Length - 1)];
        enemy1 = enemyArray[UnityEngine.Random.Range(0, enemyArray.Length - 1)];

        leadEnemy.GetComponent<BaseEntityClass>().ID = 4;
        eID4 = leadEnemy.GetComponent<BaseEntityClass>().ID;
        leadEnemy.GetComponent<BaseEntityClass>().Initialise();
        leadEnemy.transform.position = new Vector3(-30 - (eID4 * 0.75f), 2.5f, collidedEnemy.transform.position.z);

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
        
        selectionArray = new GameObject[] { mageBattleCharacter, clericBattleCharacter, rogueBattleCharacter, tankBattleCharacter, leadEnemy, thirdEnemy, secondEnemy, firstEnemy };

        sortTurnOrder();

        inBattle = true;

        currentState = BattleState.START;
        
    }

    // Update is called once per frame
    void Update () {
        // Awaits input for player within menu
        // Moves cursor with analogue stick
        // Click stick1 to select
        // Click stick2 to back
        if (BattleCam.activeSelf)
        {
            if (Input.GetButton("Back"))
            {
                if (true != Attack.activeSelf)
                {
                    Attack.SetActive(true);
                    Spells.SetActive(true);
                    Items.SetActive(true);
                    Run.SetActive(true);
                    attack1.SetActive(false);
                    attack2.SetActive(false);
                    attack3.SetActive(false);
                    attack4.SetActive(false);

                    spell1.SetActive(false);
                    spell2.SetActive(false);
                    spell3.SetActive(false);
                    spell4.SetActive(false);

                    AttackStandard.Select();
                    AttackStandard.OnSelect(null);
                }
            }
            
            //switch battlestate 
            BattleSequence();
        }



        // Highlight the entity and its turn
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

                //Debug.Log("Type: " + turnArray[currentTurn].GetComponent<BaseEntityClass>().getEntityType() + ", Turn number: " + currentTurn);

                if (true != attack1.activeSelf && true != spell1.activeSelf)
                {
                    Spells.SetActive(true);
                    Items.SetActive(true);
                    Run.SetActive(true);
                    Attack.SetActive(true);                   
                }

                cursor.SetActive(false);

                // Await UI input:
                // Basic Attack ->
                // Ability ->
                // Spell ->
                //spell1 = turnArray[currentTurn].GetComponent<CharacterClass>().;

                // Items ->
                // Defend ->
                // Run Away ->
                //
                // After player turn:
                // If TurnArray counter is not at the end
                // {Incriment TurnArray counter by 1}
                // else
                // {TurnArray counter = 0}

                Run.GetComponent<Button>().onClick.AddListener(delegate { RunKidRun(); });

                //currentState = BattleState.PROCESSING;

                break;
            #endregion

            #region ENEMYCHOICE
            case (BattleState.ENEMYCHOICE):
                //  Do enemy turn
                // If TurnArray counter is not at the end
                // {Incriment TurnArray counter by 1}
                // else
                // {TurnArray counter = 0}           
                
                turnArray[currentTurn].GetComponent<BaseEntityClass>().EnemyRunAway();

                //Debug.Log("Type: " + turnArray[currentTurn].GetComponent<BaseEntityClass>().getEntityType() + ", Turn number: " + currentTurn);            

                BaseEntityClass target = playerArray[UnityEngine.Random.Range(0, 4)].GetComponent<BaseEntityClass>();
                turnArray[currentTurn].GetComponent<BaseEntityClass>().Attack(target);

                //int AttackIndex = UnityEngine.Random.Range(0, turnArray[currentTurn].GetComponent<EnemyClass>().AttackStorage.Length);
                

                if (true == turnArray[currentTurn].GetComponent<BaseEntityClass>().getEscapedBattle())
                {
                    turnArray[currentTurn].SetActive(false);
                    turnArray[currentTurn].GetComponent<BaseEntityClass>().setAlive(false);
                    currentTurn++;
                    currentState = BattleState.PROCESSING;
                }

                //if (turnArray[currentTurn].GetComponent<EnemyClass>().mana >= turnArray[currentTurn].GetComponent<EnemyClass>().AttackStorage[AttackIndex].manaCost)
                //{
                //turnArray[currentTurn].GetComponent<EnemyClass>().AttackStorage[AttackIndex];

                if (currentTurn != turnArray.Length)
                {
                    currentTurn++;
                }

                //currentState = BattleState.PROCESSING;
                //}

                //else
                //{
                //    AttackIndex = UnityEngine.Random.Range(0, turnArray[currentTurn].GetComponent<EnemyClass>().AttackStorage.Length);
                //}



                currentState = BattleState.PROCESSING;

                break;
            #endregion

            #region SELECTION
            case (BattleState.SELECTION):

                //StartCoroutine(framewaiter());

                if (Input.GetButton("Back")/* && false == inputWait*/)
                {
                    currentState = BattleState.PLAYERCHOICE;
                }

                GameObject currentSelection = selectionArray[currSel];

                if (Input.GetAxis("Horizontal") > 0.2/* && false == inputWait*/)
                {
                    currSel++;

                    checkCurrentSelectionRight();

                    //inputWait = true;

                    System.Threading.Thread.Sleep(500);
                }

                if (Input.GetAxis("Horizontal") < -0.2/* && false == inputWait*/)
                {
                    currSel--;

                    checkCurrentSelectionLeft();

                    //inputWait = true;

                    System.Threading.Thread.Sleep(500);
                }

                //Debug.Log(currSel);
                //Debug.Log(currentSelection);

                cursor.transform.position = new Vector3(currentSelection.transform.position.x, currentSelection.transform.position.y + 1f, 0f);
                
                if (Input.GetButtonUp("Submit")/* && false == selectionWait*/)
                {
                    //find attack/ability/spell with current string
                    System.Type thisType = turnArray[currentTurn].GetComponent<BaseEntityClass>().GetType();
                    System.Reflection.MethodInfo theMethod = thisType.GetMethod(currentAttackString);
                    theMethod.Invoke(turnArray[currentTurn].GetComponent<BaseEntityClass>(), new object[] { currentSelection.GetComponent<BaseEntityClass>() });

                    currentTurn++;
                    currentState = BattleState.PROCESSING;
                }

                StartCoroutine(waitForInput());

                break;
            #endregion

            #region WIN
            case (BattleState.WIN):
           
                inBattle = false;
                player.gameObject.SetActive(true);
                battleCam.gameObject.SetActive(false);
                mainCam.gameObject.SetActive(true);
                break;
            #endregion

            #region LOSE
            case (BattleState.LOSE):
                runText.text = "Oh dear, you are dead!";
                Debug.Log("bitches be dead");
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
                    //System.Threading.Thread.Sleep(2000);
                    //StartCoroutine(framewaiter());
                    /*
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
                    */
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

                AttackStandard.Select();
                AttackStandard.OnSelect(null);

                if (currentTurn == turnArray.Length - 1)
                {
                    currentTurn = 0;
                }

                checkCurrentTurn();

                if (true != tankBattleCharacter.GetComponent<BaseEntityClass>().isAlive && true != rogueBattleCharacter.GetComponent<BaseEntityClass>().isAlive && true != clericBattleCharacter.GetComponent<BaseEntityClass>().isAlive && true != mageBattleCharacter.GetComponent<BaseEntityClass>().isAlive)
                 {
                    currentState = BattleState.LOSE;
                 }

                if (true != leadEnemy.GetComponent<BaseEntityClass>().getIsAlive() && true != thirdEnemy.GetComponent<BaseEntityClass>().getIsAlive() && true != secondEnemy.GetComponent<BaseEntityClass>().getIsAlive() && true != firstEnemy.GetComponent<BaseEntityClass>().getIsAlive())
                {
                    currentState = BattleState.WIN;
                }

                if (turnArray[currentTurn].tag == "BattleAlly")
                {
                    if (true == turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsAlive() && false == turnArray[currentTurn].GetComponent<BaseEntityClass>().getEscapedBattle())
                    {
                        int abilitiesHash = turnArray[currentTurn].GetComponent<BaseEntityClass>().abilityList.GetHashCode();

                        if (abilitiesHash != 0)
                        {
                            string abilitiesString = turnArray[currentTurn].GetComponent<BaseEntityClass>().abilityList.ToString();
                            abilitySearch(abilitiesString);
                        }

                        attack1.GetComponent<Button>().onClick.AddListener(delegate { changeStateToSelection("Attack"); });
                        //attack2.GetComponent<Button>().onClick.AddListener(delegate { changeStateToSelection(abilitiesString.); });
                        

                        int spellsHash = turnArray[currentTurn].GetComponent<BaseEntityClass>().spellList.GetHashCode();
                        string spellsString = turnArray[currentTurn].GetComponent<BaseEntityClass>().spellList.ToString();

                        //spell1.GetComponent<Button>().onClick.AddListener(delegate { changeStateToSelection("Heal"); });


                        currentState = BattleState.PLAYERCHOICE;
                    }
                    else
                    {
                        currentTurn++;
                    }
                }

                if (turnArray[currentTurn].tag == "BattleEnemy")
                {
                    if (true == turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsAlive() && false == turnArray[currentTurn].GetComponent<BaseEntityClass>().getEscapedBattle())
                    {
                        currentState = BattleState.ENEMYCHOICE;
                    }
                    else if(false == turnArray[currentTurn].GetComponent<BaseEntityClass>().getIsAlive())
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
          
                break;
            #endregion
        }
    }

    IEnumerator waitForInput()
    {
        yield return new WaitForSeconds(5f);

        inputWait = false;
    }

    public void checkCurrentSelectionRight()
    {
        
        if (currSel > (selectionArray.Length - 1))
        {
            currSel = 0;
        }

        if (false == selectionArray[currSel].GetComponent<BaseEntityClass>().getIsAlive())
        {
            currSel++;

            checkCurrentSelectionRight();
        }
    }
    public void checkCurrentSelectionLeft()
    {
        if (currSel < 0)
        {
            currSel = (selectionArray.Length - 1);
        }

        if (false == selectionArray[currSel].GetComponent<BaseEntityClass>().getIsAlive())
        {
            currSel--;

            checkCurrentSelectionLeft();
        }

    }

    public void checkCurrentTurn()
    {
        if (currentTurn == turnArray.Length - 1)
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

    public void changeStateToSelection(string thisAttack)
    {
        currentState = BattleState.SELECTION;

        currentAttackString = thisAttack;

        Spells.SetActive(false);
        Items.SetActive(false);
        Run.SetActive(false);
        Attack.SetActive(false);

        attack1.SetActive(false);
        attack2.SetActive(false);
        attack3.SetActive(false);
        attack4.SetActive(false);

        spell1.SetActive(false);
        spell2.SetActive(false);
        spell3.SetActive(false);
        spell4.SetActive(false);

        cursor.SetActive(true);
    }
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
        if(UnityEngine.Random.Range(0,100) <= 100)
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

    public void abilitySearch(string abilities)
    {
        //int numButtons = 4;
        Button attackTwo = attack2.GetComponent<Button>();

        //for (int buttonCount = 2; buttonCount < numButtons + 2; buttonCount++)
        //{
            if (abilities.Contains("Taunt"))
            {
                abilities.Remove(abilities.IndexOf("Taunt"));
                //attack + buttonCount

            }
            else if(abilities.Contains("Sentinel"))
            {
                abilities.Remove(abilities.IndexOf("Sentinel"));
            
            }
            else if (abilities.Contains("Shield_Slam"))
            {
                abilities.Remove(abilities.IndexOf("Shield_Slam"));
            
            }
            else if (abilities.Contains("Shiv"))
            {
                abilities.Remove(abilities.IndexOf("Shiv"));
                attackTwo.onClick.AddListener(delegate { changeStateToSelection("Shiv"); });
            }
            else if (abilities.Contains("Quick_Attack"))
            {
                abilities.Remove(abilities.IndexOf("Quick_Attack"));

            }
            else if (abilities.Contains("Shadow_Strike"))
            {
                abilities.Remove(abilities.IndexOf("Shadow_Strike"));

            }
            else if (abilities.Contains("Meditate"))
            {
                abilities.Remove(abilities.IndexOf("Meditate"));

            }
            else
            {

            }
        //}
    }
}
