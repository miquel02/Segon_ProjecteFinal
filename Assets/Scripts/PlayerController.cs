using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //This script controlls the player movement and interactions

    //Access the players components
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _Collider;
    //Variables to control the players speed
    private float _turnSpeed = 360;
    private float _speed;
    private float normalSpeed = 20;
    private float sprintSpeed = 40;
    private float rollSpeed = 100;
    //Variables to control player actions
    public bool canMove;
    public bool canRoll;
    public bool hasAttacked;
    public bool hasSpinned;
    //Variables to control player movement
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    //Stats
    public bool isTired;
    [SerializeField] StaminaBar staminaBar;
    [SerializeField] HealthBar healthBar;
    public bool isDead;
    //Animations
    public Animator animator;
    private bool isMoving;
    private bool isWalk;
    private bool isRun;
    private bool isAttack;
    private bool isRoll;
    private bool isSpin;

    private void Start()
    {
        animator = GetComponent<Animator>();//Access the animator
        _rb = GetComponent<Rigidbody>();//Access the rigidbody
        _Collider = GetComponent<Collider>();//Access the collider
        _rb.freezeRotation = true;//We freeze the player rotations
        _Collider.enabled = true;//Enable the collider
        //We set the variables to make sure they ara in the correct state
        canMove = true;
        canRoll = true;
        isTired = false;
        isDead = false;            
    }

    private void Update()
    {

        MyInput();//We get the horizontal and vertical inputs
        //"If" to block the actons when we are dead or the game is paused
        if (!isDead && GameManager.gameManager.isPaused == false)
        {
            if (GameManager.gameManager.playerStamina.Stamina < 1)//When we run out of stamina we are tired
            {
                isTired = true;
            }
            if (GameManager.gameManager.playerStamina.Stamina > 20)
            {
                isTired = false;
                isRoll = false;
                isWalk = false;
                isRun = false;
                isAttack = false;
                isSpin = false;
            }
            //Walk
            if (horizontalInput != 0f || verticalInput != 0f)
            {  
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
            //Roll
            if (Input.GetKeyDown(KeyCode.Space) && !isRoll)
            {
                isRoll = true;
                isWalk = false;
                isRun = false;
                isAttack = false;
                isSpin = false;
                if (isRoll && !isTired)
                {
                    _Collider.enabled = false;
                    PlayerUseInstantStamina(40f);
                    animator.SetBool("isRolling", true);
                    StartCoroutine(RollTimer());
                    Invoke(nameof(DelayedCanMove), 0.4f);
                }    
            }
            //Sprint
            if(GameManager.gameManager.playerStamina.Stamina > 0 && !isTired)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if(!isRoll && !isSpin && !isAttack)
                    {
                        _speed = sprintSpeed;
                        animator.SetBool("isSprint", true);
                        PlayerUseStamina(5f);
                        Debug.Log("Sprint");
                    }
                }
                else
                {
                    _speed = normalSpeed;
                    animator.SetBool("isSprint", false);
                    PlayerRegenStamina();
                }
            }
            else
            {
                animator.SetBool("isSprint", false);
                _speed = normalSpeed;
                PlayerRegenStamina();
            }
        
            //Attack
            if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttack)
            {
                isRoll = false;
                isWalk = false;
                isRun = false;
                isAttack = true;
                isSpin = false;
            
                if (isAttack && !isTired)
                {
                    animator.SetBool("isAttacking", true);
                    PlayerUseInstantStamina(20f);
                    hasAttacked = true;
                    Invoke(nameof(DelayedCanMove), 0.2f);
                }
            }
            //Spin
            if (Input.GetKeyDown(KeyCode.E) && !isSpin)
            {
                isRoll = false;
                isWalk = false;
                isRun = false;
                isAttack = false;
                isSpin = true;

                if (isSpin && !isTired)
                {
                    hasSpinned = true;
                    PlayerUseInstantStamina(40f);
                    animator.SetBool("isSpinning", true); 
                    Invoke(nameof(DelayedCanMove), 0.2f);           
                }         
            }   
            //Dies
            if(GameManager.gameManager.playerHealth.currentHealth <= 0)
            {
                isRoll = false;
                isWalk = false;
                isRun = false;
                isAttack = false;
                isSpin = false;
                animator.SetBool("IsDying", true);
                Debug.Log("dead");
                isDead = true;
            }
            //Heal
            if(DataPersistance.PlayerStats.numberPotions > 0 && Input.GetKeyDown(KeyCode.Q))
            {
                DataPersistance.PlayerStats.numberPotions--;
                PlayerTakeHeal(30);
            }
        }
        if(isDead)//If we die game over is set to true
        {
            GameManager.gameManager.gameOver = true;
        }
    }

    private void FixedUpdate()
    {
        if (canMove && !isDead)//If we can move an we arent dead we move
        {
            Move();
        }  
    }
    private void MyInput()//We access the inputs
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void Move()//Function
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        _rb.AddForce(moveDirection.normalized * _speed * 10f, ForceMode.Force);
    }
    public void DelayedCanMove()//Function to controll timings between actions and animations
    {
        canMove = true;
        canRoll = true;
        isRoll = false;
        isAttack = false;
        isSpin = false;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRolling", false);
        animator.SetBool("isSpinning", false);
        _Collider.enabled = true;
    }

    private void PlayerUseStamina(float _staminaAmount)//Function to use stamina over time
    {
        GameManager.gameManager.playerStamina.UseStamina(_staminaAmount);
        staminaBar.SetStamina(GameManager.gameManager.playerStamina.Stamina);
    }
    private void PlayerUseInstantStamina(float _staminaAmount)//Function to use stamina
    {
        GameManager.gameManager.playerStamina.UseInstantStamina(_staminaAmount);
        staminaBar.SetStamina(GameManager.gameManager.playerStamina.Stamina);
    }
    private void PlayerRegenStamina()//Function to regen stamina
    {
        GameManager.gameManager.playerStamina.RegenStamina();
        staminaBar.SetStamina(GameManager.gameManager.playerStamina.Stamina);
    }
    
    public void PlayerTakeDmg(int _dmg)//Function to take damage
    {
        GameManager.gameManager.playerHealth.DamageUnit(_dmg);
        healthBar.SetHealth(GameManager.gameManager.playerHealth.Health);
        animator.SetTrigger("isHit");    
    }
    private void PlayerTakeHeal(int _heal)//Function to heal
    {
        GameManager.gameManager.playerHealth.HealUnit(_heal);
        healthBar.SetHealth(GameManager.gameManager.playerHealth.Health);
    }

    IEnumerator RollTimer()//Courroutine to controll the roll animation
    {   yield return new WaitForSeconds(0.2f);
        _rb.AddForce(transform.forward * rollSpeed, ForceMode.Impulse);    
    }
}
