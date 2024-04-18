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
    public float horizontalTurnSensitivityController;
    public float horizontalTurnSensitivityKeyboard;
    public float verticalTurnAmount;
    public float verticalTurnSensitivityController;
    public float verticalTurnSensitivityKeyboard;
    public Transform forwardTransform;
    public Transform cameraTransform;

    [Header("Moving")]
    public float forwardMoveAm;
    public float sideMoveAm;

    [Header("Camera")]
    public float camDistance;
    public float camHeight;


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
        verticalTurns();

        // Moving //

        // Camera
        handleCamera();
        cameraTransform.LookAt(transform);
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

        if (isPlayerOne && gameScript.playerOneControls == "Controller") // This is player 1 and player 1 uses controller
        {
            horizontalTurnAmount = turnX;
            verticalTurnAmount = turnY;
        }

        if (!isPlayerOne && gameScript.playerTwoControls == "Controller") // This is player 2 and player 2 uses controller
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
        if (isPlayerOne)
        {
            float turnStrength = gameScript.playerOneControls == "Keyboard" ? horizontalTurnSensitivityKeyboard : horizontalTurnSensitivityController;
            transform.Rotate(Vector3.up * horizontalTurnAmount * turnStrength * Time.deltaTime);
        }
        else
        {
            float turnStrength = gameScript.playerTwoControls == "Keyboard" ? horizontalTurnSensitivityKeyboard : horizontalTurnSensitivityController;
            transform.Rotate(Vector3.up * horizontalTurnAmount * turnStrength * Time.deltaTime);
        }
    }

    void verticalTurns()
    {
        if (isPlayerOne)
        {
            float turnStrength = gameScript.playerOneControls == "Keyboard" ? verticalTurnSensitivityKeyboard : verticalTurnSensitivityController;
            forwardTransform.Rotate(Vector3.right * verticalTurnAmount * -turnStrength * Time.deltaTime);
        }
        else
        {
            float turnStrength = gameScript.playerTwoControls == "Keyboard" ? verticalTurnSensitivityKeyboard : verticalTurnSensitivityController;
            forwardTransform.Rotate(Vector3.right * verticalTurnAmount * -turnStrength * Time.deltaTime);
        }
    }



    //// Moving ////

    void OnControllerMove(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        float moveX = turnInput[0];
        float moveY = turnInput[1];

        if (isPlayerOne && gameScript.playerOneControls == "Controller") // This is player 1 and player 1 uses controller
        {
            sideMoveAm = moveX;
            forwardMoveAm = moveY;
        }

        if (!isPlayerOne && gameScript.playerTwoControls == "Controller") // This is player 2 and player 2 uses controller
        {
            sideMoveAm = moveX;
            forwardMoveAm = moveY;
        }

    }




    //// CAMERA ////
    void handleCamera()
    {
        // Position the camera behind and above the player
        cameraTransform.localPosition = new Vector3(0f, camHeight, -camDistance);

        // Find height of ground under the camera
        RaycastHit hit; float groundHeightUnderCamera = 0f;
        if (Physics.Raycast(cameraTransform.position + Vector3.up * 5f, Vector3.down, out hit, 20f))
        {
            groundHeightUnderCamera = hit.point.y;
            Debug.DrawRay(cameraTransform.position, Vector3.down * 4f, Color.red);
        }
        print(cameraTransform.position);
        // We are under the ground, move up the camera
        if (cameraTransform.position.y < groundHeightUnderCamera)
        {
            print("AHHH");
        }

        // Look at the player
        cameraTransform.LookAt(transform);
    }
}
