using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSensitivity = 10f;
    public float rotateSensitivity = 20f;
    public GameObject cameraRotate;

    private float verticalRotation = 0f;
    private const float MaxVerticalAngle = 60f; // Grados máximos de rotación vertical

    public int coins = 0;
    public TMP_Text scoreText;
    public TMP_Text statusText;

    void Start()
    {
        scoreText.text = "Score: 0";
        statusText.text = "Normal";
        statusText.color = Color.green;
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSensitivity * Time.deltaTime;
        transform.Translate(movement);
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Acumula la rotación vertical y la limita
        verticalRotation -= mouseY * rotateSensitivity * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -MaxVerticalAngle, MaxVerticalAngle);

        // Aplica la rotación horizontal al jugador y la vertical a la cámara
        transform.Rotate(0, mouseX * rotateSensitivity * Time.deltaTime, 0);
        cameraRotate.transform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            coins++;
            scoreText.text = $"Score:  {coins}";
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "modificador")
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            statusText.text = "Diminuto";
            statusText.color = Color.red;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "modificador")
        {
            transform.localScale = new Vector3(1, 1, 1);
            statusText.text = "Normal";
            statusText.color = Color.green;
        }
    }
}


