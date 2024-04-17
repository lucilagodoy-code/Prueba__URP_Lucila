using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovemementFPS_CC : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    

    Vector3 velocity;
    public float gravity = -9.81f;

    public Transform grpundCheck;
    public float groundDistance = 0.45f;
    public LayerMask groundMask;
    public bool isGrounded;

    public float jumpHeight = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(grpundCheck.position, groundDistance, groundMask); //* Comprueba si el jugador está en el suelo

        if(isGrounded && velocity.y < 0) //* Si está en el suelo y la velocidad vertical es menor que 0
        {
            velocity.y = -2f; //* Aplica una velocidad vertical de -2
        }

        float x = Input.GetAxisRaw ("Horizontal"); //* Obtiene el movimiento del ratón en el eje X
        float z = Input.GetAxisRaw ("Vertical"); //* Obtiene el movimiento del ratón en el eje Y

        Vector3 move = transform.right * x + transform.forward * z; //* Calcula el movimiento del jugador

        controller.Move(move*speed*Time.deltaTime); //* Mueve al jugador

        if(Input.GetButtonDown("Jump") && isGrounded) //* Si pulsa el botón de saltar y está en el suelo
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -gravity * Time.deltaTime); //* Aplica una velocidad vertical
        }
        velocity.y += gravity * Time.deltaTime; //* Aplica la gravedad
        controller.Move(velocity*Time.deltaTime); //* Mueve al jugador Verticalmente hacia abajo
    }
}