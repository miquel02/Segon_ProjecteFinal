using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private float _speed;
    [SerializeField] private float _turnSpeed = 360;
    private Vector3 _input;

    private float normalSpeed = 6;
    private float sprintSpeed = 10;
    private float rollSpeed = 30;

    public bool canMove;
    public bool canRoll;
    public bool hasAttacked;
    public bool hasSpinned;

    //Stats
    public float currentEnergy;
    public float maxEnergy;
    [SerializeField] StaminaBar staminaBar;

    //Animations
    public Animator animator;
    public bool isMoving;

    public bool isWalk;
    public bool isRun;
    public bool isAttack;
    public bool isRoll;
    public bool isSpin;


    private void Start()
    {
        animator = GetComponent<Animator>();
        canMove = true;
        canRoll = true;
        maxEnergy = 100;
        currentEnergy = maxEnergy;
    }

    private void Update()
    {
        GatherInput();
        Look();
        //PlayerRegenStamina();


        //Walk
        if (_input != Vector3.zero)
        {
           
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
            //InvokeRepeating("RestoreEnergy", 1f, 1f);
        }

        //Roll
        if (Input.GetKeyDown(KeyCode.Space) && currentEnergy != 0 && !isRoll)
        {
            isRoll = true;
            isWalk = false;
            isRun = false;
            isAttack = false;
            isSpin = false;

            if (isRoll)
            {

                animator.SetBool("isRolling", true);
                transform.Translate(Vector3.forward * rollSpeed * Time.deltaTime);
                Invoke(nameof(DelayedCanMove), 0.4f);
            }    
        }
   

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(!isRoll && !isSpin && !isAttack)
            {
                _speed = sprintSpeed;
                animator.SetBool("isSprint", true);
            }
        }
        else
        {
            _speed = normalSpeed;
            animator.SetBool("isSprint", false);
        }

        //Attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentEnergy != 0 && !isAttack)
        {
            isRoll = false;
            isWalk = false;
            isRun = false;
            isAttack = true;
            isSpin = false;
            
            if (isAttack)
            {
                animator.SetBool("isAttacking", true);
                hasAttacked = true;
                Invoke(nameof(DelayedCanMove), 0.2f);
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            //canMove = false;
            animator.SetBool("isBlocking", true);
        }
        else if (!canMove)
        {
            Invoke(nameof(DelayedCanMove), 0.4f);
            animator.SetBool("isBlocking", false);
        }

        //Spin
        if (Input.GetKeyDown(KeyCode.Mouse2) && currentEnergy != 0 && !isSpin)
        {
            isRoll = false;
            isWalk = false;
            isRun = false;
            isAttack = false;
            isSpin = true;

            if (isSpin)
            {
                hasSpinned = true;
                animator.SetBool("isSpinning", true); 
                Invoke(nameof(DelayedCanMove), 0.2f);           
            }         
        }       
    }

    void LateUpdate()
    {

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }  
    }
    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        _rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);
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


    }

    public void RestoreEnergy(int energySpeed)
    {
        currentEnergy = (currentEnergy + 5)*energySpeed;
        
    }
    
    private void PlayerUseStamina(float staminaAmount)
    {
        GameManager.gameManager.playerStamina.UseStamina(staminaAmount);
        staminaBar.SetStamina(GameManager.gameManager.playerStamina.Stamina);
    }
    private void PlayerRegenStamina()
    {
        GameManager.gameManager.playerStamina.RegenStamina();
        staminaBar.SetStamina(GameManager.gameManager.playerStamina.Stamina);
    }
    

}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
