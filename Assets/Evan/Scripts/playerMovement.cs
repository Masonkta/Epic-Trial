using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public gameHandler gameScript;
    public bool isPlayerOne;
    public Transform forwardTransform;
    public Transform cameraTransform;


    [Header("Turning")]
    public float xSensController;
    public float xSensKeyboard;
    public float ySensController;
    public float ySensKeyboard;

    private float horizontalTurnAmount;
    private float verticalTurnAmount;


    [Header("Moving")]
    public bool sprinting;
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpHeight;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private float forwardMoveAm;
    private float sideMoveAm;
    private bool isGrounded;
    private float speed;
    private float gravity = Physics.gravity.y;

    [Header("Air Dashing")]
    public bool ableToAirDash;


    [Header("Camera")]
    public float camSideOffset;
    public float camHeight;
    public float camDistance;
    public float minHeightOverGround;
    public float xRotation;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        checkIsGrounded();

        // Turning // 
        horizontalTurns();
        verticalTurns();

        // Moving //
        move();

        // Camera
        handleCamera();
    }


    ///////// Jumping //////////

    void Jump()
    {
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void OnControllerJump()
    {
        if ((isPlayerOne && gameScript.playerOneControls == "Controller") || (!isPlayerOne && gameScript.playerTwoControls == "Controller"))
        {
            if (isGrounded)
                Jump();
            else if (playerVelocity.y > 0f && ableToAirDash) // Player is not on ground and has not started falling down
            {
                controller.Move(transform.TransformDirection(transform.forward * 20f));
                ableToAirDash = false;
            }
        }
    }

    void OnKeyboardJump()
    {
        if ((isPlayerOne && gameScript.playerOneControls == "Keyboard") || (!isPlayerOne && gameScript.playerTwoControls == "Keyboard"))
        {
            if (isGrounded)
                Jump();
            else if (playerVelocity.y > 0f && ableToAirDash) // Player is not on ground and has not started falling down
            {
                controller.Move( forwardTransform.transform.forward*3f);
                ableToAirDash = false;
            }
        }

        
    }

    void checkIsGrounded()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded) ableToAirDash = true;
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
            float turnStrength = gameScript.playerOneControls == "Keyboard" ? xSensKeyboard : xSensController;
            transform.Rotate(Vector3.up * horizontalTurnAmount * turnStrength * Time.deltaTime);
        }
        else
        {
            float turnStrength = gameScript.playerTwoControls == "Keyboard" ? xSensKeyboard : xSensController;
            transform.Rotate(Vector3.up * horizontalTurnAmount * turnStrength * Time.deltaTime);
        }
    }

    void verticalTurns()
    {
        if (isPlayerOne)
        {
            float turnStrength = gameScript.playerOneControls == "Keyboard" ? ySensKeyboard : ySensController;
            xRotation -= verticalTurnAmount * turnStrength * Time.deltaTime;
        }
        else
        {
            float turnStrength = gameScript.playerTwoControls == "Keyboard" ? ySensKeyboard : ySensController;
            xRotation -= verticalTurnAmount * turnStrength * Time.deltaTime;
        }

        xRotation = Mathf.Clamp(xRotation, -55f, 55f);
        forwardTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }



    //// Moving ////

    void OnControllerMove(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        float moveX = turnInput[0];
        float moveY = turnInput[1];

        if ((isPlayerOne && gameScript.playerOneControls == "Controller") || (!isPlayerOne && gameScript.playerTwoControls == "Controller")) // This is player 1 and player 1 uses controller
        {
            sideMoveAm = moveX;
            forwardMoveAm = moveY;
        }

    }

    void OnKeyboardMove(InputValue value)
    {
        Vector2 turnInput = value.Get<Vector2>();

        float moveX = turnInput[0];
        float moveY = turnInput[1];

        if ((isPlayerOne && gameScript.playerOneControls == "Keyboard") || (!isPlayerOne && gameScript.playerTwoControls == "Keyboard")) // This is player 1 and player 1 uses controller
        {
            sideMoveAm = moveX;
            forwardMoveAm = moveY;
        }

    }

    void OnControllerSprint()
    {
        sprinting = !sprinting;
    }

    void OnKeyboardSprint()
    {
        sprinting = !sprinting;
    }

    void setSpeed()
    {
        if (sprinting && (Mathf.Abs(forwardMoveAm) > 0.05f || Mathf.Abs(sideMoveAm) > 0.05f))
            speed = sprintSpeed;
        else
            speed = walkSpeed;
    }

    void move()
    {
        setSpeed();

        Vector3 moveDirection = new Vector3(sideMoveAm, 0f, forwardMoveAm);

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -1f;
        controller.Move(playerVelocity * Time.deltaTime);
    }



    //// CAMERA ////
    
    void handleCamera()
    {
        // Position the camera behind and above the player
        cameraTransform.localPosition = new Vector3(camSideOffset, camHeight, -camDistance);


        // Find height of ground under the camera
        RaycastHit hit; float heightOfGround = 0f;
        if (Physics.Raycast(cameraTransform.position + Vector3.up * camDistance, Vector3.down, out hit, 20f))
        {
            heightOfGround = hit.point.y + minHeightOverGround;
        }

        if (cameraTransform.transform.position.y < heightOfGround)
        {
            Vector3 prev = cameraTransform.transform.position;
            cameraTransform.transform.position = new Vector3(prev.x, heightOfGround, prev.z);
        }


        // Look at the player
        cameraTransform.LookAt(transform.position + transform.forward * 2f);
    }


}
