using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float horizontalMovement; //joystick horizontal input
    private float verticalMovement; //joystick horizontal input
    // Player private variables
    private bool iddle = true;
    private bool canJump = true;
    private bool doubleJump = true;
    private bool resetJump = false;
    float limitedSpeed;
    float playerGravity;
    float playerDrag;
    private bool ladderClimbing;
    private bool swimming;
    private Animator playerAnimator;

    bool playaudio = false;

    //Player Stats
    public float playerSpeed;
    public float maxSpeed;
    public float jumpPower;
    public float climbSpeed;
    public float swimmingSpeed;

    //Player Class animation
    public RuntimeAnimatorController warriorAnimator;
    public RuntimeAnimatorController mageAnimator;
    public RuntimeAnimatorController priestAnimator;
    public RuntimeAnimatorController thiefAnimator;

    private Rigidbody2D playerRigibody;

 
    //Audio and music
    public AudioSource jumpSound;
    public AudioSource doubleJumpSound;
    public AudioSource laddersSound;
    public enum PlayerClass {
        Warrior, Mage, Priest, Thief
    };
    PlayerClass m_playerClass;

    // Use this for initialization
    void Start()
    {
        playerRigibody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        //save initial values for player rigibody
        playerGravity = playerRigibody.gravityScale;
        playerDrag = playerRigibody.drag;


        m_playerClass = PlayerClass.Warrior;

         //source = GetComponent<AudioSource>();

       // Animator playerAnimator = transform.gameObject.GetComponent<Animator>();
        //playerAnimator.runtimeAnimatorController = warriorAnimator;

    }

    // Update is called once per frame
    void Update()
    {
        if (canJump == true || doubleJump == true)
            iddle = true;

        //update player animation depends on x speed and if he is jumping
        playerAnimator.SetFloat("Speed", Mathf.Abs(playerRigibody.velocity.x));
        playerAnimator.SetBool("Grounded", iddle);
        playerAnimator.SetBool("isClimbing", ladderClimbing);
        playerAnimator.SetBool("isSwimming", swimming);

        //Change x local size to negative to change the direction the player is looking at
        if (horizontalMovement > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (horizontalMovement < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (verticalMovement == -0.0f && canJump == false)
            resetJump = true;



    }

    void FixedUpdate()
    {


        ////////////////////////////////// MOVE LEFT AND RIGHT ////////////////////////////////////

        //horizontalMovement = Input.GetAxis("Horizontal");
        //verticalMovement = Input.GetAxis("Vertical");
        //playerRigibody.AddForce(Vector2.right * playerSpeed * horizontalMovement);
        //limitedSpeed = Mathf.Clamp(playerRigibody.velocity.x, -maxSpeed, maxSpeed); //limited max speed
        //playerRigibody.velocity = new Vector2(limitedSpeed, playerRigibody.velocity.y);

        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        playerRigibody.velocity = new Vector2(horizontalMovement * 3, playerRigibody.velocity.y);




        //////////////////////////// PLAYER JUMP //////////////////////////////////////////////
        if (verticalMovement > 0.1 && canJump == true && ladderClimbing == false && swimming == false) //joystick up we jump
        {
            playerRigibody.velocity = new Vector2(playerRigibody.velocity.x * 0.8f, 0); //reduce x speed on jumping
            playerRigibody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //jump force
            canJump = false;
            doubleJump = true;
            resetJump = false;
            //jumpSound.Play();
        }

        if (verticalMovement > 0.1 && doubleJump == true && ladderClimbing == false && swimming == false && resetJump == true) //joystick up we jump
        {
            playerRigibody.velocity = new Vector2(playerRigibody.velocity.x * 0.8f, 0); //reduce x speed on jumping
            playerRigibody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //jump force
            canJump = false;
            doubleJump = false;
            //doubleJumpSound.Play();

        }

        ////////////////////// LADDER MOVE ///////////////////

        if (ladderClimbing == true)
        {
            playaudio = true;
            playerRigibody.gravityScale = 0f;
            playerRigibody.drag = 15f;

            if (verticalMovement > 0.7f || verticalMovement < -0.7f)
            {
                playerRigibody.AddForce(Vector2.up * climbSpeed * verticalMovement);
                float limitedClimbSpeed = Mathf.Clamp(playerRigibody.velocity.y, -climbSpeed, climbSpeed); //limited max speed
                playerRigibody.velocity = new Vector2(playerRigibody.velocity.x, limitedClimbSpeed);

                //laddersSound.Play();

            }           
                //laddersSound.Stop();

        }
        else
        {
            playerRigibody.gravityScale = playerGravity;
            playerRigibody.drag = playerDrag;

        }

        ////////////////////////// SWIMMING //////////////////
        if (swimming == true && ladderClimbing == false)
        {
            playerRigibody.gravityScale = 0.50f;
            playerRigibody.drag = 8f;

            if (verticalMovement > 0.3f)
            {
                playerRigibody.AddForce(Vector2.up * swimmingSpeed * verticalMovement);
                float limitedClimbSpeed = Mathf.Clamp(playerRigibody.velocity.y, -swimmingSpeed, swimmingSpeed); //limited max speed
                playerRigibody.velocity = new Vector2(playerRigibody.velocity.x, limitedClimbSpeed);

            }

        }
        else
        {
            if (swimming == false && ladderClimbing == false)
            {

            
                playerRigibody.gravityScale = playerGravity;
                playerRigibody.drag = playerDrag;
            }

        }



    }


    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            if(m_playerClass == PlayerClass.Thief)
            {
                canJump = true;
                doubleJump = false;
            }
            else
            {
                canJump = false;
                doubleJump = true;
            }


        }
        if (coll.gameObject.tag == "Wall")
        {
            playerRigibody.velocity = new Vector2(horizontalMovement * 0, playerRigibody.velocity.y);


        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wall" && m_playerClass== PlayerClass.Thief)
        {
            canJump = true;
            doubleJump = false;

        }

    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            ladderClimbing = true;
            canJump = false;
            doubleJump = false;
        }

        if (collision.gameObject.tag == "Water")
        {
            swimming = true;
            canJump = false;
            doubleJump = false;
        }




    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            ladderClimbing = false;
            playerRigibody.velocity = new Vector2(playerRigibody.velocity.x, 0);
            //Debug.Log(playerRigibody.velocity);


        }

        if (collision.gameObject.tag == "Water")
        {
            swimming = false;
            //canJump = false;
            //doubleJump = false;
            //playerRigibody.velocity = new Vector2(playerRigibody.velocity.x, 0);
            ////Debug.Log(playerRigibody.velocity);

        }



    }


    public void ChangePlayer(int newPlayer)
    {

        int i = (int)newPlayer;

        switch (i)
        {
            case 0:
                {
                    playerAnimator.runtimeAnimatorController = warriorAnimator;
                    m_playerClass = PlayerClass.Warrior;
                    break;
                }

            case 1:
                {
                    playerAnimator.runtimeAnimatorController = mageAnimator;
                    m_playerClass = PlayerClass.Mage;
                    break;
                }

            case 2:
                {
                    playerAnimator.runtimeAnimatorController = priestAnimator;
                    m_playerClass = PlayerClass.Priest;
                    break;
                }

            case 3:
                {
                    playerAnimator.runtimeAnimatorController = thiefAnimator;
                    m_playerClass = PlayerClass.Thief;
                    break;
                }

            default:
                {
                    playerAnimator.runtimeAnimatorController = warriorAnimator;
                    m_playerClass = PlayerClass.Warrior;
                    break;
                }
        }


    }

    public PlayerClass GetPlayerClass()
    {
        return m_playerClass;
    }

    public void SetJumpTrue()
    {
        canJump = true;
        doubleJump = false;
    }


}

