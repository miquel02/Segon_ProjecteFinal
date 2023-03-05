using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _Collider;
    public float _speed;
    [SerializeField] private float _turnSpeed = 360;
    private Vector3 _input;

    private float normalSpeed = 20;
    private float sprintSpeed = 40;
    private float rollSpeed = 100;

    public bool canMove;
    public bool canRoll;
    public bool hasAttacked;
    public bool hasSpinned;

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
    public bool isMoving;

    private bool isWalk;
    private bool isRun;
    private bool isAttack;
    private bool isRoll;
    private bool isSpin;

    //Particles


    private void Start()
    {
        animator = GetComponent<Animator>();
        canMove = true;
        canRoll = true;
        isTired = false;
        isDead = false;
        _Collider.enabled = true;

        _rb = GetComponent<Rigidbody>();
        _Collider = GetComponent<Collider>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        //GatherInput();
        //Look();

        MyInput();
        //PlayerRegenStamina();

        if (!isDead)
        {

        
            if (GameManager.gameManager.playerStamina.Stamina < 1)
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
                    //transform.Translate(Vector3.forward * rollSpeed * Time.deltaTime);
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
            /*
            if (Input.GetKey(KeyCode.Mouse1))
            {
                //canMove = false;
                animator.SetBool("isBlocking", true);
            }
            else if (!canMove)
            {
                Invoke(nameof(DelayedCanMove), 0.4f);
                animator.SetBool("isBlocking", false);
            }*/

            //Spin
            if (Input.GetKeyDown(KeyCode.Mouse2) && !isSpin)
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

            if(DataPersistance.PlayerStats.numberPotions > 0 && Input.GetKeyDown(KeyCode.Q))
            {
                DataPersistance.PlayerStats.numberPotions--;
                PlayerTakeHeal(30);
            }
        }
        else
        {
            GameManager.gameManager.gameOver = true;
        }
    }

    private void FixedUpdate()
    {
        if (canMove && !isDead)
        {
            Move();
        }  
    }

    /*private void Look()
    {
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }*/

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void Move()
    {
      
            //_rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);

            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

                _rb.AddForce(moveDirection.normalized * _speed * 10f, ForceMode.Force);

    }
    
    public void DelayedCanMove()
    {
        canMove = true;
        canRoll = true;
        //isAttacking = false;
        isRoll = false;
        isAttack = false;
        isSpin = false;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRolling", false);
        animator.SetBool("isSpinning", false);
        _Collider.enabled = true;



    }


    private void PlayerUseStamina(float _staminaAmount)
    {
        GameManager.gameManager.playerStamina.UseStamina(_staminaAmount);
        staminaBar.SetStamina(GameManager.gameManager.playerStamina.Stamina);
    }
    private void PlayerUseInstantStamina(float _staminaAmount)
    {
        GameManager.gameManager.playerStamina.UseInstantStamina(_staminaAmount);
        staminaBar.SetStamina(GameManager.gameManager.playerStamina.Stamina);
    }
    private void PlayerRegenStamina()
    {
        GameManager.gameManager.playerStamina.RegenStamina();
        staminaBar.SetStamina(GameManager.gameManager.playerStamina.Stamina);
    }
    
    public void PlayerTakeDmg(int _dmg)
    {
        GameManager.gameManager.playerHealth.DamageUnit(_dmg);
        healthBar.SetHealth(GameManager.gameManager.playerHealth.Health);
        animator.SetTrigger("isHit");
        
    }

    private void PlayerTakeHeal(int _heal)
    {
        GameManager.gameManager.playerHealth.HealUnit(_heal);
        healthBar.SetHealth(GameManager.gameManager.playerHealth.Health);
    }

    IEnumerator RollTimer()
    {   yield return new WaitForSeconds(0.2f);
        _rb.AddForce(transform.forward * rollSpeed, ForceMode.Impulse);
        //transform.Translate(Vector3.forward * rollSpeed * Time.deltaTime);
        Debug.Log("Rodar");
        
    }
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
