using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCM : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float speedW = 3;
    public float speedR = 9;

    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    public Transform cam;


    public Vector3 velocity;
    public float gravity = -9.81f;

    public Transform grpundCheck;
    public float groundDistance = 0.45f;
    public LayerMask groundMask;
    public bool isGrounded;

    public float jumpHeight = 3f;

    Animator animator;
    bool isjumping;
    bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        //*bloquear cursos en pantalla
        Cursor.lockState = CursorLockMode.Locked;

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        MovePlayer();
        JumpPlayer();
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(grpundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -5f;
            animator.SetBool("isGrounded", true);
            isGround = true;
            animator.SetBool("isJumping", false);
            isjumping = false;
        }
        else
        {
            animator.SetBool("isGrounded", false);
            isGround = false;

            if(isjumping && velocity.y < 0)
            {
                animator.SetBool("isFalling", true);
            }
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothTime, turnSmoothVelocity);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                speed= speedR; 
            }	
            else
            {
                speed= speedW;
            }
                
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    void JumpPlayer()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -gravity * Time.deltaTime);
            animator.SetBool("isJumping", true);
            isjumping = true;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
