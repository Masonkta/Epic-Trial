using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public gameHandler gameScript;
    public bool isPlayerOne;

    [Header("Jumping")]
    public float jumpStrength;
    public bool isGrounded;

    [Header("Turning")]
    public float horizontalTurnAmount;
    public float horizontalTurnSensitivity;
    public float verticalTurnAmount;
    public float verticalTurnSensitivity;
    public Transform Camera;


    Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        checkIsGrounded();

        // Turning // 
        horizontalTurns();
    }


    ///////// Jumping //////////

    void OnControllerJump()
    {
        //if (isGrounded)
        if (isPlayerOne && gameScript.playerOneControls == "Controller")
            rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);

        //if (isGrounded)
        if (!isPlayerOne && gameScript.playerTwoControls == "Controller")
            rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
    }

    void OnKeyboardJump()
    {
        if (isPlayerOne && gameScript.playerOneControls == "Keyboard")
            rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);

        if (!isPlayerOne && gameScript.playerTwoControls == "Keyboard")
            rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
    }

    void checkIsGrounded()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * 2f);
        if (Physics.Raycast(transform.position, Vector3.down, 2f))
        {
            // Check if the raycast hits an object on the terrain layer
            isGrounded = true;
        }
        else
        {
            // If no hit, return false
            isGrounded = false;
        }
    }


    //////// Turning ///////////
    
    void OnControllerTurn(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        float turnX = turnInput[0];
        float turnY = turnInput[1];


        if (isPlayerOne && gameScript.playerOneControls == "Controller")
        {
            horizontalTurnAmount = turnX;
            verticalTurnAmount = turnY;
        }

        if (!isPlayerOne && gameScript.playerTwoControls == "Controller")
        {
            horizontalTurnAmount = turnX;
            verticalTurnAmount = turnY;
        }

    }

    void OnKeyboardTurn(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        float turnX = turnInput[0];
        float turnY = turnInput[1];

        if (isPlayerOne && gameScript.playerOneControls == "Keyboard")
        {
            horizontalTurnAmount = turnX;
            verticalTurnAmount = turnY;
        }

        if (!isPlayerOne && gameScript.playerTwoControls == "Keyboard")
        {
            horizontalTurnAmount = turnX;
            verticalTurnAmount = turnY;
        }

    }

    void horizontalTurns()
    {
        transform.Rotate(Vector3.up * horizontalTurnAmount * Time.deltaTime * horizontalTurnSensitivity);
    }
}
