using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraLook : MonoBehaviour
{
    public float rotateSensitivity = 100f;
    public Transform playerBody;

    public float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //* Oculta el cursor y lo bloquea en el centro de la pantalla
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")*rotateSensitivity*Time.deltaTime; //* Obtiene el movimiento del ratón en el eje X
        float mouseY = Input.GetAxis("Mouse Y")*rotateSensitivity*Time.deltaTime; //* Obtiene el movimiento del ratón en el eje Y

        xRotation -= mouseY; //* Acumula la rotación vertical
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //* Limita la rotación vertical
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //* Rota la cámara en el eje X (vertical)

        playerBody.Rotate(Vector3.up * mouseX); //* Rota el cuerpo del jugador en el eje Y (horizontal)
        
    }
}
