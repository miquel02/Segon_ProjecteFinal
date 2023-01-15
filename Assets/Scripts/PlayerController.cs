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
    private float rollSpeed = 15;

    public bool canMove;
    public bool canRoll;
    public bool isAttacking;
    public bool hasAttacked;


    //Animationa
    public Animator animator;
    public bool isMoving;


    private void Start()
    {
        animator = GetComponent<Animator>();
        canMove = true;
        canRoll = true;
    }

    private void Update()
    {
        GatherInput();
        Look();

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = sprintSpeed;
            
            animator.SetBool("isSprint", true);
        }
        else
        {
            _speed = normalSpeed;
            
            animator.SetBool("isSprint", false);
        }

        //Attack
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            hasAttacked = true;
            isAttacking = true;
            canMove = false;
            animator.SetBool("isAttacking", true);
        }
        else if(!canMove)
        {
            //isAttacking = false;
            Invoke(nameof(DelayedCanMove), 0.4f);
            animator.SetBool("isAttacking", false);
            //isAttacking = false;

        }

        //Roll
        if (Input.GetKeyDown(KeyCode.Space) && canRoll)
        {
            canRoll = false;
            
            canMove = false;
            animator.SetBool("isRolling", true);
        }
        else if (!canMove && !canRoll)
        {
            Invoke(nameof(DelayedCanMove), 0.4f);
            transform.Translate(Vector3.forward * rollSpeed * Time.deltaTime);
            animator.SetBool("isRolling", false);
        }

        //Walk
        if (_input != Vector3.zero) 
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
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

    }

}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}


